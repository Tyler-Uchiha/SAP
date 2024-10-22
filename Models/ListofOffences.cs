using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace SAP.Models
{
    public class ListofOffences
    {
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [MaxLength(50, ErrorMessage = "Maximum characters reached (50)")]
        public string? Offences { get; set; }
        [MaxLength(4,ErrorMessage =("Max code length reached"))]
        public string? OffenceCode { get; set; }
    }
}
