using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAP.Models
{
    public class Managers
    {
        [Key]
        public int Id { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public int CaseCount { get; set; }
        public string? ManagerId { get; set; }
    }
}
