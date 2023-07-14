using System.ComponentModel;

namespace ProjetoPv.Models
{
    public class Treinadores
    {
        public int Id { get; set; }
        [DisplayName("Nome do Treinador")]
        public string Nome { get; set; }
        public int Contacto { get; set; }
        public string Qualificacoes { get; set; }
        public string AspNetUsersId { get; set; }

    }
}
