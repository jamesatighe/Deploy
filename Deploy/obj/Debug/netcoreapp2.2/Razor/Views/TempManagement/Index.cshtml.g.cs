#pragma checksum "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "315544e9c14a8592fcc4289e6a14c8963c0102ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TempManagement_Index), @"mvc.1.0.view", @"/Views/TempManagement/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TempManagement/Index.cshtml", typeof(AspNetCore.Views_TempManagement_Index))]
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
#line 1 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\_ViewImports.cshtml"
using Deploy;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"315544e9c14a8592fcc4289e6a14c8963c0102ae", @"/Views/TempManagement/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61fc30401d5c31f8b5904ac5f2a4b15299047601", @"/Views/_ViewImports.cshtml")]
    public class Views_TempManagement_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Deploy.ViewModel.TennantDeployParams>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Download", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "TempManagement", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-type", "DeployChoices", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info btn-default"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-type", "CAPolicy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(45, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
  
    ViewData["Title"] = "TempManagement";

#line default
#line hidden
            BeginContext(97, 381, true);
            WriteLiteral(@"<h2>File Uploads</h2>
<div>
    <p>
        This page can be used to automatically update the deployment site with new template parameters and Conditional Access Policies.
    </p>
    <p>
        Download the template file. Edit to your needs then click browse, select the file and click upload.
    </p>
</div>
<p><h3> Template Links</h3></p>
<div>

    <p>
        ");
            EndContext();
            BeginContext(478, 194, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "315544e9c14a8592fcc4289e6a14c8963c0102ae5355", async() => {
                BeginContext(599, 69, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-download\"></span> Deployment Choices");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-type", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"] = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(672, 10, true);
            WriteLiteral("\r\n        ");
            EndContext();
            BeginContext(682, 182, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "315544e9c14a8592fcc4289e6a14c8963c0102ae7603", async() => {
                BeginContext(798, 62, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-download\"></span> CA Policies");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-type", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"] = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(864, 24, true);
            WriteLiteral("\r\n    </p>\r\n\r\n</div>\r\n\r\n");
            EndContext();
#line 25 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
 using (@Html.BeginForm("Index", "TempManagement", FormMethod.Post, new { enctype = "multipart/form-data" }))
{

#line default
#line hidden
            BeginContext(1002, 481, true);
            WriteLiteral(@"    <div>
        <label class=""btn btn-primary btn-lg"" for=""files"">
            <input id=""files"" name=""files"" type=""file"" multiple style=""display:none"" onchange=""$('#upload-file-info').html($(this).val());"" /> Browse
        </label>
        <span class=""label label-info"" id=""upload-file-info""></span>
    </div>
    <div style=""margin-top:10px"">
        <input type=""submit"" class=""btn btn-success btn-lg navbtn"" name=""Submit"" id=""Submit"" value=""Upload"" />
    </div>
");
            EndContext();
#line 36 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
}

#line default
#line hidden
            BeginContext(1486, 22, true);
            WriteLiteral("\r\n\r\n<div>\r\n    <p><h4>");
            EndContext();
            BeginContext(1509, 15, false);
#line 40 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
      Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(1524, 22, true);
            WriteLiteral("</h4></p>\r\n    <p><h4>");
            EndContext();
            BeginContext(1547, 20, false);
#line 41 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
      Write(ViewBag.ChoiceResult);

#line default
#line hidden
            EndContext();
            BeginContext(1567, 22, true);
            WriteLiteral("</h4></p>\r\n    <p><h4>");
            EndContext();
            BeginContext(1590, 19, false);
#line 42 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\TempManagement\Index.cshtml"
      Write(ViewBag.ParamResult);

#line default
#line hidden
            EndContext();
            BeginContext(1609, 25, true);
            WriteLiteral("</h4></p>\r\n</div>\r\n\r\n\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Deploy.ViewModel.TennantDeployParams> Html { get; private set; }
    }
}
#pragma warning restore 1591
