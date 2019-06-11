using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password not strong enough.")]
        public string NewPassword { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmNewPassword { get; set; }
    }
}
