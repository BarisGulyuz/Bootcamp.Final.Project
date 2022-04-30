using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.UI.TagHelpers
{
    public class ActionButtons : TagHelper
    {
        public bool Status { get; set; }
        public string ActivateButtonUrl { get; set; }
        public bool IsDeleteActive { get; set; } = true;
        public string DeleteButtonUrl { get; set; }
        public bool IsEditActive { get; set; } = true;
        public string EditButtonUrl { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder template = new();
            if (IsDeleteActive) template.Append($"<a class='btn btn-danger btn-sm mr-2' href='{DeleteButtonUrl}'> Sil </a>");
            if (IsEditActive) template.Append($"<a class='btn btn-warning btn-sm mr-2' href='{EditButtonUrl}'> Düzenle </a>");
            if (Status == false) template.Append($"<a class='btn btn-success btn-sm' href='{ActivateButtonUrl}'> Aktif Et </a>");

            output.Content.SetHtmlContent(template.ToString());

        }
    }
}
