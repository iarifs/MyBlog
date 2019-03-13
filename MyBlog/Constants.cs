using System;
using System.Collections.Generic;
using System.Web;

namespace MyBlog
{
    public static class Constants
    {
        public static readonly List<string> AllowedFileExtension = new List<string> { ".jpg", ".jpeg", ".png" };

        public static readonly string UploadFolder = "~/Upload/";

        public static readonly string MappedUploadFolder = HttpContext.Current.Server.MapPath(UploadFolder);
    }

}