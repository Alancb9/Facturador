using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CapaDeEntidades;
using System.Windows.Forms;

namespace CapaDeDatos
{
    public class CapaDeDatosFacturacion
    {
        string conexion = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;

        public void InsertarCliente(CapaDeEntidadesCliente _Cliente)
        {
            try
            {
                string Query = "INSERTAR_CLIENTE";
                using (SqlConnection Conn = new SqlConnection(conexion))
                {
                    using (SqlCommand Cmd = new SqlCommand(Query, Conn))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        
                        Cmd.Parameters.AddWithValue("@Nombres", _Cliente.Nombres);
                        Cmd.Parameters.AddWithValue("@Apellidos", _Cliente.Apellidos);
                        Cmd.Parameters.AddWithValue("@Cedula", _Cliente.Cedula);
                        Cmd.Parameters.AddWithValue("@Telefono", _Cliente.Telefono);
                        Cmd.Parameters.AddWithValue("@Correo", _Cliente.Correo);
                        Cmd.Parameters.AddWithValue("@Direccion", _Cliente.Direccion);
                        Cmd.Connection.Open();
                        Cmd.ExecuteNonQuery();
                        Cmd.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error1: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InsertarFactura(CapaDeEntidadesFactura _Facturacion)
        {
            try
            {
                string Query = "INSERTAR_FACTURA";
                using (SqlConnection Conn = new SqlConnection(conexion))
                {
                    using (SqlCommand Cmd = new SqlCommand(Query, Conn))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@IdCliente", _Facturacion.IdCliente);
                        Cmd.Parameters.AddWithValue("@BaseImponibleCero", _Facturacion.BaseImponibleCero);
                        Cmd.Parameters.AddWithValue("@BaseImponibleDoce", _Facturacion.BaseImponibleDoce);
                        Cmd.Parameters.AddWithValue("@Subtotal", _Facturacion.Subtotal);
                        Cmd.Parameters.AddWithValue("@IVA", _Facturacion.IVA);
                        Cmd.Parameters.AddWithValue("@Total", _Facturacion.Total);
                        Cmd.Connection.Open();
                        Cmd.ExecuteNonQuery();
                        Cmd.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error2: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void InsertarDetalle(CapaDeEntidadesDetalle _Detalle)
        {
            try
            {
                string Query = "INSERTAR_DETALLE_FACTURA";
                using (SqlConnection Conn = new SqlConnection(conexion))
                {
                    using (SqlCommand Cmd = new SqlCommand(Query, Conn))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("@IdFactura", _Detalle.IdFactura);
                        Cmd.Parameters.AddWithValue("@DescripcionProducto", _Detalle.DescripcionProducto);
                        Cmd.Parameters.AddWithValue("@Cantidad", _Detalle.Cantidad);
                        Cmd.Parameters.AddWithValue("@PrecioUnitario", _Detalle.PrecioUnitario);
                        Cmd.Parameters.AddWithValue("@IVAProducto", _Detalle.IVAProducto);
                        Cmd.Parameters.AddWithValue("@TotalProducto", _Detalle.TotalProducto);
                        Cmd.Connection.Open();
                        Cmd.ExecuteNonQuery();
                        Cmd.Connection.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error3: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public int ObtenerIdFactura()
        {
            int ultimoIdFactura = 0;
            try
            {
                using (SqlConnection Conn = new SqlConnection(conexion))
                {
                    using (SqlCommand Cmd = new SqlCommand("ObtenerUltimoIdFactura", Conn))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Conn.Open();

                        // Ejecutar el procedimiento almacenado y obtener el resultado
                        ultimoIdFactura = (int)Cmd.ExecuteScalar();
                    }
                }
                return ultimoIdFactura;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error4: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return 0;
            }

        }

    }
}
