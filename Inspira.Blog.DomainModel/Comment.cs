using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class Comment
    {
        virtual public Int32 ID { get; set; }
        virtual public String Title { get; set; }
        virtual public String Text { get; set; }
        virtual public String AuthorName { get; set; }
        virtual public String AuthorEmail { get; set; }
        virtual public String AuthorUrl { get; set; }
        virtual public DateTime CreatedAt { get; set; }
        virtual public Boolean IsApproved { get; set; }
        virtual public DateTime? ApprovedAt { get; set; }
        virtual public Post Post { get; set; }
    }
}
