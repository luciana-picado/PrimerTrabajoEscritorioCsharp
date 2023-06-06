using DB;
using Elementos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo
{
    public class MetodoMarcas
    {
        public List<Marca> Listar()
        {   
            List<Marca> lista= new List<Marca> ();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, descripcion from marcas");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                { Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
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
