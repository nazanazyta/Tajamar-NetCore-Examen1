using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen1NetCoreNazaret.Data;
using Examen1NetCoreNazaret.Models;

namespace Examen1NetCoreNazaret.Controllers
{
    public class GenerosController : Controller
    {
        BibliotecaContext context;

        public GenerosController()
        {
            this.context = new BibliotecaContext();
        }
        public IActionResult Generos()
        {
            List<Genero> generos = this.context.GetGeneros();
            return View(generos);
        }

        public IActionResult Crear()
        {
            ViewData["max"] = this.context.GetMaximoIdGenero();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Genero genero)
        {
            this.context.CreateGenero(genero.IdGenero, genero.Nombre);
            return RedirectToAction("Generos");
        }

        public IActionResult Borrar(int idgenero)
        {
            Genero genero = this.context.GetGeneroId(idgenero);
            return View(genero);
        }

        [HttpPost]
        public IActionResult Borrar(Genero gen)
        {
            this.context.DeleteGenero(gen.IdGenero);
            return RedirectToAction("Generos");
        }
    }
}
