using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ProjetoPv.Models
{
    public class Equipas
    {
        public int Id { get; set; }
        [DisplayName("Nome da Equipa")]
        public string Nome { get; set; }
        public int ModalidadesId { get; set; }
        public Modalidades? Modalidade { get; set; }
        public int Categoria { get; set; }
        public int TreinadoresId { get; set; }
        [DisplayName("Nome do Treinador")]  
        public Treinadores? Treinador { get; set; }

    }
}
    