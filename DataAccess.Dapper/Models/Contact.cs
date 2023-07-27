using System.ComponentModel.DataAnnotations;

namespace  DataAccess.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre Obligatorio")]
        public string Name { get; set; }
        [Required]
        public string Email{ get; set; }
        [Required]
        public DateTime Created { get; set; }
    }
}
