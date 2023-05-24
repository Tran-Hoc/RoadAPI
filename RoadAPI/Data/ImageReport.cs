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
    public string? Image { get; set; }

    //[Column("pathToImage")]
    //public string? ImageAfterProcessing { get; set; }

    [Column("reportID")]
    public Guid? ReportId { get; set; }

    [Column("result_processing")]
    public string? ResultProcessing { get; set; }

    [ForeignKey("ReportId")]
    [InverseProperty("ImageReports")]
    public virtual Report? Report { get; set; }
}
