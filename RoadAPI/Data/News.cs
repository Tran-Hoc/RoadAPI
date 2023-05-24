using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RoadAPI.Data;

[Table("news")]
public partial class News
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

    [Column("description")]
    public string? Description { get; set; }

    public byte[]? Image { get; set; }
}
