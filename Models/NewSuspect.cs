using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAP.Models
{
    public class NewSuspect
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Suspect ID Number")] 
        [MaxLength(13)]
        public string? SuspectIdNum{ get; set; }
        public string? SuspectId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [MaxLength(50,ErrorMessage = "Maximum characters reached (50)")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? First_Name { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [MaxLength(50, ErrorMessage = "Maximum characters reached (50)")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? Last_Name { get; set; }

        [Required]
        [MaxLength(6)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? Sex { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100 years")]
        public int Age { get; set; }

        [Required]
        [DisplayName("Height (cm)")]
        [Range(typeof(int), "60", "250", ErrorMessage = "Height must be between 60cm and 250cm")]
        public int? Height { get; set; }

        [Required]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? Eye_Color { get; set; }

    }    
}