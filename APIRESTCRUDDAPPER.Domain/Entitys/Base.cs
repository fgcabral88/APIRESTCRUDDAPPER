using System.ComponentModel.DataAnnotations;

namespace APIRESTCRUDDAPPER.Domain.Entitys
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
