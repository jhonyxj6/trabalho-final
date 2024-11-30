using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trabalho.Models;

namespace trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<ModeloLivros> livros = new List<ModeloLivros>()
        {
               new ModeloLivros{Id=1, autor ="Dom Casmurro",Titulo="Machado de Assis", quantidade=2},
                new ModeloLivros{Id=2,autor ="Memórias Póstumas de Brás Cubas",Titulo="Machado de Assis", quantidade=2},
               new ModeloLivros{Id=3,autor ="João Guimarães Rosa",Titulo="Grande Sertão: Veredas", quantidade=4},
               new ModeloLivros{Id=4,autor ="Aluísio Azevedo",Titulo="O Cortiço", quantidade=4},
               new ModeloLivros{Id=5,autor ="José de Alencar",Titulo="Iracema", quantidade=1},
               new ModeloLivros{Id=6,autor ="Mário de Andrade",Titulo="Macunaíma", quantidade=11},
               new ModeloLivros{Id=7,autor ="Jorge Amado",Titulo="Capitães da Areia", quantidade=2},
               new ModeloLivros{Id=8,autor ="Graciliano Ramos",Titulo="Vidas Secas", quantidade=9},
               new ModeloLivros{Id=9,autor ="Joaquim Manuel de Macedo",Titulo="A Moreninha", quantidade=2},
               new ModeloLivros{Id=10,autor ="Erico Verissimo",Titulo="O Tempo e o Vento", quantidade=1},
               new ModeloLivros{Id=11,autor ="Clarice Lispector",Titulo="A Hora da Estrela", quantidade=1},
               new ModeloLivros{Id=12,autor ="Rachel de Queiroz",Titulo="O Quinze", quantidade=1},
               new ModeloLivros{Id=13,autor ="José Lins do Rego",Titulo="Menino do Engenho", quantidade=5},
               new ModeloLivros{Id=14,autor ="João Guimarães Rosa",Titulo="Sagarana", quantidade=3},
               new ModeloLivros{Id=15,autor ="José Lins do Rego",Titulo="Fogo Morto", quantidade=1},





        };





        [HttpPost]
        public ActionResult<List<ModeloLivros>> AdicionarLivro(ModeloLivros novoLivros)
        {
            if (novoLivros.Id == 0 && livros.Count > 0)
                novoLivros.Id = livros[livros.Count - 1].Id + 1;

            livros.Add(novoLivros);
            return Ok(livros);

        }
        [HttpGet]
        public ActionResult<List<ModeloLivros>> ListaLivros()
        {
            return Ok(livros);

        }

       
    }   


}


