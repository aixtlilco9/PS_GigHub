using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels
{//cntr+12 to find this then alt+enter+enter to moe to a seperate file and then did alt+R move to folder.
    //not sure why this needed to be moved tho
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        //above was added manually as well as data annotations and tru resharper alt+enter
    }
}