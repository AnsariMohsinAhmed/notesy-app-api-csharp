using System.ComponentModel.DataAnnotations.Schema;

namespace notesy_api_c_sharp.Models
{
    public class Note
    {
        public int ID { get; set; }
        [Column("Note")]
        public string? Text { get; set; }
    }
}
