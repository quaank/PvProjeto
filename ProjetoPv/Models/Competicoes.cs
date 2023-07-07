using System.Runtime.InteropServices;

namespace ProjetoPv.Models
{
    public class Competicoes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EquipasId { get; set; }
        public Equipas? EquipasParticipantes { get; set; }
        public DateTime Data { get; set; }
        public string Localidade { get; set; }
        public string Resultados { get; set; }  

    }
}
