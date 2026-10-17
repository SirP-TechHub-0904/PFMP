using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Areas.ITrainings.Pages.Admin
{
    public class EvaluationBarcodeModel : PageModel
    {
        public byte[] BarcodeImage { get; set; }
        public void OnGet()
        {
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            try
            {
                string url = "https://fmbepnutrition.ng/itrainings/account/evaluationrequest";
                System.Drawing.Image qrImg = barcode.Draw(url, 100);

                int padding = 20; // padding around QR
                int textHeight = 40; // space for text at bottom
                int newWidth = qrImg.Width + (padding * 2);
                int newHeight = qrImg.Height + (padding * 2) + textHeight;

                using (var bmp = new System.Drawing.Bitmap(newWidth, newHeight))
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                using (var font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold))
                using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black))
                using (var sf = new System.Drawing.StringFormat() { Alignment = System.Drawing.StringAlignment.Center })
                {
                    // White background
                    g.Clear(System.Drawing.Color.White);

                    // Draw QR in the middle
                    g.DrawImage(qrImg, new System.Drawing.Rectangle(padding, padding, qrImg.Width, qrImg.Height));

                    // Draw text below QR
                    var textRect = new System.Drawing.RectangleF(0, qrImg.Height + (padding * 1.2f), newWidth, textHeight);
                    g.DrawString("Scan for FMBP Daily Evaluation", font, brush, textRect, sf);

                    BarcodeImage = AppServices.TurnImageToByteArray(bmp);
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
