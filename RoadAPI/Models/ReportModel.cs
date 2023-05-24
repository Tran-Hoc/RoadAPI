using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoadAPI.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoadAPI.Models
{
    public class ReportModel
    {
        //[Column("timeSend", TypeName = "datetime")]
        //public DateTime? TimeSend { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("status")]
        public short? Status { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        public IFormFile? Image { get; set; }
        [Column("id_account")]
        public Guid? IdAccount { get; set; }
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

        [Column("image")]
        public string? Image { get; set; }

        [Column("id_account")]
        public Guid? IdAccount { get; set; }


    }
    public class ReportUpdateModel
    {
        //[Column("id")]
        //public Guid Id { get; set; }

        [Column("status")]
        public short? Status { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        public IFormFile? Image { get; set; }
    }
}
