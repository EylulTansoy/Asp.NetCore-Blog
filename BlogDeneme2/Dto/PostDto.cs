using System.Collections.Generic;

namespace BlogDeneme2.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string CreateTime { get; set; }
        public List<string> Tags { get; set; }
        public string YazarIsmi { get; set; }
    }
}
