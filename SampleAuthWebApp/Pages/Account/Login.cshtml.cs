using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleAuthWebApp.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace SampleAuthWebApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid) return Page();

            // verify the credential
            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                //  Creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", "True"),
                    new Claim("Manager", "True"),
                    new Claim("EmploymentDate", "2023-05-01")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                // encrypt and serialize the principal into a string using HTTPContext object and save to cookie
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);

                // if everything works correctly, redirect to appropriate page
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}