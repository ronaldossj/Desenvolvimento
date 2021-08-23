using System.ComponentModel.DataAnnotations;

namespace Desenvolvimento.Models
{
    public class Imoveis
    {
        public int ID { get; set; }

        [Display(Name = "Tipo do Negócio")]
        public string tipo_negocio { get; set; }

        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "CPF Cliente")]
        public string cpf_cliente { get; set; }
        
        [Display(Name = "Nome do Cliente")]
        public string nome_cliente { get; set; } 
        
        [Display(Name = "Id do Cliente")]
        public string id_cliente { get; set; }

        [Display(Name = "Valor")]
        public decimal valor { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }
    }
}