using Amazon;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorWebUI.Areas.User.Pages.Admin
{
    public class UpdateEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UpdateEmailModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {

            [Required]
            public string NewEmail { get; set; }

        }

        [BindProperty]
        public AppUser UserData { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserData = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(UserData.Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            UserData = user;

            // Check if the new email is already taken
            var checkemail = await _userManager.FindByEmailAsync(Input.NewEmail);
            if (checkemail == null)
            {
                // Generate token for email change
                var emailtoken = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var changeEmailResult = await _userManager.ChangeEmailAsync(user, Input.NewEmail, emailtoken);

                if (changeEmailResult.Succeeded)
                {
                    // Update UserName as well (since username = email in your case)
                    user.UserName = Input.NewEmail;
                    var updateUserNameResult = await _userManager.UpdateAsync(user);

                    if (updateUserNameResult.Succeeded)
                    {
                        TempData["success"] = "Email and Username updated successfully";
                        return RedirectToPage("./info", new { id = user.Id });
                    }
                    else
                    {
                        TempData["error"] = "Email updated, but unable to update Username";
                        return Page();
                    }
                }

                TempData["error"] = "Unable to change Email";
                return Page();
            }
            else
            {
                TempData["error"] = "Email Already Taken";
                return Page();
            }
        }
    }

}
