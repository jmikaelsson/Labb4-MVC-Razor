using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Labb4_MVC_Razor.Data;
using Labb4_MVC_Razor.Models;
using Labb4_MVC_Razor.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Labb4_MVC_Razor.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationDbContext;

        public RegisterModel(
           UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager,
           IUserStore<IdentityUser> userStore,
           SignInManager<IdentityUser> signInManager,
           ILogger<RegisterModel> logger,
           IEmailSender emailSender,
           ApplicationDbContext applicationDbContext)

        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _applicationDbContext = applicationDbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "E-post är obligatoriskt.")]
            [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
            [Display(Name = "E-post")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
            [StringLength(100, ErrorMessage = "Lösenordet måste vara minst {2} tecken långt och högst {1} tecken långt.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Lösenord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekräfta lösenord")]
            [Compare("Password", ErrorMessage = "Lösenorden stämmer inte överens.")]
            public string ConfirmPassword { get; set; }

            public string Role { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }
            // Customer information fields
            [Required]
            [Display(Name = "Förnamn")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Efternamn")]
            public string LastName { get; set; }
            [Required]
            [Phone]
            [Display(Name = "Telefonnummer")]
            public string PhoneNumber { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            try
            {
                // Kontrollera och skapa roller om de inte existerar
                if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                }
                // Fyll i Input.RoleList med roller
                Input = new InputModel
                {
                    RoleList = (await _roleManager.Roles.Select(x => x.Name).ToListAsync()).Select(roleName => new SelectListItem
                    {
                        Text = roleName,
                        Value = roleName
                    }).ToList()
                };
                ReturnUrl = returnUrl;
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            }
            catch (Exception ex)
            {
                // Logga och hantera fel
                _logger.LogError(ex, "Fel vid inläsning av registreringssidan.");
                throw; // eller hantera felet på ett lämpligt sätt
            }
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Användaren skapade ett nytt konto med lösenord.");

                    var roleToAssign = !string.IsNullOrEmpty(Input.Role) ? Input.Role : SD.Role_Customer;
                    await _userManager.AddToRoleAsync(user, roleToAssign);


                    var userId = await _userManager.GetUserIdAsync(user);

                    var customer = new Customer
                    {
                        ApplicationUserId = userId,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        PhoneNumber = Input.PhoneNumber,
                        Email = Input.Email
                    };
                    _applicationDbContext.Customers.Add(customer);
                    await _applicationDbContext.SaveChangesAsync();


                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Bekräfta din e-postadress",
                        $"Bekräfta ditt konto genom att <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klicka här</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Om vi kom hit så misslyckades något, visa formuläret igen
            return Page();
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
