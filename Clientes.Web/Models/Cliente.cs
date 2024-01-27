using System.ComponentModel.DataAnnotations;

namespace Clientes.Web.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "El CUIT debe tener 11 caracteres.")]
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "El teléfono debe ser numérico.")]
        public string Telefono { get; set; }
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; }
        public bool Activo { get; set; }
    }
}