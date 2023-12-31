﻿using HtmlTableHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibsTest
{
    public class Html
    {
        public static string GetHtml()
        {
            var data = new List<object>();
            var html = data.ToHtmlTable(
                tableAttributes:
                new { @class = "table table-striped" } //this is dynamic type, support all attribute 
                , trAttributes:
                new { ID = "table-to-pdf" }, tdAttributes: new { width = "120 px" },
                thAttributes: new { @class = "dark-theme" }
            );
            return html ?? string.Empty;
        }
    }
}
