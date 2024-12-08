using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalculoVacaciones.Negocios.Services;
public class DepartamentoService : IDepartamentoService
{
    private readonly string _cadenaSql;

    public DepartamentoService(string cadenaSql)
    {
        _cadenaSql = cadenaSql;
    }

    public List<Departamento> Listar()
    {
        List<Departamento> departamentos = [];

        using var conn = new SqlConnection(_cadenaSql);

        try
        {
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ObtenerDepartamentos", conn);
            // Ejecutar la consulta y obtener los resultados
            cmd.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Departamento departamento = new()
                {
                    Id = Convert.ToInt32(reader["IdDepartamento"]),
                    Nombre = reader["NombreDepartamento"].ToString(),
                };

                departamentos.Add(departamento);
            }
        }
        catch (Exception ex)
        {
            // Manejar el error (log, rethrow, etc.)
            Console.WriteLine(ex.Message);
        }

        return departamentos;
    }
}
