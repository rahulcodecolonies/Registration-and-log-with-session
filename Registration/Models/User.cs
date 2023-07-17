using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    public class User
    {
        [DefaultValue(0)]
        public int Id { get; set; }
        [Required(ErrorMessage ="enter mail")]
        [Display(Name ="email address")]
        [EmailAddress] public string Email { get; set;}
        [Required(ErrorMessage ="pass")]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
}
