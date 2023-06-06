using DB;
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

namespace Ventana
{
    public partial class frmCatalogo : Form

    {
        private List<Articulos> listaArticulos;

        public frmCatalogo()
        {
            InitializeComponent();
        }

        private void frmCatalogo_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Precio");
            ocultarColumna("UrlImagen");
            ocultarColumna("Id");


        }
        public void ocultarColumna(string columna)
        {
            dgvArticulos.Columns[columna].Visible = false;
        }
        public void cargar()
        {
            try
            {
                MetodoArticulos Marticulo = new MetodoArticulos();
                listaArticulos = Marticulo.listar();
                dgvArticulos.DataSource = listaArticulos;
                cargarImagen(listaArticulos[0].UrlImagen);

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
                pboArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pboArticulo.Load("https://www.campana.gob.ar/wp-content/uploads/2022/05/placeholder-1.png");

            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulos seleccionado = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmNuevoArticulo frmNuevo = new frmNuevoArticulo();
            frmNuevo.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulos articulos;
            articulos = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            frmNuevoArticulo frmNuevoArticulo = new frmNuevoArticulo(articulos);
            frmNuevoArticulo.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MetodoArticulos metart = new MetodoArticulos();
            Articulos articulo;
            articulo = (Articulos)dgvArticulos.CurrentRow.DataBoundItem;
            DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar permanentemente este ítem?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta == DialogResult.Yes)
                metart.eliminar(articulo);
            MessageBox.Show("Eliminado exitosamente");
            cargar();
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulos> listaFiltrada = new List<Articulos>();
            string filtro = txtFiltroRapido.Text;
            if (filtro != "")
            {
                listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Codigo.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listaFiltrada = listaArticulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumna("UrlImagen");
            ocultarColumna("Id");
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Código")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
            if (opcion == "Nombre")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
            if (opcion == "Descripción")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {   if (validarFiltro())
                    return;
                MetodoArticulos metart=new MetodoArticulos();
                string campo = cboCampo.SelectedItem.ToString();
                string criterio= cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvArticulos.DataSource= metart.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnActualizarLista_Click(object sender, EventArgs e)
        {
            cargar();
        }
        private bool validarFiltro()
        {
            if(cboCampo.SelectedIndex<0)
            {
                MessageBox.Show("Por favor seleccione un campo");
                return true;
            }
            if(cboCriterio.SelectedIndex<0)
            {
                MessageBox.Show("Por favor seleccione un criterio");
                return true;
            }
            if(cboCampo.SelectedItem.ToString()=="Precio")
            {
                if(string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Debes introducir un filtro de búsqueda");
                    return true;
                }
                if(soloNumeros(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Solo se deben ingresar números");
                    return true;
                }
               
            }
            return false;
        }
        private bool soloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if(char.IsNumber(caracter))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
