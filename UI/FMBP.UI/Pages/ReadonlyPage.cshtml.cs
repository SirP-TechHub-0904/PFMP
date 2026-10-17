using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMBP.UI.Pages
{
    public class ReadonlyPageModel : PageModel
    {
        private readonly RoleManager<AppRole> _role;

        public ReadonlyPageModel(RoleManager<AppRole> role)
        {
            _role = role;
        }

        //public async Task OnGet()
        //{
        //    AppRole Managerf = new AppRole
        //    {
        //        Name = "NFDS",
        //        Description = "Description for the new role" // You can set this to any value you want
        //    };
        //    try
        //    {
        //        var checkManagerf = await _role.FindByNameAsync("NFDS");

        //        if (checkManagerf == null)
        //        {
        //            await _role.CreateAsync(Managerf);
        //        }
        //    }
        //    catch (Exception ex) { }
        //    //
        //    AppRole xManagerf = new AppRole
        //    {
        //        Name = "SNO",
        //        Description = "Description for the new role" // You can set this to any value you want
        //    };
        //    try
        //    {
        //        var checkManagerf = await _role.FindByNameAsync("SNO");

        //        if (checkManagerf == null)
        //        {
        //            await _role.CreateAsync(xManagerf);
        //        }
        //    }
        //    catch (Exception ex) { }

        //}
    }
}
