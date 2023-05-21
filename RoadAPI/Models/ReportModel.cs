using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoadAPI.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadAPI.Models
{
    public class ReportModel
    {
        [Column("timeSend", TypeName = "datetime")]
        public DateTime? TimeSend { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("status")]
        public short? Status { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        public List<IFormFile> Image { get; set; }
    }
    public class ReportViewModel
    {
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
    public class ReportUpdateModel
    {
        //[Column("id")]
        //public Guid Id { get; set; }

        [Column("status")]
        public short? Status { get; set; }

        [Column("content")]
        public string? Content { get; set; }
    }
}
