
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trabalho.Models;
using trabalho.Requests;

namespace trabalho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<ModeloLivros> livros = new List<ModeloLivros>()
        {
            new ModeloLivros{Id=1, Autor ="Dom Casmurro",Titulo="Machado de Assis", quantidade=2, ano = 1899},
            new ModeloLivros{Id=2,Autor ="Memórias Póstumas de Brás Cubas",Titulo="Machado de Assis", quantidade=2, ano= 1881},
            new ModeloLivros{Id=3,Autor ="João Guimarães Rosa",Titulo="Grande Sertão: Veredas", quantidade=4,ano= 1956},
            new ModeloLivros{Id=4,Autor ="Aluísio Azevedo",Titulo="O Cortiço", quantidade=4, ano= 1890},
            new ModeloLivros{Id=5,Autor ="José de Alencar",Titulo="Iracema", quantidade=1, ano =1865},
            new ModeloLivros{Id=6,Autor ="Mário de Andrade",Titulo="Macunaíma", quantidade=11, ano=1928},
            new ModeloLivros{Id=7,Autor ="Jorge Amado",Titulo="Capitães da Areia", quantidade=2, ano = 1937},
            new ModeloLivros{Id=8,Autor ="Graciliano Ramos",Titulo="Vidas Secas", quantidade=9, ano=1938},
            new ModeloLivros{Id=9,Autor ="Joaquim Manuel de Macedo",Titulo="A Moreninha", quantidade=2, ano=1844},
            new ModeloLivros{Id=10,Autor ="Erico Verissimo",Titulo="O Tempo e o Vento", quantidade=1, ano =1949},
            new ModeloLivros{Id=11,Autor ="Clarice Lispector",Titulo="A Hora da Estrela", quantidade=1, ano=1977},
            new ModeloLivros{Id=12,Autor ="Rachel de Queiroz",Titulo="O Quinze", quantidade=1, ano=1930},
            new ModeloLivros{Id=13,Autor ="José Lins do Rego",Titulo="Menino do Engenho", quantidade=5, ano= 1932},
            new ModeloLivros{Id=14,Autor ="João Guimarães Rosa",Titulo="Sagarana", quantidade=3, ano =1946},
            new ModeloLivros{Id=15,Autor ="José Lins do Rego",Titulo="Fogo Morto", quantidade=1, ano = 1943},
        };
        private static List<Aluguel> alugueis = new List<Aluguel>() { };


        [HttpGet]
        public ActionResult<List<ModeloLivros>> ListaLivros()
        {
            return Ok(livros);
        }


        [HttpPost("alugar/{livroId}")]
        public ActionResult AlugarLivro(int livroId, [FromBody] AluguelRequest request)
        {

            var encontrarLivro = livros.Find(l => l.Id == livroId);
     
            if (encontrarLivro == null)
            {
                return NotFound("livro não encontrado");
            }

            if (encontrarLivro.quantidade <= 0)
            {
                return BadRequest("Não há exemplares disponíveis para aluguel no momento.");
            }

            var alugado = alugueis.Find(a => a.livroId == livroId);

            if (alugado != null) {
                if (alugado.Nome == request.Nome)
                {
                    return BadRequest($"O usuário {request.Nome} já alugou esse livro.");
                }
            }
            var novoAluguel = new Aluguel { };

            novoAluguel.Id = alugueis.Count > 0 ? alugueis[alugueis.Count - 1].Id + 1 : 1;
            novoAluguel.Nome = request.Nome;
            novoAluguel.AnoNascimento = request.AnoNascimento;
            novoAluguel.livroId = livroId;
            novoAluguel.criado_em = DateTime.Now;
            novoAluguel.devolvido_em = null;
            
            alugueis.Add(novoAluguel);

            encontrarLivro.quantidade--;

            return Created();
        }

        [HttpPost("devolver/{aluguelId}")]
        public ActionResult DevolverLivros(int aluguelId)
        {
            var devolverLivro = alugueis.Find(l => l.Id == aluguelId);

            if (devolverLivro == null)
            {
                return NotFound("Aluguel não encontrado.");
            }

            if (devolverLivro.devolvido_em != null)
            {
                return BadRequest("Livro já devolvido.");
            }

            var livroDevolucao = livros.Find(r => r.Id == devolverLivro.livroId);

            if (livroDevolucao == null)
            {
                return NotFound("Livro não encontrado.");
            }

            livroDevolucao.quantidade++;
            devolverLivro.devolvido_em = DateTime.Now;

            return Created();
        }


        [HttpGet("alugados")]
        public ActionResult<List<object>> LivrosAlugados()
        {
            var alugueisComDetalhes = alugueis.Select(aluguel =>
            {
                var livro = livros.FirstOrDefault(l => l.Id == aluguel.livroId);
                if (livro != null)
                {
                    return new
                    {
                        aluguel.Id,
                        aluguel.Nome,
                        aluguel.AnoNascimento,
                        LivroTitulo = livro.Titulo,
                        LivroAno = livro.ano,
                        aluguel.criado_em,
                        aluguel.devolvido_em
                    };
                }
                return null;
            }).Where(a => a != null)
            .OrderBy(a => a.devolvido_em)
            .ToList();

            return Ok(alugueisComDetalhes);
        }
    }
}
