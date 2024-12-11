namespace trabalho.Models
{
    public class Aluguel
    {
        public int Id { get; set; }
        
        public string Nome {  get; set; } = string.Empty;
        
        public int AnoNascimento { get; set; }

        public int livroId { get; set; }

        public DateTime criado_em {  get; set; }
        public DateTime? devolvido_em { get; set; }

    }
}
