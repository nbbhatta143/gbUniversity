#pragma checksum "D:\C# Programming\Capstone Stuff\GroupBCapstoneProject\GroupBCapstoneProject\Views\Faculty\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12d1ded08c250c4b0480b910bc34967fb33ed281"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Faculty_Index), @"mvc.1.0.view", @"/Views/Faculty/Index.cshtml")]
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
#line 1 "D:\C# Programming\Capstone Stuff\GroupBCapstoneProject\GroupBCapstoneProject\Views\_ViewImports.cshtml"
using GroupBCapstoneProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\C# Programming\Capstone Stuff\GroupBCapstoneProject\GroupBCapstoneProject\Views\_ViewImports.cshtml"
using GroupBCapstoneProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12d1ded08c250c4b0480b910bc34967fb33ed281", @"/Views/Faculty/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50fe17463a6e178e695157ba9184f8ef4f726319", @"/Views/_ViewImports.cshtml")]
    public class Views_Faculty_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <p>Hello there, Faculty Person!</p>\r\n");
            WriteLiteral("    <p>Enrolled Classes</p>\r\n    <p>Query the database (specifically the enrollments table), make a list</p>\r\n    <p>Register to teach a class by hitting the button</p>\r\n");
#nullable restore
#line 10 "D:\C# Programming\Capstone Stuff\GroupBCapstoneProject\GroupBCapstoneProject\Views\Faculty\Index.cshtml"
     using (Html.BeginForm("Index", "Faculty", FormMethod.Post))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <input type=\"submit\" value=\"Sign up to teach a class\" class=\"btn btn-dark\" name=\"btnSubmit\" />\r\n");
#nullable restore
#line 13 "D:\C# Programming\Capstone Stuff\GroupBCapstoneProject\GroupBCapstoneProject\Views\Faculty\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591