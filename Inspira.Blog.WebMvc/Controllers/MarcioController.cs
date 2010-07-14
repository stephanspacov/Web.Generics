using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Models;
using Inspira.Blog.WebMvc.ViewModels;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class MarcioController : Controller
    {
        public ActionResult Melo()
        {
            return View();
        }

        public ActionResult Silva()
        {
            return View("Melo");
        }

        public ActionResult Indeciso(String sobrenome, Int32 idade)
        {
            if (sobrenome == "Melo")
            {
                var melo = new Pessoa { Idade = idade, Nome = "marcio melo" };

                var viewModel = new MeloViewModel();
                viewModel.Nome = melo.Nome;
                viewModel.Anos = melo.Idade.ToString();
                viewModel.VeioDoFuturo = melo.VeioDoFuturo;

                ViewData["view_model"] = viewModel;
                return View("Melo", viewModel);
            }
            else if (sobrenome == "Silva")
            {
                return View("Silva");
            }
            else
            {
                return Content("Não conheço esse sobrenome, mas a idade é: " + idade);
            }
        }

        public ActionResult Ide(Pessoa pessoa, Post post)
        {
            return Content("Nome: " + pessoa.Nome + " - Idade: " + pessoa.Idade + " - Veio do futuro: " + pessoa.VeioDoFuturo);
        }
    }
}
