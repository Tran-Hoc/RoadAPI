using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RoadAPI.Data;

[Table("account")]
public partial class Account
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("pass")]
    public string? Pass { get; set; }

    [Column("pathToImage")]
    public string? PathToImage { get; set; }

    [Column("name")]
    [StringLength(500)]
    public string? Name { get; set; }

    [InverseProperty("IdAccountNavigation")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
