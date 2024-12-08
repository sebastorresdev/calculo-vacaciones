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

    public bool Editar(Departamento departamento)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new("ActualizarDepartamento", conexion);

            cmd.Parameters.AddWithValue("@IdDepartamento", departamento.Id);
            cmd.Parameters.AddWithValue("@NombreDepartamento", departamento.Nombre);

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

    public bool Eliminar(int idDepartamento)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new SqlCommand("EliminarDepartamento", conexion);

            cmd.Parameters.AddWithValue("@IdDepartamento", idDepartamento);

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

    public bool Guardar(Departamento departamento)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            //Abrir la conexión
            conexion.Open();

            SqlCommand cmd = new SqlCommand("GuardarDepartamento", conexion);

            cmd.Parameters.AddWithValue("@NombreDepartamento", departamento.Nombre);
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

    public Departamento ObtenerPorId(int idDepartamento)
    {
        var departamento = new Departamento();

        using var conexion = new SqlConnection(_cadenaSql);

        conexion.Open();

        SqlCommand cmd = new("ObtenerDepartamentoPorId", conexion);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@IdDepartamento", idDepartamento);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            departamento.Id = Convert.ToInt32(reader["IdDepartamento"]);
            departamento.Nombre = reader["NombreDepartamento"].ToString();
        }

        return departamento;
;
    }
}
