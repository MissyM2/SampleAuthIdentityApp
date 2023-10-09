using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SampleAuthIdentityWebApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace SampleAuthIdentityWebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEmailService emailService;

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();

        // we have already dependency injected Identity into the AspNetCore so we can grab an instance of it through the constructor
        public RegisterModel(UserManager<IdentityUser> userManager,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            // validate modelstate
            if (!ModelState.IsValid) return Page();

            // validate email address (optional since we already configured it in the options in Program.cs)

            // Create the user
            // create the user object
            var user = new IdentityUser
            {
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email
            };

            var result = await this.userManager.CreateAsync(user, RegisterViewModel.Password);
            if (result.Succeeded)
            {
                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.PageLink(pageName: "/Account/ConfirmEmail",
                    values: new { userId = user.Id, token = confirmationToken });

                // for sending out the email, we need an smtp server

                await emailService.SendAsync("missymaloney1@gmail.com",
                    user.Email,
                    "Please confirm email",
                    $"Please click on this link to confirm your email address: {confirmationLink}");

                return RedirectToPage("/Account/Login");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Register",error.Description);
                }
                return Page();
            }
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
