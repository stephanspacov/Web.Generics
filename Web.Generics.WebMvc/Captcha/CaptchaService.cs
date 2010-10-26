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
using System.Web;
using System.Drawing;

namespace Web.Generics.ApplicationServices
{
    // Raphael Cruzeiro 2010-08-12
    /// <summary>
    /// A CAPTCHA (Completely Automated Public Turing test to tell Computers and Humans Apart) Service
    /// </summary>
    public class CaptchaService
    {
        private Web.Captcha.Captcha captcha;

        public CaptchaService(int width, int height, int length)
        {
            captcha = new Web.Captcha.Captcha(width, height, length);
        }

        public CaptchaService(String text, int width, int height)
        {
            captcha = new Web.Captcha.Captcha(text, width, height);
        }

        public CaptchaService(int length)
        {
            captcha = new Web.Captcha.Captcha(5);
        }

        /// <summary>
        /// Generates a new CAPTCHA valid for 3 minutes
        /// </summary>
        /// <returns>The CAPTCHA image</returns>
        public Image Generate()
        {
            captcha.Draw();
            HttpContext.Current.Session["Captcha_ValidUntil"] = DateTime.Now.AddMinutes(3);
            HttpContext.Current.Session["Captcha_Code"] = captcha.Text;
            return captcha.Image;
        }

        /// <summary>
        /// Validates the CAPTCHA
        /// </summary>
        /// <param name="code">The code entered by the user</param>
        /// <returns>The result of the validation</returns>
        public Web.Captcha.CaptchaResult Validate(string code)
        {
            if (Convert.ToDateTime(HttpContext.Current.Session["Captcha_ValidUntil"]) > DateTime.Now)
                return Web.Captcha.CaptchaResult.Invalid;

            if (code == (string)HttpContext.Current.Session["Captcha_Code"])
                return Web.Captcha.CaptchaResult.Validated;

            return Web.Captcha.CaptchaResult.WrongCode;
        }
    }
}
