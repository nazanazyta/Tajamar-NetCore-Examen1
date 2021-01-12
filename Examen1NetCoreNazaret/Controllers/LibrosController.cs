using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen1NetCoreNazaret.Data;
using Examen1NetCoreNazaret.Models;

namespace Examen1NetCoreNazaret.Controllers
{
    public class LibrosController : Controller
    {
        BibliotecaContext context;

        public LibrosController()
        {
            this.context = new BibliotecaContext();
        }

        public IActionResult Libros()
        {
            List<Libro> libros = this.context.GetLibros();
            List<Genero> generos = this.context.GetGeneros();
            ViewData["generos"] = generos;
            return View(libros);
        }

        [HttpPost]
        public IActionResult Libros(int idgenero)
        {
            List<Libro> libros = this.context.BuscarPorGenero(idgenero);
            //ViewData["librosgenero"] = libros;
            List<Genero> generos = this.context.GetGeneros();
            ViewData["generos"] = generos;
            return View(libros);
        }

        public IActionResult Detalles(int idlibro)
        {
            Libro libro = this.context.GetLibroId(idlibro);
            return View(libro);
        }

        public IActionResult Crear()
        {
            ViewData["max"] = this.context.GetMaximoIdLibro();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Libro libro)
        {
            this.context.CreateLibro(libro.IdLibro, libro.Titulo, libro.Autor, libro.Sinopsis,
                libro.Imagen, libro.IdGenero);
            return RedirectToAction("Libros");
        }

        public IActionResult Editar(int idlibro)
        {
            Libro libro = this.context.GetLibroId(idlibro);
            return View(libro);
        }

        [HttpPost]
        public IActionResult Editar(Libro libro)
        {
            this.context.UpdateLibro(libro.IdLibro, libro.Titulo, libro.Autor, libro.Sinopsis,
                libro.Imagen, libro.IdGenero);
            return RedirectToAction("Libros");
        }
    }
}
