using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDeNegocio;
using CapaDeEntidades;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace CapaPresentacion
{
    public partial class Facturador : Form
    {

        CapaDeNegocioFacturacion CapaNegocio;
        CapaDeEntidadesFactura EntidadFactura;
        CapaDeEntidadesCliente EntidadCliente;
        public Facturador()
        {
            InitializeComponent();
            CapaNegocio = new CapaDeNegocioFacturacion();
            EntidadFactura = new CapaDeEntidadesFactura();
            EntidadCliente = new CapaDeEntidadesCliente();
            
        }

        private void Facturador_Load(object sender, EventArgs e)
        {
            EntidadFactura.IdFactura = CapaNegocio.ObtenerIdFactura();
            if (EntidadFactura.IdFactura == 0)
            {
                EntidadFactura.IdFactura = 1;
            }
            else
            {
                EntidadFactura.IdFactura += 1;
            }
            txtNumeroFactura.Text = EntidadFactura.IdFactura.ToString();
            cmbIva.Text = "12";
            txtSubtotal12.Text = "0";
            txtSubtotal0.Text = "0";
            txtSubtotal.Text = "0";
            txtIva12.Text = "0";
            txtTotalAPagar.Text = "0";
        }

        private void btnIngresoProducto_Click(object sender, EventArgs e)
        {
            decimal total = numPrecioUnitario.Value * numCantidad.Value;
            txtSubtotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + total).ToString();
            if (cmbIva.Text == "12")
            {
                decimal impuesto = total * 0.12m;
                total += impuesto;
                txtSubtotal12.Text = (Convert.ToDecimal(txtSubtotal12.Text) + (total)).ToString();
                txtIva12.Text = (Convert.ToDecimal(txtIva12.Text) + impuesto).ToString();
            }
            else
            {
                txtSubtotal0.Text = (Convert.ToDecimal(txtSubtotal0.Text) + (total)).ToString();
            }

            object[] nuevaFila = new object[]
            {
                numCantidad.Value,
                txtDescripcion.Text,
                numPrecioUnitario.Value,
                cmbIva.Text,
                total
            };

            txtTotalAPagar.Text = (Convert.ToDecimal(txtTotalAPagar.Text) + total).ToString();

            dgvdetalle.Rows.Add(nuevaFila);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                EntidadCliente.Nombres = txtNombres.Text;
                EntidadCliente.Apellidos = txtApellidos.Text;
                EntidadCliente.Cedula = txtCedula.Text;
                EntidadCliente.Telefono = (int)Convert.ToInt64(txtTelefono.Text);
                EntidadCliente.Correo = txtCorreo.Text;
                EntidadCliente.Direccion = txtDireccion.Text;
                CapaNegocio.InsertarCliente(EntidadCliente);

                EntidadFactura.IdCliente = EntidadFactura.IdFactura;
                EntidadFactura.BaseImponibleDoce = Convert.ToDecimal(txtSubtotal12.Text);
                EntidadFactura.BaseImponibleCero = Convert.ToDecimal(txtSubtotal0.Text);
                EntidadFactura.Subtotal = Convert.ToDecimal(txtSubtotal.Text);
                EntidadFactura.IVA = Convert.ToDecimal(txtIva12.Text);
                EntidadFactura.Total = Convert.ToDecimal(txtTotalAPagar.Text);
                CapaNegocio.InsertarFactura(EntidadFactura);

                foreach (DataGridViewRow row in dgvdetalle.Rows)
                {
                    // Asegúrate de que la fila no esté vacía
                    if (!row.IsNewRow)
                    {
                        // Crear una nueva instancia de la entidad Detalle
                        CapaDeEntidadesDetalle EntidadDetalle = new CapaDeEntidadesDetalle();

                        // Asignar los valores de las celdas a la entidad
                        EntidadDetalle.IdFactura = EntidadFactura.IdFactura;
                        EntidadDetalle.Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                        EntidadDetalle.DescripcionProducto = row.Cells["Descripcion"].Value.ToString();
                        EntidadDetalle.PrecioUnitario = Convert.ToDecimal(row.Cells["PrecioUnitario"].Value);
                        EntidadDetalle.IVAProducto = Convert.ToInt32(row.Cells["ivaProducto"].Value);
                        EntidadDetalle.TotalProducto = Convert.ToDecimal(row.Cells["Total"].Value);

                        // Llamar a la capa de negocio para insertar en la base de datos
                        CapaNegocio.InsertarDetalle(EntidadDetalle);
                    }
                }







            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EntidadFactura.IdFactura = CapaNegocio.ObtenerIdFactura();
            if (EntidadFactura.IdFactura == 0)
            {
                EntidadFactura.IdFactura = 1;
            }
            else
            {
                EntidadFactura.IdFactura += 1;
            }
            txtNumeroFactura.Text = EntidadFactura.IdFactura.ToString();
            

            

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss")) + ".pdf";
            //savefile.ShowDialog();
            
            string PaginaHTML_Texto = Properties.Resources.plantilla.ToString();

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@CLIENTE", txtNombres.Text + txtApellidos.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@DOCUMENTO", txtCedula.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));

            string filas = string.Empty;
            
            foreach (DataGridViewRow row in dgvdetalle.Rows)
            {
                if (!row.IsNewRow)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Descripcion"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioUnitario"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["ivaProducto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Total"].Value.ToString() + "</td>";
                    filas += "</tr>";
                    
                }
                
            }


            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);

            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SUBTOTAL12", txtSubtotal12.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SUBTOTAL0", txtSubtotal0.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@SUBTOTAL", txtSubtotal.Text);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@IVA12", txtIva12.Text);
       
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@PRECIOFINAL", txtTotalAPagar.Text);



            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    //Creamos un nuevo documento y lo definimos como PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);   //Cada que se haga un cambio se guarda el archivo en memoria
                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));
                    using (StringReader sr = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }
                    pdfDoc.Close();
                    stream.Close();

                    try
                    {
                        System.Diagnostics.Process.Start(savefile.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo abrir el archivo PDF: " + ex.Message, "Error al abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }



                }

            }
            cmbIva.Text = "12";
            txtSubtotal12.Text = "0";
            txtSubtotal0.Text = "0";
            txtSubtotal.Text = "0";
            txtIva12.Text = "0";
            txtTotalAPagar.Text = "0";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtCedula.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtDireccion.Text = "";
            txtDescripcion.Text = "";
            numCantidad.Value = 0;
            numPrecioUnitario.Value = 0.0m;
            cmbIva.Text = "12";
            dgvdetalle.Rows.Clear();
        }
    }
}
