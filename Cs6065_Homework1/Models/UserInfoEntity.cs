using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace Cs6065_Homework1.Models
{
    public class UserInfoEntity
    {
        [Required]
        [Key, ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
