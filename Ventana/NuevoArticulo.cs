using Elementos;
using Metodo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace Ventana
{
    public partial class frmNuevoArticulo : Form
    {
        public frmNuevoArticulo (Articulos articulo)
        {
            InitializeComponent();
            this.articulo= articulo;
            Text = "Modificar articulo";
        }
        private Articulos articulo = null;
        private OpenFileDialog archivo = null;
        public frmNuevoArticulo()
        {
            InitializeComponent();
        }

        private void btnCancelarNuevo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNuevoArticulo_Load(object sender, EventArgs e)
        {
            MetodoMarcas marca = new MetodoMarcas();
            MetodoCategorias categ = new MetodoCategorias();
            try
            {
                cboMarca.DataSource = marca.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categ.Listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                if (articulo != null) 
                {
                  txtCodigo.Text =articulo.Codigo;
                  txtNombre.Text = articulo.Nombre;
                  txtDescripcion.Text=  articulo.Descripcion;
                    txtImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                  cboMarca.SelectedValue = articulo.Marca.Id;
                  cboCategoria.SelectedValue=articulo.Categoria.Id;
                    txtPrecio.Text = articulo.Precio.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pboNuevoArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pboNuevoArticulo.Load("https://www.campana.gob.ar/wp-content/uploads/2022/05/placeholder-1.png");


            }
        }
        private void btnAceptarNuevo_Click(object sender, EventArgs e)
        {   MetodoArticulos metodoArticulos = new MetodoArticulos();
            try
            {   if (validarFiltro())
                    return;
                if (articulo == null)
                    articulo = new Articulos();
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.UrlImagen = txtImagen.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categorias)cboCategoria.SelectedItem;
                articulo.Precio = Decimal.Parse(txtPrecio.Text);
               if (articulo.Id != 0)
                {
                    metodoArticulos.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                    
                }
                else
                {
                    metodoArticulos.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
               //GUARDAR IMAGEN
               //if(archivo!=null && txtImagen.Text.ToUpper().Contains("HTTP"))
               // {
               //     File.Copy(archivo.FileName, ConfigurationManager.AppSettings["Catalogo"] + archivo.SafeFileName);
               // }
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
         }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }

        


        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            archivo= new OpenFileDialog();
            archivo.Filter = "jpg|*.jpg;| png|*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {   
                txtImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);
               //guardar imagen local
               //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["Catalogo-app"]+archivo.SafeFileName);
            }

        }
        private bool validarFiltro()
        {
           if(string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debe introducir un código");
                return true;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe introducir un nombre");
                return true;
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Debe introducir una descripción");
                return true;
            }
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debe introducir un precio");
                return true;

            }
            if(!(soloNumeros(txtPrecio.Text)))
            {
                MessageBox.Show("Solo se deben ingresar números");
                return true;
            }
            return false;

        }
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (char.IsNumber(caracter))
                    return true;
            }
            return false;
        }
    }
}
