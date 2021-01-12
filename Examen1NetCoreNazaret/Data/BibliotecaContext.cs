using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Examen1NetCoreNazaret.Models;

#region PROCEDIMIENTOS ALMACENADOS
//CREATE PROCEDURE insertarlibro (@idlibro int, @titulo nvarchar(30),
//@autor nvarchar(30), @sinopsis nvarchar(30), @imagen nvarchar(30), @idgenero int)
//AS
//    insert into libros values (@idlibro, @titulo,
//    @autor, @sinopsis, @imagen, @idgenero)
//GO

//CREATE PROCEDURE modificarlibro (@idlibro int, @titulo nvarchar(30),
//@autor nvarchar(30), @sinopsis nvarchar(30), @imagen nvarchar(30), @idgenero int)
//AS
//    update libros set titulo=@titulo, autor = @autor,
//    sinopsis = @sinopsis, imagen = @imagen, idgenero = @idgenero
//	where idlibro=@idlibro
//GO

//CREATE PROCEDURE borrargenero (@idgenero int)
//AS
//    delete from generos where idgenero=@idgenero
//GO
#endregion

namespace Examen1NetCoreNazaret.Data
{
    public class BibliotecaContext
    {
        SqlDataAdapter adlib;
        SqlDataAdapter adgen;
        DataTable tablalib;
        DataTable tablagen;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public BibliotecaContext()
        {
            String cadena = "Data Source=localhost;Initial Catalog=BIBLIOTECA;User ID=sa;Password=MCSD2020";
            this.adlib = new SqlDataAdapter("select * from libros", cadena);
            this.tablalib = new DataTable();
            this.adlib.Fill(this.tablalib);
            this.adgen = new SqlDataAdapter("select * from generos", cadena);
            this.tablagen = new DataTable();
            this.adgen.Fill(this.tablagen);
            this.cn = new SqlConnection(cadena);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Libro> GetLibros()
        {
            var query = from datos in this.tablalib.AsEnumerable()
                        select datos;
            List<Libro> libros = new List<Libro>();
            foreach (var row in query)
            {
                Libro libro = new Libro();
                libro.IdLibro = row.Field<int>("idlibro");
                libro.Titulo = row.Field<String>("titulo");
                libro.Autor = row.Field<String>("autor");
                libro.Sinopsis = row.Field<String>("sinopsis");
                libro.Imagen = row.Field<String>("imagen");
                libro.IdGenero = row.Field<int>("idgenero");
                libros.Add(libro);
            }
            return libros;
        }

        public Libro GetLibroId(int idlibro)
        {
            var query = from datos in this.tablalib.AsEnumerable()
                        where datos.Field<int>("idlibro") == idlibro
                        select datos;
            var row = query.First();
            Libro libro = new Libro();
            libro.IdLibro = row.Field<int>("idlibro");
            libro.Titulo = row.Field<String>("titulo");
            libro.Autor = row.Field<String>("autor");
            libro.Sinopsis = row.Field<String>("sinopsis");
            libro.Imagen = row.Field<String>("imagen");
            libro.IdGenero = row.Field<int>("idgenero");
            return libro;
        }

        public int GetMaximoIdLibro()
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "select max(idlibro)+1 as maximolibro from libros";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            int idMax = Convert.ToInt32(this.reader["maximolibro"]);
            this.reader.Close();
            this.cn.Close();
            return idMax;
        }

        public void CreateLibro(int idlibro, String titulo, String autor, String sinopsis,
            String imagen, int idgenero)
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "insertarlibro";
            this.com.Parameters.AddWithValue("@idlibro", idlibro);
            this.com.Parameters.AddWithValue("@titulo", titulo);
            this.com.Parameters.AddWithValue("@autor", autor);
            this.com.Parameters.AddWithValue("@sinopsis", sinopsis);
            this.com.Parameters.AddWithValue("@imagen", imagen);
            this.com.Parameters.AddWithValue("@idgenero", idgenero);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
            //this.com.CommandType = CommandType.Text;
            //this.com.CommandText = "insert into libros values (@idlibro, @titulo, @autor," +
            //    " @sinopsis, @imagen, @idgenero)";
            //this.com.Parameters.AddWithValue("@idlibro", idlibro);
            //this.com.Parameters.AddWithValue("@titulo", titulo);
            //this.com.Parameters.AddWithValue("@autor", autor);
            //this.com.Parameters.AddWithValue("@sinopsis", sinopsis);
            //this.com.Parameters.AddWithValue("@imagen", imagen);
            //this.com.Parameters.AddWithValue("@idgenero", idgenero);
            //this.cn.Open();
            //this.com.ExecuteNonQuery();
            //this.cn.Close();
            //this.com.Parameters.Clear();
        }

        public void UpdateLibro(int idlibro, String titulo, String autor, String sinopsis,
            String imagen, int idgenero)
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "modificarlibro";
            this.com.Parameters.AddWithValue("@idlibro", idlibro);
            this.com.Parameters.AddWithValue("@titulo", titulo);
            this.com.Parameters.AddWithValue("@autor", autor);
            this.com.Parameters.AddWithValue("@sinopsis", sinopsis);
            this.com.Parameters.AddWithValue("@imagen", imagen);
            this.com.Parameters.AddWithValue("@idgenero", idgenero);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

            //this.com.CommandType = CommandType.Text;
            //this.com.CommandText = "update libros set titulo=@titulo," +
            //    " autor=@autor, sinopsis=@sinopsis, imagen=@imagen, idgenero=@idgenero" +
            //    " where idlibro=@idlibro";
            //this.com.Parameters.AddWithValue("@idlibro", idlibro);
            //this.com.Parameters.AddWithValue("@titulo", titulo);
            //this.com.Parameters.AddWithValue("@autor", autor);
            //this.com.Parameters.AddWithValue("@sinopsis", sinopsis);
            //this.com.Parameters.AddWithValue("@imagen", imagen);
            //this.com.Parameters.AddWithValue("@idgenero", idgenero);
            //this.cn.Open();
            //this.com.ExecuteNonQuery();
            //this.cn.Close();
            //this.com.Parameters.Clear();
        }

        public List<Genero> GetGeneros()
        {
            var query = from datos in this.tablagen.AsEnumerable()
                        select datos;
            List<Genero> generos = new List<Genero>();
            foreach (var row in query)
            {
                Genero genero = new Genero();
                genero.IdGenero = row.Field<int>("idgenero");
                genero.Nombre = row.Field<String>("genero");
                generos.Add(genero);
            }
            return generos;
        }

        public int GetMaximoIdGenero()
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "select max(idgenero)+1 as maximogenero from generos";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            this.reader.Read();
            int idMax = Convert.ToInt32(this.reader["maximogenero"]);
            this.reader.Close();
            this.cn.Close();
            return idMax;
        }

        public void CreateGenero(int idgenero, String nombre)
        {
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "insert into generos values (@idgenero, @nombre)";
            this.com.Parameters.AddWithValue("@idgenero", idgenero);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public Genero GetGeneroId(int idgenero)
        {
            var query = from datos in this.tablagen.AsEnumerable()
                        where datos.Field<int>("idgenero") == idgenero
                        select datos;
            var row = query.First();
            Genero genero = new Genero();
            genero.IdGenero = row.Field<int>("idgenero");
            genero.Nombre = row.Field<String>("genero");
            return genero;
        }

        public void DeleteGenero(int idgenero)
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "borrargenero";
            this.com.Parameters.AddWithValue("@idgenero", idgenero);
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

            //this.com.CommandType = CommandType.Text;
            //this.com.CommandText = "delete from generos where idgenero=@idgenero";
            //this.com.Parameters.AddWithValue("@idgenero", idgenero);
            //this.cn.Open();
            //this.com.ExecuteNonQuery();
            //this.cn.Close();
            //this.com.Parameters.Clear();
        }

        public List<Libro> BuscarPorGenero(int idgenero)
        {
            var query = from datos in this.tablalib.AsEnumerable()
                        where datos.Field<int>("idgenero") == idgenero
                        select datos;
            List<Libro> libros = new List<Libro>();
            foreach(var row in query)
            {
                Libro libro = new Libro();
                libro.IdLibro = row.Field<int>("idlibro");
                libro.Titulo = row.Field<String>("titulo");
                libro.Autor = row.Field<String>("autor");
                libro.Sinopsis = row.Field<String>("sinopsis");
                libro.Imagen = row.Field<String>("imagen");
                libro.IdGenero = row.Field<int>("idgenero");
                libros.Add(libro);
            }
            return libros;
        }
    }
}
