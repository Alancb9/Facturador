using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDeEntidades
{
    public class CapaDeEntidadesFactura
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public decimal BaseImponibleCero { get; set; }
        public decimal BaseImponibleDoce { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
    }

    public class CapaDeEntidadesCliente
    {
        //public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }

    public class CapaDeEntidadesDetalle
    {
        public int IdFactura { get; set; }
        public string DescripcionProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IVAProducto { get; set; }
        public decimal TotalProducto { get; set; }
    }
}
