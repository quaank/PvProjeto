using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoPv.Models
{
    public class Treino
    {
        public int Id { get; set; }
      
        public DateTime Data { get; set; }
     
        public string Localizacao { get; set; }
        public int EquipasId { get; set; }
        [DisplayName("Nome da Equipa")]
        public Equipas? Equipa { get; set; }
    }
}
