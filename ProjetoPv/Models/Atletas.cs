using System.ComponentModel;

namespace ProjetoPv.Models
{
    public class Atletas
    {
        public int Id { get; set; }
        [DisplayName("Nome do Atleta")]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int ModalidadesId { get; set; }
        public Modalidades? Modalidade { get; set; }
        public int EquipasId { get; set; }
        public Equipas? Equipa { get; set; }
        public string Posicao { get; set; }
    }
}
