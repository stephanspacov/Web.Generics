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
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Web.Generics.Util;

namespace Web.Generics.UserInterface
{
    public class UtilController : Controller
    {
        public ActionResult Minify(String parameters)
        {
            var cache = !String.IsNullOrEmpty(Request.QueryString["cache"]);

            var url = Request.QueryString["url"];

            // READING FILES
            StringBuilder sbToStrip = new StringBuilder();
            string[] vtArquivo = url.Split(Convert.ToChar("|"));

            Encoding utf8 = Encoding.GetEncoding("utf-8");
            StreamReader srArquivo;
            DateTime lastModifiedFileGlobal = DateTime.MinValue;

            string filePath;
            DateTime fileLastModified;

            if (vtArquivo[0].Contains(".css"))
            {
                foreach (string stNomeArquivo in vtArquivo)
                {
                    Response.ContentType = "text/css";

                    var file = stNomeArquivo;
                    /*
                    // set folder and file name
                    string file;
                    string folder;
                    if (stNomeArquivo.Contains(","))
                    {
                        string[] fileFolder = stNomeArquivo.Split(',');
                        file = fileFolder[0];
                        folder = fileFolder[1];
                    }
                    else
                    {
                        file = stNomeArquivo;
                        folder = "global";
                    }

                    if (string.IsNullOrEmpty(folder))
                        continue;
                     */

                    filePath = Server.MapPath("~/assets/css/" + file);

                    fileLastModified = System.IO.File.GetLastWriteTime(filePath);
                    lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

                    try
                    {
                        srArquivo = new StreamReader(filePath, utf8);
                        sbToStrip.Append(Environment.NewLine);
                        //sbToStrip.Append("/*************** File loaded successfully: \"" + file + "\" / \"" +  + "\" ***************/");
                        sbToStrip.Append(Environment.NewLine);
                        sbToStrip.Append(Environment.NewLine);
                        sbToStrip.Append(srArquivo.ReadToEnd());
                        sbToStrip.Append(Environment.NewLine);
                        srArquivo.Close();
                    }
                    catch
                    {
                        sbToStrip.Append("/* ERROR:  Missing file " + filePath + " */");
                    }
                }
            }
            if (vtArquivo[0].Contains(".js"))
            {
                Response.ContentType = "text/javascript";
                foreach (string stNomeArquivo in vtArquivo)
                {
                    filePath = Server.MapPath("js/") + stNomeArquivo;

                    fileLastModified = System.IO.File.GetLastWriteTime(filePath);
                    lastModifiedFileGlobal = fileLastModified > lastModifiedFileGlobal ? fileLastModified : lastModifiedFileGlobal;

                    srArquivo = new StreamReader(filePath, utf8);
                    sbToStrip.Append(srArquivo.ReadToEnd());
                    sbToStrip.Append(Environment.NewLine);
                    srArquivo.Close();
                }
            }


            //DO REPLACEMENT
            string stContent = sbToStrip.ToString();

            // CSS
            if (vtArquivo[0].Contains(".css"))
            {
                // replaces
/*                stContent = stContent.Replace("$root/", Util.AssetsRoot);
                stContent = stContent.Replace("$global/", Util.GlobalPath);
                stContent = stContent.Replace("$language/", Util.LanguagePath);
                stContent = stContent.Replace("$upload-root/", Util.UploadsRoot);
                stContent = stContent.Replace("$upload-global/", Util.GlobalUploadPath);
                stContent = stContent.Replace("$upload-language/", Util.LanguageUploadPath);*/

                Dictionary<string, string> dicVariables = new Dictionary<string, string>();

                const string varRegEx = @"\$(?<varname>[^{}$]*){(?<varvalue>[^}$]*)}";
                const RegexOptions varRegExOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;

                MatchCollection variables = Regex.Matches(stContent, varRegEx, varRegExOptions); //finds all variables in the css document
                foreach (Match match in variables)
                {
                    dicVariables.Add(match.Groups["varname"].Value.Trim(), match.Groups["varvalue"].Value.Trim());  //stores it in a dictionary for a later use
                }
                stContent = Regex.Replace(stContent, varRegEx + @"[^\r\n]*[\r\n]", string.Empty, varRegExOptions); // removes all variables to clean the css

                foreach (string varname in dicVariables.Keys)
                {
                    stContent = stContent.Replace("$" + varname, dicVariables[varname]);  //replaces each variable with its value
                }

                if (Request.QueryString["v"] != null)
                {
                    stContent = Regex.Replace(stContent, "url\\((.[^\\)]*\\?.*)\\)", "url($1&v=" + Request.QueryString["v"] + ")"); // replace all images paths with a query adding the &v=version 
                    stContent = Regex.Replace(stContent, "url\\((.[^\\)\\?]*)\\)", "url($1?v=" + Request.QueryString["v"] + ")"); // replace all images paths adding the ?v=version 
                }
            }


            // CACHE
            if (cache)
            {
                DoCache(lastModifiedFileGlobal);
            }

            // GZIP ENCODE
            Gzip.GZipEncodePage(System.Web.HttpContext.Current.Request, System.Web.HttpContext.Current.Response);

            //OUTPUT
            //Response.Write("/**\n * @author " + Common.Config.AuthorName + " - " + Common.Config.AuthorAddress + "\n */\n");
            return Content(stContent);
        }

        public void DoCache(DateTime lastModifiedUnc)
        {
            string sDtModHdr = Request.Headers.Get("If-Modified-Since");
            // does header contain If-Modified-Since?
            if (!string.IsNullOrEmpty(sDtModHdr))
            {

                sDtModHdr = sDtModHdr.Split(';')[0];
                // convert to UNC date
                DateTime dtModHdrUnc = Convert.ToDateTime(sDtModHdr).ToUniversalTime();
                dtModHdrUnc = dtModHdrUnc.AddMilliseconds(dtModHdrUnc.Millisecond * -1);
                lastModifiedUnc = lastModifiedUnc.ToUniversalTime();
                lastModifiedUnc = lastModifiedUnc.AddMilliseconds(lastModifiedUnc.Millisecond * -1);

                // if it was within the last month, return 304 and exit
                if (DateTime.Compare(
                    new DateTime(
                    dtModHdrUnc.Year,
                    dtModHdrUnc.Month,
                    dtModHdrUnc.Day,
                    dtModHdrUnc.Hour,
                    dtModHdrUnc.Minute,
                    dtModHdrUnc.Second),
                    new DateTime(
                    lastModifiedUnc.Year,
                    lastModifiedUnc.Month,
                    lastModifiedUnc.Day,
                    lastModifiedUnc.Hour,
                    lastModifiedUnc.Minute,
                    lastModifiedUnc.Second)) == 0)
                {
                    Response.StatusCode = 304;
                    Response.StatusDescription = "Not Modified";
                    Response.CacheControl = "public";
                    Response.End();
                }
            }
            Response.Cache.SetLastModified(lastModifiedUnc);
            Response.CacheControl = "public";
        }
    }
}