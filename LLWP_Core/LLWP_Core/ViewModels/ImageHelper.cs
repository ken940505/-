using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LLWP_Core.ViewModels
{
    public static class ImageHelper
    {
        public static IHtmlContent Image(this IHtmlHelper helper, string src, string width)
        {
            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", src);

            builder.MergeAttribute("width", width);

            return new HtmlString(builder.ToString());
        }
    }
}
