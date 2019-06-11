using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class ChangeEmailDto
    {
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
