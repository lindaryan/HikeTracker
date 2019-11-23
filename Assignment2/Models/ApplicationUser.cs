using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace Assignment2.Models
namespace Assignment2
{
    // inherit from asp.net identity's IdentityUser class for authentication
    public class ApplicationUser : IdentityUser
    {
    }
}



//using Microsoft.AspNetCore.Identity;

//namespace Assignment2
//{
//    public class ApplicationUser : IdentityUser<string>
//    {
//    }
//}