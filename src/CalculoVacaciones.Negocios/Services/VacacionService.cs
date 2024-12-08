using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalculoVacaciones.Negocios.Services;
public class VacacionService : IVacacionService
{
    private readonly string _cadenaSql;

    public VacacionService(string cadenaSql)
    {
        _cadenaSql = cadenaSql;
    }

    public double ObtenerDiasTomados(int id, DateTime fechaSeleccionada)
    {
        using var conexion = new SqlConnection(_cadenaSql);
        
        try
        {
            conexion.Open();
            SqlCommand cmd = new ("ObtenerDiasTomados", conexion);

            cmd.Parameters.AddWithValue("@IdEmpleado", id);
            cmd.Parameters.AddWithValue("@FechaSeleccionada", fechaSeleccionada);
            cmd.CommandType = CommandType.StoredProcedure;

            // Usamos ExecuteScalar para obtener el primer valor de la primera columna
            var resultado = cmd.ExecuteScalar();

            // Convertimos el resultado a double (si es nulo, asignamos 0)
            return resultado != DBNull.Value ? Convert.ToDouble(resultado) : 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }
}
