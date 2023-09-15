using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AjaxAndJQueryProject.Models
{
    public class UserLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Formate")]
        public string Email { get; set; }
        [Required(ErrorMessage ="{0} is Required")]
        [RegularExpression(@"^[A-Za-z0-9]{8,}$")]
        public string Password { get; set; }
        [ForeignKey("AjaxEmployee")]
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
