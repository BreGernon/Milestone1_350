using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Milestone1_350.Models
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Username")]
        public string username { get; set; }
        
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }    
    }
}
