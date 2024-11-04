namespace APIRESTCRUDDAPPER.Models
{
    public class ResponseModel<Entity>
    {
        public Entity? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}
