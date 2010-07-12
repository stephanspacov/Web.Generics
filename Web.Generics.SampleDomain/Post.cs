using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class Post
    {
        public Post()
        {
            this.Tags = new List<Tag>();
            this.Comments = new List<Comment>();
        }

        virtual public Int32 ID { get; set; }
        virtual public String Title { get; set; }
        virtual public String Text { get; set; }
        virtual public DateTime CreatedAt { get; set; }
        virtual public Boolean IsPublished { get; set; }
        virtual public DateTime? PublishedAt { get; set; }
        virtual public DateTime LastUpdatedAt { get; set; }

        virtual public WebLog WebLog { get; set; }
        virtual public IList<Tag> Tags { get; set; }
        virtual public IList<Comment> Comments { get; set; }
    }
}
