using System.ComponentModel.DataAnnotations;

namespace CalculoVacaciones.Data.Models;
public class Departamento
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del departamento es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string? Nombre { get; set; }
}
