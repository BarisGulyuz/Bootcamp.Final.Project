using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.UI.TagHelpers
{
    public class StatusTagHelper : TagHelper
    {
        public bool Status { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder template = new();
            if (Status == true) 
                template.Append($"<span class='d-flex justify-content-center align-items-center badge bg-success text-white'> Aktif </span>");
            else 
                template.Append($"<span class='d-flex justify-content-center align-items-center badge bg-danger text-white'> Pasif </span>");
            output.Content.SetHtmlContent(template.ToString());
        }
    }
}
