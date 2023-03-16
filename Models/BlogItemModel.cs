using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogbackend.Models
{
    public class BlogItemModel
    {
        public int id { get; set; }

        public string userID { get; set; }

        public string PublishedName { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }

        public string isPublished { get; set; }

        public string isDeleted { get; set; }

        public BlogItemModel() {}
    }
}