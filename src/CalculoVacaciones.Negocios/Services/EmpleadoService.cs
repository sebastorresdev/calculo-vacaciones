using CalculoVacaciones.Data.Models;
using CalculoVacaciones.Negocios.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalculoVacaciones.Negocios.Services;
public class EmpleadoService : IEmpleadoService
{
    private readonly string _cadenaSql;

    public EmpleadoService(string cadenaSql)
    {
        _cadenaSql = cadenaSql;
    }

    public List<Empleado> Listar()
    {
        List<Empleado> empleados = [];

        using var conn = new SqlConnection(_cadenaSql);

        try
        {
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ObtenerEmpleados", conn);
            // Ejecutar la consulta y obtener los resultados
            cmd.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Empleado empleado = new Empleado
                {
                    IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                    Nombre = reader["Nombre"].ToString(),
                    PrimerApellido = reader["PrimerApellido"].ToString(),
                    SegundoApellido = reader["SegundoApellido"].ToString(),
                    Departamento = reader["Departamento"].ToString(),
                    IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                    IdTipoEmpleado = Convert.ToInt32(reader["IdTipoEmpleado"]),
                    TipoEmpleado = reader["TipoEmpleado"].ToString(),
                    FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]),
                    Estado = Convert.ToBoolean(reader["Estado"]) ? "Activo" : "Inactivo",
                    CorreoElectronico = reader["CorreoElectronico"].ToString(),
                    EsJefe = reader["EsJefe"].ToString()
                };

                empleados.Add(empleado);
            }
        }
        catch (Exception ex)
        {
            // Manejar el error (log, rethrow, etc.)
            Console.WriteLine(ex.Message);
        }

        return empleados;
    }

    public int Guardar(Empleado empleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);
        
        try
        {
            
            conexion.Open();
            SqlCommand cmd = new SqlCommand("InsertarEmpleado", conexion);

            cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
            cmd.Parameters.AddWithValue("@PrimerApellido", empleado.PrimerApellido);
            cmd.Parameters.AddWithValue("@SegundoApellido", empleado.SegundoApellido);
            cmd.Parameters.AddWithValue("@IdDepartamento", empleado.IdDepartamento);
            cmd.Parameters.AddWithValue("@EsJefe", empleado.EsJefe == "SI");
            cmd.Parameters.AddWithValue("@IdTipoEmpleado", empleado.IdTipoEmpleado);
            cmd.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
            cmd.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
            cmd.Parameters.AddWithValue("@EsActivo", empleado.Estado == "Activo");
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //devuelve false si hay un error 
            //string error = ex.Message;
            return 0;

        }
    }

    public Empleado ObtenerPorId(int idEmpleado)
    {
        var empleado = new Empleado();

        using var conexion = new SqlConnection(_cadenaSql);

        conexion.Open();

        SqlCommand cmd = new("ObtenerEmpleadoPorId", conexion);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            empleado.IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]);
            empleado.Nombre = reader["Nombre"].ToString();
            empleado.PrimerApellido = reader["PrimerApellido"].ToString();
            empleado.SegundoApellido = reader["SegundoApellido"].ToString();
            empleado.Departamento = reader["Departamento"].ToString();
            empleado.IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]);
            empleado.TipoEmpleado = reader["TipoEmpleado"].ToString();
            empleado.IdTipoEmpleado = Convert.ToInt32(reader["IdTipoEmpleado"]);
            empleado.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
            empleado.Estado = Convert.ToBoolean(reader["Estado"]) ? "Activo" : "Inactivo";
            empleado.CorreoElectronico = reader["CorreoElectronico"].ToString();
            empleado.EsJefe = reader["EsJefe"].ToString();
        }

        return empleado;
    }

    public bool Editar(Empleado empleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new("ActualizarEmpleado", conexion);

            cmd.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);

            cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
            cmd.Parameters.AddWithValue("@PrimerApellido", empleado.PrimerApellido);
            cmd.Parameters.AddWithValue("@SegundoApellido", empleado.SegundoApellido);
            cmd.Parameters.AddWithValue("@IdDepartamento", empleado.IdDepartamento);
            cmd.Parameters.AddWithValue("@EsJefe", empleado.EsJefe == "SI");
            cmd.Parameters.AddWithValue("@IdTipoEmpleado", empleado.IdTipoEmpleado);
            cmd.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
            cmd.Parameters.AddWithValue("@CorreoElectronico", empleado.CorreoElectronico);
            cmd.Parameters.AddWithValue("@EsActivo", empleado.Estado == "Activo");

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

    public bool Eliminar(int idEmpleado)
    {
        using var conexion = new SqlConnection(_cadenaSql);

        try
        {
            conexion.Open();

            SqlCommand cmd = new SqlCommand("EliminarEmpleado", conexion);

            cmd.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

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

}
