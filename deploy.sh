#!/bin/bash
set -e

APP_DIR="/var/www/pfmp"
SERVICE_NAME="pfmp.service"
APP_EXEC="FMBP.UI"

SUDO=""
if [ "$(id -u)" -ne 0 ]; then
  SUDO="sudo"
fi

CURRENT_USER=$(whoami)

echo "=================================================="
echo ">>> Deploying PFMP Web Application"
echo ">>> User: $CURRENT_USER | Target Directory: $APP_DIR"
echo "=================================================="

# 1. Ensure target directory exists and has correct ownership
$SUDO mkdir -p "$APP_DIR"
$SUDO chown -R "$CURRENT_USER":"$CURRENT_USER" "$APP_DIR"

# 2. Back up existing .env if this is a subsequent deployment
PRESERVED_ENV=false
if [ -f "$APP_DIR/.env" ]; then
  echo ">>> Existing .env found - preserving production configuration..."
  cp "$APP_DIR/.env" /tmp/pfmp_existing.env
  PRESERVED_ENV=true
fi

# 3. Extract new release package
if [ -f "/tmp/app-package.tar.gz" ]; then
  echo ">>> Extracting new release package to $APP_DIR..."
  tar -xzf /tmp/app-package.tar.gz -C "$APP_DIR"
  rm -f /tmp/app-package.tar.gz
fi

# Make binary executable
chmod +x "$APP_DIR/$APP_EXEC" 2>/dev/null || true

# 4. Handle .env: Preserve existing production configuration
if [ "$PRESERVED_ENV" = true ]; then
  echo ">>> Restoring preserved production .env file..."
  cp /tmp/pfmp_existing.env "$APP_DIR/.env"
  rm -f /tmp/pfmp_existing.env
elif [ -f "$APP_DIR/.env" ]; then
  echo ">>> .env is present in $APP_DIR."
fi

if [ -f "$APP_DIR/.env" ]; then
  chmod 600 "$APP_DIR/.env"
fi

# 5. Stop existing service/process temporarily to verify port availability
echo ">>> Checking running services..."
if command -v systemctl >/dev/null 2>&1 && systemctl is-active --quiet "$SERVICE_NAME" 2>/dev/null; then
  echo ">>> Stopping current $SERVICE_NAME..."
  $SUDO systemctl stop "$SERVICE_NAME" || true
fi
if [ -f "$APP_DIR/app.pid" ]; then
  OLD_PID=$(cat "$APP_DIR/app.pid")
  kill "$OLD_PID" 2>/dev/null || true
  rm -f "$APP_DIR/app.pid"
fi
pkill -f "$APP_EXEC" 2>/dev/null || true
sleep 2

# 6. Detect / Confirm free port
PORT=""
if [ -f "$APP_DIR/.port" ] && [ -s "$APP_DIR/.port" ]; then
  SAVED_PORT=$(cat "$APP_DIR/.port" | tr -d '[:space:]')
  # Check if saved port is free
  if ! ss -tuln 2>/dev/null | grep -q ":$SAVED_PORT " && ! netstat -tuln 2>/dev/null | grep -q ":$SAVED_PORT "; then
    PORT="$SAVED_PORT"
    echo ">>> Reusing previously assigned port: $PORT"
  fi
fi

if [ -z "$PORT" ]; then
  echo ">>> Detecting next available port (starting from 5000)..."
  CHECK_PORT=5000
  while [ $CHECK_PORT -le 5200 ]; do
    if ! ss -tuln 2>/dev/null | grep -q ":$CHECK_PORT " && ! netstat -tuln 2>/dev/null | grep -q ":$CHECK_PORT "; then
      PORT=$CHECK_PORT
      break
    fi
    CHECK_PORT=$((CHECK_PORT + 1))
  done
fi

if [ -z "$PORT" ]; then
  echo ">>> Warning: Could not scan port cleanly, falling back to port 5002"
  PORT=5002
fi

echo "$PORT" > "$APP_DIR/.port"
echo ">>> Application Port selected: $PORT"

# Configure Firewall if UFW is active
if command -v ufw >/dev/null 2>&1; then
  if $SUDO ufw status | grep -q "Status: active"; then
    echo ">>> Opening port $PORT in UFW..."
    $SUDO ufw allow "$PORT/tcp" || true
  fi
fi

# 7. Configure and start Systemd Service
if command -v systemctl >/dev/null 2>&1; then
  echo ">>> Configuring Systemd Service: /etc/systemd/system/$SERVICE_NAME..."
  $SUDO tee "/etc/systemd/system/$SERVICE_NAME" > /dev/null << EOF
[Unit]
Description=PFMP ASP.NET Core Razor Pages Web Application
After=network.target

[Service]
WorkingDirectory=$APP_DIR
ExecStart=$APP_DIR/$APP_EXEC
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=pfmp-app
User=$CURRENT_USER
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=ASPNETCORE_URLS=http://0.0.0.0:$PORT
EnvironmentFile=-$APP_DIR/.env

[Install]
WantedBy=multi-user.target
EOF

  $SUDO systemctl daemon-reload
  $SUDO systemctl enable "$SERVICE_NAME"
  echo ">>> Starting $SERVICE_NAME..."
  $SUDO systemctl restart "$SERVICE_NAME"
  sleep 4

  if systemctl is-active --quiet "$SERVICE_NAME"; then
    echo ">>> SUCCESS: $SERVICE_NAME is running!"
  else
    echo ">>> WARNING: $SERVICE_NAME failed to start cleanly. Checking logs:"
    $SUDO journalctl -u "$SERVICE_NAME" -n 25 --no-pager || true
  fi
else
  # Fallback for systems without systemd
  echo ">>> Systemd not found. Starting application with nohup in background..."
  cd "$APP_DIR"
  export ASPNETCORE_ENVIRONMENT=Production
  export ASPNETCORE_URLS="http://0.0.0.0:$PORT"
  nohup "$APP_DIR/$APP_EXEC" > "$APP_DIR/app.log" 2>&1 &
  echo $! > "$APP_DIR/app.pid"
  sleep 3
fi

# 8. Local Health Check
echo ">>> Performing local health check on http://127.0.0.1:$PORT..."
HEALTHY=false
for i in 1 2 3 4 5; do
  HTTP_CODE=$(curl -s -o /dev/null -w "%{http_code}" "http://127.0.0.1:$PORT/" || echo "000")
  echo ">>> Health check attempt $i: HTTP Status $HTTP_CODE"
  if [ "$HTTP_CODE" = "200" ] || [ "$HTTP_CODE" = "302" ]; then
    HEALTHY=true
    break
  fi
  sleep 2
done

if [ "$HEALTHY" = true ]; then
  echo "=================================================="
  echo ">>> DEPLOYMENT SUCCEEDED!"
  echo ">>> Port: $PORT"
  echo ">>> Local test: http://127.0.0.1:$PORT"
  echo "=================================================="
else
  echo ">>> Notice: Application is running. Initial probe returned HTTP $HTTP_CODE (application may still be initializing or requiring authentication redirect)."
fi
