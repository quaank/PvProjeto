using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProjetoPv.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ProjetoPvUser class
public class ProjetoPvUser : IdentityUser
{
    public bool HasClicked{ get; set; }
}

 