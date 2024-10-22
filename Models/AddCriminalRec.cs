using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAP.Models
{
    public class AddCriminalRec
    {
        [Key]
        public int Id { get; set; }

        public string? CriminalRecordId { get; set; }
        [ForeignKey("Standard")]
        public string? CriminalIdNum { get; set; }
        [Required]
        [DisplayName("Offence Committed")]
        [MaxLength(50, ErrorMessage = "Maximum characters reached (50)")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? OffenceCommitted { get; set; }

        [Required]
        [DisplayName("Sentence In Years")]
        [Range(1, 99, ErrorMessage = "Max Sentence Reached")]
        public int Sentence { get; set; }

        [Required]
        [DisplayName("Issued At")]
        [MaxLength(50, ErrorMessage = "Maximum characters reached (50)")]
        public string? IssuedAt { get; set; }

        [Required]
        [DisplayName("Issued By")]
        [MaxLength(50, ErrorMessage = "Maximum characters reached (50)")]
        public string? IssuedBy { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string? IssueDate { get; set; }

        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public int Age { get; set; }


    }
}

        