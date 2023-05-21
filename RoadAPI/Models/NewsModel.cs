using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoadAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadAPI.Models
{
    public class NewsModel
    {
        //public Guid Id { get; set; }
        [StringLength(250)]
        public string? Author { get; set; }
        public string? Title { get; set; }
        public IFormFile? Image { get; set; }
        //public DateTime? PublishedAt { get; set; }
        public string? Content { get; set; }
    }

    public class NewsViewModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("author")]
        [StringLength(250)]
        public string? Author { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("pathToImage")]
        public string? PathToImage { get; set; }

        [Column("publishedAt", TypeName = "datetime")]
        public DateTime? PublishedAt { get; set; }

        [Column("content")]
        public string? Content { get; set; }

    }

}
