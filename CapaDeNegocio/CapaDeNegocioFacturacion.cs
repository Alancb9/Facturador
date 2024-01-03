using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeEntidades;
using CapaDeDatos;


namespace CapaDeNegocio
{
    public class CapaDeNegocioFacturacion
    {
        public void InsertarCliente(CapaDeEntidadesCliente entidad_Cliente)
        {
            CapaDeDatosFacturacion CapaDeDatos_Facturacion = new CapaDeDatosFacturacion();
            CapaDeDatos_Facturacion.InsertarCliente(entidad_Cliente);
        }

        public void InsertarFactura(CapaDeEntidadesFactura entidad_Factura)
        {
            CapaDeDatosFacturacion CapaDeDatos_Facturacion = new CapaDeDatosFacturacion();
            CapaDeDatos_Facturacion.InsertarFactura(entidad_Factura);
        }

        public void InsertarDetalle(CapaDeEntidadesDetalle entidad_Detalle)
        {
            CapaDeDatosFacturacion CapaDeDatos_Facturacion = new CapaDeDatosFacturacion();
            CapaDeDatos_Facturacion.InsertarDetalle(entidad_Detalle);
        }
        public int ObtenerIdFactura()
        {
            CapaDeDatosFacturacion capaDeDatos = new CapaDeDatosFacturacion();
            return capaDeDatos.ObtenerIdFactura();
        }
    }
}
