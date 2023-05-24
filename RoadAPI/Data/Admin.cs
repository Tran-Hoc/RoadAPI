using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RoadAPI.Data;

[Table("Admin")]
public partial class Admin
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    [StringLength(500)]
    public string? Username { get; set; }

    [Column("pass")]
    public string? Pass { get; set; }

    [Column("image")]
    public byte[]? Image { get; set; }

    [Column("name")]
    public string? Name { get; set; }
}
