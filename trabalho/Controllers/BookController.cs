
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
               new ModeloLivros{Id=1, autor ="Dom Casmurro",Titulo="Machado de Assis", quantidade=2, ano = 1899},
                new ModeloLivros{Id=2,autor ="Memórias Póstumas de Brás Cubas",Titulo="Machado de Assis", quantidade=2, ano= 1881},
               new ModeloLivros{Id=3,autor ="João Guimarães Rosa",Titulo="Grande Sertão: Veredas", quantidade=4,ano= 1956},
               new ModeloLivros{Id=4,autor ="Aluísio Azevedo",Titulo="O Cortiço", quantidade=4, ano= 1890},
               new ModeloLivros{Id=5,autor ="José de Alencar",Titulo="Iracema", quantidade=1, ano =1865},
               new ModeloLivros{Id=6,autor ="Mário de Andrade",Titulo="Macunaíma", quantidade=11, ano=1928},
               new ModeloLivros{Id=7,autor ="Jorge Amado",Titulo="Capitães da Areia", quantidade=2, ano = 1937},
               new ModeloLivros{Id=8,autor ="Graciliano Ramos",Titulo="Vidas Secas", quantidade=9, ano=1938},
               new ModeloLivros{Id=9,autor ="Joaquim Manuel de Macedo",Titulo="A Moreninha", quantidade=2, ano=1844},
               new ModeloLivros{Id=10,autor ="Erico Verissimo",Titulo="O Tempo e o Vento", quantidade=1, ano =1949},
               new ModeloLivros{Id=11,autor ="Clarice Lispector",Titulo="A Hora da Estrela", quantidade=1, ano=1977},
               new ModeloLivros{Id=12,autor ="Rachel de Queiroz",Titulo="O Quinze", quantidade=1, ano=1930},
               new ModeloLivros{Id=13,autor ="José Lins do Rego",Titulo="Menino do Engenho", quantidade=5, ano= 1932},
               new ModeloLivros{Id=14,autor ="João Guimarães Rosa",Titulo="Sagarana", quantidade=3, ano =1946},
               new ModeloLivros{Id=15,autor ="José Lins do Rego",Titulo="Fogo Morto", quantidade=1, ano = 1943},

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


        [HttpPost("alugar/{Titulo}")]
        public ActionResult AlugarPorTitulo(string Titulo)
        {

            var encontarlivro = livros.Find(l => l.Titulo == Titulo);

            if (encontarlivro == null)
            {
                return NotFound("livro não encontrado");
            }
            if (encontarlivro.alugar == true)
            {
                return NotFound("Este livro já está alugado");

            }
            if (encontarlivro.alugar = true) ;

            {
                return Ok($"{encontarlivro.Titulo} foi alugado com sucesso!!!! ");

            }





        }

        [HttpPost("devolver/{titulo}")]
        public ActionResult DevolverLivros(string titulo)
        {
            var devolverlivri = livros.Find(l => l.Titulo == titulo);

            if (devolverlivri == null)
            {
                return NotFound("não econtado");
            }

            if (devolverlivri.alugar == false)
            {
                return NotFound("livro não alugadod");
            }
            devolverlivri.alugar = false;

            devolverlivri.quantidade++;

            return Ok($"Livro '{devolverlivri.Titulo}' devolvido com sucesso.");
        }


        [HttpGet("alugados")]
        public ActionResult<List<ModeloLivros>> LivrosAlugados()
        {
            var encontraalugado = livros.FindAll(l => l.alugar);


            if (encontraalugado.Count == 0)
            {
                return NotFound("Não há livros alugados agora ");
            }
            return Ok(encontraalugado);




        }











    }





}
