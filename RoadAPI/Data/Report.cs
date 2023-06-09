﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RoadAPI.Data;

[Table("report")]
public partial class Report
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("timeSend", TypeName = "datetime")]
    public DateTime? TimeSend { get; set; }

    [Column("location")]
    public string? Location { get; set; }

    [Column("status")]
    public short? Status { get; set; }

    [Column("content")]
    public string? Content { get; set; }

    [InverseProperty("Report")]
    public virtual ICollection<ImageReport> ImageReports { get; set; } = new List<ImageReport>();
}
