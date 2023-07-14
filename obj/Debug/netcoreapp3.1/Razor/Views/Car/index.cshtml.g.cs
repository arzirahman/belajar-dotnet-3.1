#pragma checksum "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d0768836a6997c6ce8db505f498b9c786dfe5e29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_index), @"mvc.1.0.view", @"/Views/Car/index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\_ViewImports.cshtml"
using Coba_Net;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\_ViewImports.cshtml"
using Coba_Net.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0768836a6997c6ce8db505f498b9c786dfe5e29", @"/Views/Car/index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b1c68aed5da30d0592cbcb31cd02e64132a2559", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CarListView>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Car/Index"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("flex flex-1 rounded-lg bg-[#4e4f3e] text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Car/Delete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
  
    ViewData["Title"] = "Car List";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""flex flex-col gap-10 pb-10 h-full overflow-y-auto"">
    <div class=""flex w-full p-6 px-10 shadow-lg text-2xl font-bold bg-[#202020]"">
        <span>Car List:</span>
    </div>

    <div class=""px-10 w-full justify-between flex"">
        <div class=""flex w-[400px] gap-4"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0768836a6997c6ce8db505f498b9c786dfe5e295348", async() => {
                WriteLiteral("\r\n                <div class=\"flex-1\">\r\n                    <input name=\"search\"");
                BeginWriteAttribute("value", " value=\"", 543, "\"", 575, 1);
#nullable restore
#line 15 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 551, Model.Pagination.Search, 551, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" class=""bg-transparent w-full h-full p-2 pl-4 rounded-l-lg"" />
                </div>
                <div class=""flex justify-center items-center px-4"">
                    <button type=""submit"">
                        <svg xmlns=""http://www.w3.org/2000/svg"" width=""20"" height=""20"" fill=""currentColor"" class=""bi bi-search"" viewBox=""0 0 16 16"">
                            <path d=""M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z""/>
                        </svg>
                    </button>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            <a href=""/Car/Add"">
                <button class=""bg-[#104152] pr-6 flex items-center gap-2 shadow-lg p-3 hover:scale-105 transition-all duration-300 font-bold text-3xl rounded-lg"">
                    <svg xmlns=""http://www.w3.org/2000/svg"" width=""20"" height=""20"" fill=""currentColor"" class=""bi bi-plus"" viewBox=""0 0 16 16"">
                        <path d=""M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z""/>
                    </svg>
                    <span class=""text-base"">Add Car</span>
                </button>
            </a>
        </div>
    </div>

    <div class=""px-10 w-full"">
        <div class=""bg-[#1e1e1e] shadow-lg rounded-xl w-full"">
");
#nullable restore
#line 38 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
             if (Model.Cars.Count > 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""w-full"">
                    <thead>
                        <tr>
                            <th class=""p-3 text-lg"">Name</th>
                            <th class=""p-3 text-lg"">Brand</th>
                            <th class=""p-3 text-lg"">Color</th>
                            <th class=""p-3 text-lg"">Price</th>
                            <th class=""p-3 text-lg"">Action</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 51 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                         foreach (var car in Model.Cars)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td class=\"p-2 bg-[#1D3E3E] border-y-[2px] border-[#121212] text-center\">");
#nullable restore
#line 54 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                    Write(car.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td class=\"p-2 bg-[#1D3E3E] border-y-[2px] border-[#121212] text-center\">");
#nullable restore
#line 55 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                    Write(car.Brand);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td class=\"p-2 bg-[#1D3E3E] border-y-[2px] border-[#121212] text-center\">");
#nullable restore
#line 56 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                    Write(car.Color);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td class=\"p-2 bg-[#1D3E3E] border-y-[2px] border-[#121212] text-center\">");
#nullable restore
#line 57 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                    Write(car.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td class=\"p-2 bg-[#1D3E3E] border-y-[2px] border-[#121212] text-center\">\r\n                                    <div class=\"flex justify-center items-center gap-3\">\r\n                                        <a");
            BeginWriteAttribute("href", " href=\"", 3410, "\"", 3437, 2);
            WriteAttributeValue("", 3417, "/Car/Edit?Id=", 3417, 13, true);
#nullable restore
#line 60 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 3430, car.Id, 3430, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                            <button class=""p-2 rounded-md bg-[#EF8E33]"">
                                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""20"" height=""20"" fill=""currentColor"" class=""bi bi-pencil"" viewBox=""0 0 16 16"">
                                                    <path d=""M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z""/>
                                                </svg>
                                            </button>
                                        </a>
                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0768836a6997c6ce8db505f498b9c786dfe5e2913181", async() => {
                WriteLiteral("\r\n                                            <input type=\"hidden\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 4487, "\"", 4502, 1);
#nullable restore
#line 68 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 4495, car.Id, 4495, 7, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                                            <button class=""p-2 rounded-md bg-[#D72630]"">
                                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""20"" height=""20"" fill=""currentColor"" class=""bi bi-trash"" viewBox=""0 0 16 16"">
                                                    <path d=""M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5Zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6Z""/>
                                                    <path d=""M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1ZM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118ZM2.5 3h11V2h-11v1Z""/>
                                                </svg>
                                            </button>
                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 79 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n                <div class=\"flex p-4 px-6 w-full justify-between\">\r\n                    <div>Showing <strong class=\"text-[#03dac5]\">");
#nullable restore
#line 83 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                            Write(1 + " - " + Model.Cars.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> From <strong class=\"text-[#03dac5]\">");
#nullable restore
#line 83 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                                                         Write(Model.Pagination.DataCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong> Data</div>\r\n                    <div class=\"flex gap-2 items-center\">\r\n");
#nullable restore
#line 85 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                         if (Model.Pagination.HasPrevPage())
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 6095, "\"", 6216, 6);
            WriteAttributeValue("", 6102, "/Car/Index?page=", 6102, 16, true);
#nullable restore
#line 87 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6118, Model.Pagination.Page - 1, 6118, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6146, "&amp;limit=", 6146, 11, true);
#nullable restore
#line 87 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6157, Model.Pagination.Limit, 6157, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6180, "&amp;search=", 6180, 12, true);
#nullable restore
#line 87 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6192, Model.Pagination.Search, 6192, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""text-[#03dac5]"">
                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-chevron-left"" viewBox=""0 0 16 16"">
                                    <path fill-rule=""evenodd"" d=""M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z""/>
                                </svg>
                            </a>
");
#nullable restore
#line 92 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 94 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                         if (Model.Pagination.TotalPages > 6)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\'", 6817, "\'", 6911, 4);
            WriteAttributeValue("", 6824, "/Car/Index?page=1&amp;limit=", 6824, 28, true);
#nullable restore
#line 96 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6852, Model.Pagination.Limit, 6852, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 6875, "&amp;search=", 6875, 12, true);
#nullable restore
#line 96 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6887, Model.Pagination.Search, 6887, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 6912, "\'", 6989, 1);
#nullable restore
#line 96 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 6920, (Model.Pagination.IsFirstPage()) ? "font-bold text-[#03dac5]" : "", 6920, 69, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">1</a>\r\n                            <span>..</span>\r\n                            <a");
            BeginWriteAttribute("href", " href=\'", 7073, "\'", 7259, 6);
            WriteAttributeValue("", 7080, "/Car/Index?page=", 7080, 16, true);
#nullable restore
#line 98 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7096, Model.Pagination.IsCenterPage() ? Model.Pagination.Page : Model.Pagination.GetCenterPage(), 7096, 93, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7189, "&amp;limit=", 7189, 11, true);
#nullable restore
#line 98 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7200, Model.Pagination.Limit, 7200, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7223, "&amp;search=", 7223, 12, true);
#nullable restore
#line 98 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7235, Model.Pagination.Search, 7235, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 7260, "\'", 7338, 1);
#nullable restore
#line 98 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7268, (Model.Pagination.IsCenterPage()) ? "font-bold text-[#03dac5]" : "", 7268, 70, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                ");
#nullable restore
#line 99 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                            Write(Model.Pagination.IsCenterPage() ? Model.Pagination.Page : Model.Pagination.GetCenterPage());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </a>\r\n                            <span>..</span>\r\n                            <a");
            BeginWriteAttribute("href", " href=\'", 7578, "\'", 7699, 6);
            WriteAttributeValue("", 7585, "/Car/Index?page=", 7585, 16, true);
#nullable restore
#line 102 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7601, Model.Pagination.TotalPages, 7601, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7629, "&amp;limit=", 7629, 11, true);
#nullable restore
#line 102 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7640, Model.Pagination.Limit, 7640, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 7663, "&amp;search=", 7663, 12, true);
#nullable restore
#line 102 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7675, Model.Pagination.Search, 7675, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 7700, "\'", 7776, 1);
#nullable restore
#line 102 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 7708, (Model.Pagination.IsLastPage()) ? "font-bold text-[#03dac5]" : "", 7708, 68, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 102 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                                                                                                                                                 Write(Model.Pagination.TotalPages);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 103 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                        }
                        else
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                             for (int i = 1; i <= Model.Pagination.TotalPages; i++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <a");
            BeginWriteAttribute("href", " href=\"", 8046, "\"", 8141, 6);
            WriteAttributeValue("", 8053, "/Car/Index?page=", 8053, 16, true);
#nullable restore
#line 108 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8069, i, 8069, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8071, "&amp;limit=", 8071, 11, true);
#nullable restore
#line 108 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8082, Model.Pagination.Limit, 8082, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8105, "&amp;search=", 8105, 12, true);
#nullable restore
#line 108 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8117, Model.Pagination.Search, 8117, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\'", 8142, "\'", 8215, 1);
#nullable restore
#line 108 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8150, (Model.Pagination.Page == i) ? "font-bold text-[#03dac5]" : "", 8150, 65, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 108 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                                                                                                                                                                                                        Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 109 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 109 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                             
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 112 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                         if (Model.Pagination.HasNextPage())
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <a");
            BeginWriteAttribute("href", " href=\"", 8404, "\"", 8525, 6);
            WriteAttributeValue("", 8411, "/Car/Index?page=", 8411, 16, true);
#nullable restore
#line 114 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8427, Model.Pagination.Page + 1, 8427, 28, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8455, "&amp;limit=", 8455, 11, true);
#nullable restore
#line 114 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8466, Model.Pagination.Limit, 8466, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 8489, "&amp;search=", 8489, 12, true);
#nullable restore
#line 114 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
WriteAttributeValue("", 8501, Model.Pagination.Search, 8501, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""text-[#03dac5]"">
                                <svg xmlns=""http://www.w3.org/2000/svg"" width=""16"" height=""16"" fill=""currentColor"" class=""bi bi-chevron-right"" viewBox=""0 0 16 16"">
                                    <path fill-rule=""evenodd"" d=""M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z""/>
                                </svg>
                            </a>
");
#nullable restore
#line 119 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n");
#nullable restore
#line 122 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""flex flex-col items-center w-full gap-10 p-10 bg-[#1e1e1e] rounded-xl"">
                    <svg xmlns=""http://www.w3.org/2000/svg"" width=""80"" height=""80"" fill=""currentColor"" class=""bi bi-search"" viewBox=""0 0 16 16"">
                        <path d=""M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z""/>
                    </svg>
                    <span class=""text-2xl"">Data Not Found</span>
                </div>
");
#nullable restore
#line 131 "C:\Users\arzir\OneDrive\Documents\Coba Net\Views\Car\index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CarListView> Html { get; private set; }
    }
}
#pragma warning restore 1591
