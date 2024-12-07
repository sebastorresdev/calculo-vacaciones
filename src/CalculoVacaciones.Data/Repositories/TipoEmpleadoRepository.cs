using CalculoVacaciones.Data.Interfaces;
using CalculoVacaciones.Data.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalculoVacaciones.Data.Repositories;
public class TipoEmpleadoRepository : ITipoEmpleadoRepository
{
    private readonly string _cadenaSql;

    public TipoEmpleadoRepository(string cadenaSql)
    {
        _cadenaSql = cadenaSql;
    }

    public List<TipoEmpleado> Listar()
    {
        List<TipoEmpleado> tipoEmpleados = [];

        using var conn = new SqlConnection(_cadenaSql);

        try
        {
            conn.Open();

            using SqlCommand cmd = new ("ObtenerTipoEmpleados", conn);
            // Ejecutar la consulta y obtener los resultados
            cmd.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TipoEmpleado tipoEmpleado = new()
                {
                    Id = Convert.ToInt32(reader["IdTipoEmpleado"]),
                    Nombre = reader["NombreTipo"].ToString(),
                    DiasVacacionesAnuales = Convert.ToDecimal(reader["DiasVacacionesAnuales"])
                };

                tipoEmpleados.Add(tipoEmpleado);
            }
        }
        catch (Exception ex)
        {
            // Manejar el error (log, rethrow, etc.)
            Console.WriteLine(ex.Message);
        }

        return tipoEmpleados;
    }
}
