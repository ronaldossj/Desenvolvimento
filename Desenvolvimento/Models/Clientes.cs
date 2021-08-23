using System.ComponentModel.DataAnnotations;

namespace Desenvolvimento.Models
{
    public class Clientes
    {
        public int ID { get; set; }
        [Display(Name = "Nome do Cliente")]
        public string nome_cliente { get; set; }
        
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Display(Name = "Ativo/Desativo")]
        public bool status { get; set; }
    }
}