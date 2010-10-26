/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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
