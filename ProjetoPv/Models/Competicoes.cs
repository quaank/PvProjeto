using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ProjetoPv.Models
{
    public class Competicoes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EquipasId { get; set; }
        [DisplayName("Confronto")]
        public Equipas? Equipa { get; set; }
        public string EquipaOponente { get; set; }  
        public DateTime Data { get; set; }
        public string Localidade { get; set; }
        public string? Resultados { get; set; }  

    }
}
