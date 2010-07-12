using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class Tag
    {
        virtual public Int32 ID { get; set; }
        virtual public String Text { get; set; }
        virtual public DateTime CreatedAt { get; set; }
        virtual public IList<Post> Posts { get; set; }
    }
}
