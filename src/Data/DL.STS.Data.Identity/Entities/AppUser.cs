using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace DL.STS.Data.Identity.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Company { get; set; }
    }
}
