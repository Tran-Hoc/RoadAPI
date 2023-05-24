using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoadAPI.Models
{
    public class AccountModel
    {
        //[Column("id")]
        //public Guid? Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("pass")]
        public string? Pass { get; set; }

        [Column("Image")]
        public IFormFile? Image { get; set; }

        [Column("name")]
        [StringLength(500)]
        public string? Name { get; set; }
    }
    public class AccountViewModel
    {
        [Column("id")]
        public Guid? Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("pass")]
        public string? Pass { get; set; }

        [Column("NameImage")]
        public string? NameImage { get; set; }

        [Column("name")]
        [StringLength(500)]
        public string? Name { get; set; }
    }
}
