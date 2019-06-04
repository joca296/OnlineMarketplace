using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineMarketPlace.Application.DataTransfer
{
    public class CreateCategotyDto
    {
        [Required]
        public string Name { get; set; }
    }
}
