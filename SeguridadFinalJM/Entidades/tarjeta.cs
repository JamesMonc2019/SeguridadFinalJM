using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeguridadFinalJM.Entidades
{
    public class tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required(ErrorMessage = "Introduce el nombre")]
        [StringLength(50, ErrorMessage = "Tamaño Maximo del nombre es de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Solo acepta letras.")]
        public string nombre { get; set; } = string.Empty;
        public string plastico { get; set; } = string.Empty;

        [Required]
        public string shatarjeta { get; set; } = string.Empty;
        [Required]
        public string encryptedtarjeta { get; set; } = string.Empty;

    }
}
