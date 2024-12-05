using System.Data;
using System.IO.Pipes;
using System.Text.Json.Serialization.Metadata;

namespace trabalho.Models
{
    public class ModeloLivros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string autor { get; set; }

        public int quantidade { get; set; }

        public bool alugar { get; set; }

        public int ano { get; set; }


    }
}




