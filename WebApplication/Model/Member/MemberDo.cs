using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Model.Member
{
    public class MemberDo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Account { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}