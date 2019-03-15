using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyBlog.Models.Domain
{
    public static class SlugGenerator
    {

        public static string GenerateSlug(string title)
        {
            title = title.Trim(' ', '#', '@', '<').ToLower();
            return Regex.Replace(title, @"[^A-Za-z0-9_]+", "-");
        }
    }
}