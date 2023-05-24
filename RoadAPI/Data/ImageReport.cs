using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RoadAPI.Data;

[Table("image_report")]
public partial class ImageReport
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("pathToImage")]
    public string? PathToImage { get; set; }

    [Column("reportID")]
    public Guid? ReportId { get; set; }

    [Column("result_processing")]
    public string? ResultProcessing { get; set; }

    public byte[]? Image { get; set; }

    public byte[]? ImageAfter { get; set; }
}
