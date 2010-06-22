using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Generics.CustomTypes
{
    public class ImageFile
    {
        virtual public String FilePath { get; set; }
        public ImageFile(String filePath)
        {
            this.FilePath = filePath;
        }
    }
}
