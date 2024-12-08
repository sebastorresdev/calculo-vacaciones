namespace CalculoVacaciones.Data.Models;
public class Vacaciones
{
    public int Id { get; set; }
    public Empleado? Empleado { get; set; }
    public int VacacionesDisponibles { get; set; }
    public DateTime FechaSeleccionada { get; set; }
}
