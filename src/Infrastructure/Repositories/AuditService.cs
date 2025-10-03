//using Domain.Interfaces;
//using Domain.Models;
//using Infrastructure.Context;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Repositories
//{
//    public class AuditService : IAuditService
//    {
//        private readonly AppDbContext _context;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly UserManager<AppUser> _userManager;

//        public AuditService(
//            AppDbContext context,
//            IHttpContextAccessor httpContextAccessor,
//            UserManager<AppUser> userManager)
//        {
//            _context = context;
//            _httpContextAccessor = httpContextAccessor;
//            _userManager = userManager;
//        }

//        public async Task LogActivity(string entityName, string entityId, string action, string oldValues = null, string newValues = null)
//        {
//            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
//            string userEmail = "System";

//            if (!string.IsNullOrEmpty(userId))
//            {
//                var user = await _userManager.FindByIdAsync(userId);
//                userEmail = user?.Email ?? "Unknown";
//            }

//            // Calculate changes if both old and new values are provided
//            string changes = null;
//            if (oldValues != null && newValues != null)
//            {
//                try
//                {
//                    var oldDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(oldValues);
//                    var newDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(newValues);

//                    var changesDict = new Dictionary<string, object>();
//                    foreach (var key in newDict.Keys)
//                    {
//                        if (!oldDict.ContainsKey(key) || !Equals(oldDict[key], newDict[key]))
//                        {
//                            changesDict[key] = newDict[key];
//                        }
//                    }

//                    changes = JsonConvert.SerializeObject(changesDict);
//                }
//                catch
//                {
//                    // In case of deserialization errors, just store the full values
//                    changes = "Could not calculate changes";
//                }
//            }

//            var log = new EntityLog
//            {
//                Id = Guid.NewGuid(),
//                EntityName = entityName,
//                EntityId = entityId,
//                Action = action,
//                UserId = userId,
//                UserEmail = userEmail,
//                Timestamp = DateTime.UtcNow,
//                OldValues = oldValues,
//                NewValues = newValues,
//                Changes = changes
//            };

//            await _context.EntityLogs.AddAsync(log);
//            await _context.SaveChangesAsync();
//        }
//    }
//} 
