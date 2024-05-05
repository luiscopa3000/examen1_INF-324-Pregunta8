using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace problema7
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        // Cadena de conexión a la base de datos
        string connectionString = "server=(local);user=sa;pwd=12345678;database=DBLuis";
        [WebMethod]
        public DataSet Listar()
        {
            DataSet dsPersonas = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM persona", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsPersonas);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }

            return dsPersonas;
        }
        [WebMethod]
        public DataSet mostra1(string ci)
        {
            DataSet dsPersonas = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM persona where ci = @ci", con);
                    cmd.Parameters.AddWithValue("@ci", ci);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dsPersonas);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }

            return dsPersonas;
        }

        [WebMethod]
        public string Alta(string ci, string nombres, string paterno, string materno, string fechaNacimiento, string genero, string direccion, string telefono, string celular, string correo, string password, string rol, string departamento)
        {
            DateTime fecha_new = fechaConvertir(fechaNacimiento);
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO persona (ci, nombres, paterno, materno, fecha_nac, genero, direccion_dom, telefono, celular, correo, password, rol, departamento) VALUES (@ci, @nombres, @paterno, @materno, @fechaNacimiento, @genero, @direccion, @telefono, @celular, @correo, @password, @rol, @departamento)", con);
                    cmd.Parameters.AddWithValue("@ci", ci);
                    cmd.Parameters.AddWithValue("@nombres", nombres);
                    cmd.Parameters.AddWithValue("@paterno", paterno);
                    cmd.Parameters.AddWithValue("@materno", materno);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", fecha_new);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@celular", celular);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@departamento", departamento);
                    return cmd.ExecuteNonQuery() +"";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción: " + ex.ToString());
                return ex.ToString();
            }
        }

        [WebMethod]
        public int Cambio(string ci, string nombres, string paterno, string materno, string fechaNacimiento, string genero, string direccion, string telefono, string celular, string correo, string password, string rol, string departamento)
        {
            DateTime fecha_new = fechaConvertir(fechaNacimiento);
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE persona SET nombres = @nombres, paterno = @paterno, materno = @materno, fecha_nac = @fechaNacimiento, genero = @genero, direccion_dom = @direccion, telefono = @telefono, celular = @celular, correo = @correo, password = @password, rol = @rol, departamento = @departamento WHERE ci = @ci", con);
                    cmd.Parameters.AddWithValue("@ci", ci);
                    cmd.Parameters.AddWithValue("@nombres", nombres);
                    cmd.Parameters.AddWithValue("@paterno", paterno);
                    cmd.Parameters.AddWithValue("@materno", materno);
                    cmd.Parameters.AddWithValue("@fechaNacimiento", fecha_new);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@celular", celular);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@departamento", departamento);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Manejo de errores
                return -1;
            }
        }


        [WebMethod]
        public int Baja(string ci)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM persona WHERE ci = @ci", con);
                    cmd.Parameters.AddWithValue("@ci", ci);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return -1;
            }
        }

        public DateTime fechaConvertir(string fechaNacimientoString)
        {

            DateTime fecha = DateTime.ParseExact(fechaNacimientoString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            return fecha;

        }
    }
}
