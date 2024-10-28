using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SAP.Models
{
    public class UpdateUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentRole { get; set; }
        public string NewRole { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}






















//using Microsoft.AspNetCore.Identity;

//namespace SAP.Models
//{
//    public class UpdateUserRolesViewModel
//    {
//        public string UserId { get; set; }
//        public string UserName { get; set; }
//        public string CurrentRole { get; set; }
//        public string NewRole { get; set; }
//        public List<IdentityRole> Roles { get; set; }
//    }


//}

