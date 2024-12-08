using System.ComponentModel.DataAnnotations;

namespace CalculoVacaciones.Data.Models;
public class TipoEmpleado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string? Nombre { get; set; }

    [Range(1, 30, ErrorMessage = "Los días de vacaciones deben estar entre 1 y 30.")]
    public int DiasVacacionesAnuales { get; set; }
}
