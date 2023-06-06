using Elementos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;
using System.Collections;

namespace Metodo
{
    public class MetodoArticulos
    { AccesoDatos datos = new AccesoDatos();
        public List<Articulos> listar()


        {
            List<Articulos> lista = new List<Articulos>();

            try
            {
                datos.setearConsulta("select a.Id, codigo, nombre, a.Descripcion, m.Descripcion Marca, c.Descripcion Categoria, ImagenUrl, Precio, a.iDMarca, a.iDCategoria from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca=m.Id and a.IdCategoria=c.Id");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos art = new Articulos();
                    art.Id = (int)datos.Lector["Id"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Marca = new Marca();
                    int marca = (int)datos.Lector["Id"];
                    art.Marca.Id = marca;
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["Id"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(art);

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

        public void agregar(Articulos art)
        {
            try
            {
                datos.setearConsulta("Insert into articulos values (@cod, @nombre, @desc, @marca, @categ, @img, @precio)");
                datos.setearParametro("@cod", art.Codigo);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@desc", art.Descripcion);
                datos.setearParametro("@marca", art.Marca.Id);
                datos.setearParametro("@categ", art.Categoria.Id);
                datos.setearParametro("@img", art.UrlImagen);
                datos.setearParametro("@precio", art.Precio);
                datos.ejecutarAccion();
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

        public void modificar(Articulos art)
        {
            try
            {
                datos.setearConsulta("update articulos set codigo=@codigo, nombre=@nombre, descripcion=@descripcion, idMarca=@idMarca, idCategoria=@idCateg, imagenUrl=@img, precio=@precio where id=@id");
                datos.setearParametro("@codigo", art.Codigo);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@descripcion", art.Descripcion);
                datos.setearParametro("@idMarca", art.Marca.Id);
                datos.setearParametro("@idCateg", art.Categoria.Id);
                datos.setearParametro("@img", art.UrlImagen);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@id", art.Id);
                datos.ejecutarAccion();
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
        public void eliminar(Articulos art) 
        {
            try
            {
                datos.setearConsulta("delete from articulos where id=@id");
                datos.setearParametro("@id", art.Id);
                datos.ejecutarAccion();
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
        
        public List<Articulos> filtrar(string campo, string criterio, string filtro)
        {   List<Articulos> lista=new List<Articulos>();
            try
            {
                string consulta = "select a.Id, codigo, nombre, a.Descripcion, m.Descripcion Marca, c.Descripcion Categoria, ImagenUrl, Precio, a.iDMarca, a.iDCategoria from ARTICULOS a, MARCAS m, CATEGORIAS c where a.IdMarca=m.Id and a.IdCategoria=c.Id and ";
                switch (campo)
                {
                    case "Código":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "codigo like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "codigo like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "codigo like '%"+filtro+"%'";
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "nombre like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Descripción":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "a.descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "a.descripcion like '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "a.descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "precio > " + filtro ;
                                break;
                            case "Menor a":
                                consulta += "precio < " + filtro;
                                break;
                            case "Igual a":
                                consulta += "precio = " + filtro;
                                break;
                        }
                        break;


                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulos art = new Articulos();
                    art.Id = (int)datos.Lector["Id"];
                    art.Codigo = (string)datos.Lector["Codigo"];
                    art.Nombre = (string)datos.Lector["Nombre"];
                    art.Descripcion = (string)datos.Lector["Descripcion"];
                    art.Marca = new Marca();
                    int marca = (int)datos.Lector["Id"];
                    art.Marca.Id = marca;
                    art.Marca.Descripcion = (string)datos.Lector["Marca"];
                    art.Marca.Id = (int)datos.Lector["Id"];
                    art.Categoria = new Categorias();
                    art.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        art.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    art.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(art);
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
