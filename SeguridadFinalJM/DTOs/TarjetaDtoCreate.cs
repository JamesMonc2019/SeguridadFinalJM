using System.ComponentModel.DataAnnotations;

namespace SeguridadFinalJM.DTOs
{
    public class TarjetaDtoCreate
    {
        [Required(ErrorMessage = "Introduce el nombre")]
        [StringLength(50, ErrorMessage = "Tamaño Maximo del nombre es de 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$",  ErrorMessage = "El nombre solo acepta letras.")]
        public string nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "Introduce el plastico")]
        [MaxLength(16, ErrorMessage = "Tamaño Maximo del plastico es de 16 caracteres")]
        [MinLength(16, ErrorMessage = "Tamaño Minimo del plastico es de 16 caracteres")]
        [RegularExpression(@"^[0-9]{1,16}$", ErrorMessage = "La tarjeta solo acepta numeros")]
        public string plastico { get; set; } = string.Empty;
        public string? shatarjeta { get; set; }
        public string? encryptedtarjeta { get; set; }

    }
}
