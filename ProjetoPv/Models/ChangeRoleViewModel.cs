using Microsoft.AspNetCore.Identity;

namespace ProjetoPv.Models
{
    public class ChangeRoleViewModel
    {

        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public IEnumerable<IdentityRole> AllRoles { get; set; }
    }
}

