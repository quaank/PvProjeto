using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoPv.Models
{
    public class Atletas
    {

        public int Id { get; set; }
        [DisplayName("Nome do Atleta")]
        public string Nome { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public int ModalidadesId { get; set; }
        public Modalidades? Modalidade { get; set; }
        public int? EquipasId { get; set; }
        [DisplayName("Nome da Equipa")]
        public Equipas? Equipa { get; set; }
        public string Posicao { get; set; }
        public string AspNetUsersId { get; set; }
    }
}
