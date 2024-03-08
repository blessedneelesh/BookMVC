using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMVC.Models
{
    public class User : IdentityUser
    {
        //inherit other properties
        [NotMapped]
        public IList<string> RoleName { get; set; }
    }
}
