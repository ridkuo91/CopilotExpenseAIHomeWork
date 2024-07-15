using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GithubCopilotExpenseService.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Category { get; set; }
    }
}