using Microsoft.AspNetCore.Html;

namespace Coba_Net.Models
{
    public class Route
    {
        public string Label { set; get; }
        public string Path { set; get; }
        public IHtmlContent Icon { set; get; }
    }
}