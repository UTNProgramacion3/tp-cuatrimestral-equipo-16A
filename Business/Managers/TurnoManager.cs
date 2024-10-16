using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/*namespace Business.Managers
{
    public class ArticuloManager : ICrudRepository<Turno>
    {
        private DBManager _dbManager;

        public ArticuloManager()
        {
            _dbManager = new DBManager();
        }

        public Turno Crear(Turno entity)
        {
            string query = @"Insert into Turnos values (@IdMedico, @IdPaciente, @Fecha)";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdMedico", entity.IdMedico),
                    new SqlParameter("@IdPaciente", entity.IdPaciente),
                    new SqlParameter("@Fecha", entity.Fecha)
                   
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return new Turno();
                }
                else
                {
                    return entity;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(int id)
        {
            string query = @"Update Turnos
                            Set Activo = 0
                            Where Id = @Id";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

            try
            {

                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas al : " + ex.Message.ToString());
            }
        }
        public Turno ObtenerPorId(int id)
        {
            string query = @"SELECT 
                                A.Id,
                                A.Codigo,
                                A.Nombre,
                                A.Descripcion,
                                A.Precio,
                                M.Id AS MarcaId,
                                M.Descripcion AS MarcaDescripcion,
                                C.Id AS CategoriaId,
                                C.Descripcion AS CategoriaDescripcion
                            FROM ARTICULOS A
                            LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                            LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
                            WHERE A.Id = @Id;";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

            try
            {

                DataTable res = _dbManager.ExecuteQuery(query, parametros);

                if (res.Rows.Count == 0)
                {
                    return new Turno();
                }

                Turno turno = _mapper.MapFromRow(res.Rows[0]);

                return articulo;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas al obtener articulo por id: " + ex.Message.ToString());
            }


        }

        public List<ArticuloDTO> ObtenerTodos()
        {
            string query = @"SELECT 
                    A.Id AS Articulo_Id,
                    A.Codigo AS Articulo_Codigo,
                    A.Nombre AS Articulo_Nombre,
                    A.Descripcion AS Articulo_Descripcion,
                    A.Precio AS Articulo_Precio,
                    M.Id AS Marca_Id,
                    M.Descripcion AS Marca_Descripcion,
                    C.Id AS Categoria_Id,
                    C.Descripcion AS Categoria_Descripcion
                FROM ARTICULOS A
                LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id;";

            try
            {

                DataTable res = _dbManager.ExecuteQuery(query);

                if (res.Rows.Count == 0)
                {
                    return new List<ArticuloDTO>();
                }

                var articulosList = _mapper.ListMapFromRow(res);

                return articulosList;
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas al obtener todos los articulos: " + ex.ToString());
            }
        }

        public bool Update(ArticuloDTO entity)
        {
            string query = @"Update ARTICULOS 
                            Set Codigo = @Codigo,
                                Nombre = @Nombre,
                                Descripcion = @Descripcion,
                                IdMarca = @IdMarca,
                                IdCategoria = @IdCategoria,
                                Precio = @Precio
                            Where Id = @Id";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Codigo", entity.Articulo.Codigo),
                    new SqlParameter("@Nombre", entity.Articulo.Nombre),
                    new SqlParameter("@Descripcion", entity.Articulo.Descripcion),
                    new SqlParameter("@IdMarca", entity.Articulo.IdMarca),
                    new SqlParameter("@IdCategoria", entity.Articulo.IdCategoria),
                    new SqlParameter("@Precio", entity.Articulo.Precio),
                    new SqlParameter("@Id", entity.Articulo.Id)
                };

            try
            {

                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al hacer update: " + ex.ToString());
            }

        }

        private string FilterQueryBuilder(string campo, string condicion, string filtro, bool eliminados)
        {
            string query = @"SELECT 
                    A.Id AS Articulo_Id,
                    A.Codigo AS Articulo_Codigo,
                    A.Nombre AS Articulo_Nombre,
                    A.Descripcion AS Articulo_Descripcion,
                    A.Precio AS Articulo_Precio,
                    M.Id AS Marca_Id,
                    M.Descripcion AS Marca_Descripcion,
                    C.Id AS Categoria_Id,
                    C.Descripcion AS Categoria_Descripcion
                FROM ARTICULOS A
                LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id 
                Where ";

            if (eliminados == false)
            {
                query += "A.Codigo != '0000' And ";
            }

            if (campo == "Precio")
            {
                switch (condicion)
                {
                    case "Mayor a":
                        query += "A.Precio > " + filtro;
                        break;
                    case "Menor a":
                        query += "A.Precio < " + filtro;
                        break;
                    case "Igual a":
                        query += "A.Precio = " + filtro;
                        break;
                }
            }
            else
            {
                switch (campo)
                {
                    case "Codigo":
                        query += "A.Codigo ";
                        break;
                    case "Nombre":
                        query += "A.Nombre ";
                        break;
                    case "Marca":
                        query += "M.Descripcion ";
                        break;
                    case "Categoria":
                        query += "C.Descripcion ";
                        break;
                }

                switch (condicion)
                {
                    case "Empieza con":
                        query += "like  '" + filtro + "%' ";
                        break;
                    case "Termina por":
                        query += "like '%" + filtro + "' ";
                        break;
                    case "Igual a":
                        query += "like '%" + filtro + "%' ";
                        break;
                }

            }

            return query;
        }

        public List<ArticuloDTO> Filtrar(string campo, string condicion, string filtro, bool eliminados)
        {
            List<ArticuloDTO> listaFiltrada;

            string query = FilterQueryBuilder(campo, condicion, filtro, eliminados);

            try
            {
                DataTable res = _dbManager.ExecuteQuery(query);

                if (res.Rows.Count == 0)
                {
                    return new List<ArticuloDTO>();
                }

                listaFiltrada = _mapper.ListMapFromRow(res);

                return listaFiltrada;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar " + ex.ToString());
            }
        }
    }
}*/

