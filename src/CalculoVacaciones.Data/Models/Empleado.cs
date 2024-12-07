using System.ComponentModel.DataAnnotations;

namespace CalculoVacaciones.Data.Models
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        public string? PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public string? Departamento { get; set; }

        public string? TipoEmpleado { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es incorrecto.")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo 'Es Jefe' es obligatorio.")]
        public string? EsJefe { get; set; }

        public string? Estado { get; set; }

        [Required(ErrorMessage = "El ID de departamento es obligatorio.")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "El ID de tipo de empleado es obligatorio.")]
        public int IdTipoEmpleado { get; set; }
    }
}
