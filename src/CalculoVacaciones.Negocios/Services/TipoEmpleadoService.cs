using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalculoVacaciones.Negocios.Services;
public class TipoEmpleadoService : ITipoEmpleadoService
{
    private readonly string _cadenaSql;
    public TipoEmpleadoService(string cadenaSql)
    {
        _cadenaSql = cadenaSql;
    }

    public bool Editar(TipoEmpleado tipoEmpleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new("ActualizarTipoEmpleado", conexion);

            cmd.Parameters.AddWithValue("@IdTipoEmpleado", tipoEmpleado.Id);
            cmd.Parameters.AddWithValue("@NombreTipo", tipoEmpleado.Nombre);
            cmd.Parameters.AddWithValue("@DiasVacacionesAnuales", tipoEmpleado.DiasVacacionesAnuales);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return false;
        }
    }

    public bool Eliminar(int idTipoEmpleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new SqlCommand("EliminarTipoEmpleado", conexion);

            cmd.Parameters.AddWithValue("@IdTipoEmpleado", idTipoEmpleado);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();

            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return false;
        }
    }

    public bool Guardar(TipoEmpleado tipoEmpleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            //Abrir la conexión
            conexion.Open();

            SqlCommand cmd = new SqlCommand("GuardarTipoEmpleado", conexion);

            cmd.Parameters.AddWithValue("@NombreTipo", tipoEmpleado.Nombre);
            cmd.Parameters.AddWithValue("@DiasVacacionesAnuales", tipoEmpleado.DiasVacacionesAnuales);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.ExecuteNonQuery();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public List<TipoEmpleado> Listar()
    {
        List<TipoEmpleado> tipoEmpleados = [];

        using var conn = new SqlConnection(_cadenaSql);

        try
        {
            conn.Open();

            using SqlCommand cmd = new("ObtenerTipoEmpleados", conn);
            // Ejecutar la consulta y obtener los resultados
            cmd.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TipoEmpleado tipoEmpleado = new()
                {
                    Id = Convert.ToInt32(reader["IdTipoEmpleado"]),
                    Nombre = reader["NombreTipo"].ToString(),
                    DiasVacacionesAnuales = Convert.ToInt32(reader["DiasVacacionesAnuales"])
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

    public TipoEmpleado ObtenerPorId(int idTipoEmpleado)
    {
        var tipoEmpleado = new TipoEmpleado();

        using var conexion = new SqlConnection(_cadenaSql);

        conexion.Open();

        SqlCommand cmd = new("BuscarTipoEmpleadoPorId", conexion);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@IdTipoEmpleado", idTipoEmpleado);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            tipoEmpleado.Id = Convert.ToInt32(reader["IdTipoEmpleado"]);
            tipoEmpleado.Nombre = reader["NombreTipo"].ToString();
            tipoEmpleado.DiasVacacionesAnuales = Convert.ToInt32(reader["DiasVacacionesAnuales"]);
        }

        return tipoEmpleado;
    }
}
