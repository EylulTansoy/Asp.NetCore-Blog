using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    public class Post:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool TaskType{ get; set; }

        [ForeignKey("AccountId")]
        public int? AccountId { get; set; }
        public Account Account{ get; set; }

        //--- ONE TO MANY 
        public ICollection<Tags> Tags { get; set; }
    }
    /*
    public enum TaskType
    {
        Waited=0,       //--- 
        Accepted=1,     //--- 
        Rejected=2      //--- 
    }*/
}
