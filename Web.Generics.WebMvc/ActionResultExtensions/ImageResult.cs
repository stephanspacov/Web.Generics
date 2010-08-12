using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace Web.Generics.WebMvc
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// An specialized ActionResult for handling images
    /// </summary>
    public class ImageResult : ActionResult
    {
        public ImageResult(Image image, string contentType)
        {
            this.image = image;
            this.contentType = contentType;
        }

        private Image image;
        private string contentType;

        private ImageFormat GetFormat()
        {
            switch (contentType)
            {
                case "image/jpeg":
                    return ImageFormat.Jpeg;
                case "image/gif":
                    return ImageFormat.Gif;
                case "image/bmp":
                    return ImageFormat.Bmp;
                case "image/png":
                    return ImageFormat.Png;
                default:
                    return null;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = contentType;

            image.Save(context.HttpContext.Response.OutputStream, GetFormat());
        }

    }
}
