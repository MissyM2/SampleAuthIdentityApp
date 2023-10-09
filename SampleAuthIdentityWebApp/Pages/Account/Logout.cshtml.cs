using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleAuthIdentityWebApp.Data.Account;

namespace SampleAuthIdentityWebApp.Pages.Account
{

    // the only difference between this one and the example without Identity
    // is that you dependency inject the SignInManager instead of the ApplicationDBContext
    public class LogoutModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.SignInManager<User> signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            await signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
