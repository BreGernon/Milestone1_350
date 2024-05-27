using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Milestone1_350.Models
{
    public class RegisterModel
    {
        public RegisterModel()
        {
            //empty constructor
        }
        public RegisterModel(string firstName, string lastName, string sex, int age, string state, string email, string username, string password, string confirmPassword)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.sex = sex;
            this.age = age;
            this.state = state;
            this.email = email;
            this.username = username;
            this.password = password;
            this.confirmPassword = confirmPassword;
        }

        [Required]
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [Required]
        [DisplayName("Sex")]
        public string sex { get; set; }
        [Required]
        [DisplayName("Age")]
        public int age { get; set; }
        [Required]
        [DisplayName("State")]
        public string state { get; set; }
        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [DisplayName("Username")]
        public string username { get; set; }
        [Required]
        [DisplayName("Password")]
        public string password { get; set; }
        [Required]
        [DisplayName("Re-enter Password")]
        public string confirmPassword { get; set; }

    }

}
