using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RoadAPI.Models
{
    public class ImageReportModel
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("pathToImage")]
        public IFormFile? Image { get; set; }

        [Column("reportID")]
        public Guid? ReportId { get; set; }

        [Column("result_processing")]
        public string? ResultProcessing { get; set; }

    }
}
