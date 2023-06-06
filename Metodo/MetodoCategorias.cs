using DB;
using Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo
{
    public class MetodoCategorias
    {
        public List<Categorias> Listar()
        {
            List<Categorias> lista = new List<Categorias>();
            AccesoDatos datos = new AccesoDatos();
            try
			{
                datos.setearConsulta("select id, descripcion from categorias");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Categorias categ = new Categorias();
                    categ.Id =(int)datos.Lector["Id"];
                    categ.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(categ);
                }
                return lista;

			}
			catch (Exception ex)
			{

				throw ex;
			}
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
