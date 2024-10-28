using Microsoft.AspNetCore.Identity;

namespace SAP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string? User_Role { get; set; }
        public string? User_Role_Identifier { get; set; }
        public int CaseCount { get; set; }
    }
}
