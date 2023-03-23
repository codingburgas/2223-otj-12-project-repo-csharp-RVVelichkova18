using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace ForestrySystem.Models
{
    public class AppUser : IdentityUser
    {

        public string firstName { get; set; }
      
        public string lastName { get; set; }
    }
}
