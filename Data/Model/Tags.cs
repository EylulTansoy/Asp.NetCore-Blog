using Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public class Tags : BaseEntity
    {
        public string Name { get; set; }


        [ForeignKey("PostId")]
        public int? PostId { get; set; }
        public Post Post { get; set; }
    }
}
