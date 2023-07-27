using System.ComponentModel.DataAnnotations;

namespace QrMvc.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre Obligatorio")] 
        public string Name { get; set; }
        public string Email{ get; set; }
        public DateTime Created { get; set; }
    }
}
