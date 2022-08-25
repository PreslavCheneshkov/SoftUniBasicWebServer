using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework
{
    public class UserIdentity
    {
        public string Id { get; set; }
        [StringLength(5, MinimumLength = 20)]
        [Required]
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
