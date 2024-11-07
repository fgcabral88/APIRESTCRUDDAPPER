using APIRESTCRUDDAPPER.Domain.Enums;

namespace APIRESTCRUDDAPPER.Domain.Entitys
{
/// <summary>
/// Classe para criação de usuários
/// </summary>
    public class Usuario : Base
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public SituacaoType Situacao { get; set; }
        public string Senha { get; set; }
    }
}
