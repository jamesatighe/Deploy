#pragma checksum "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1eb4b835632a0c6263eb3afc7e5ca795c9371dea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DeployTypes_IndexSelectedold), @"mvc.1.0.view", @"/Views/DeployTypes/IndexSelectedold.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DeployTypes/IndexSelectedold.cshtml", typeof(AspNetCore.Views_DeployTypes_IndexSelectedold))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1eb4b835632a0c6263eb3afc7e5ca795c9371dea", @"/Views/DeployTypes/IndexSelectedold.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61fc30401d5c31f8b5904ac5f2a4b15299047601", @"/Views/_ViewImports.cshtml")]
    public class Views_DeployTypes_IndexSelectedold : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Deploy.ViewModel.TenantDeploys>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateNew", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-lg navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Queue", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-lg navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreateRedirect", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("GetDeploy"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetDeployAll", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "TennantParams", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info btn-lg navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SaveToAzure", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-default navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(39, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
  
    ViewData["Title"] = "IndexSelected";

#line default
#line hidden
            BeginContext(90, 901, true);
            WriteLiteral(@"
<div id=""DeleteWarning"" class=""modal"" tabindex=""1"" style=""display:none;"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div style=""font-size:18px;"">
                <i class=""fa fa-2x fa-exclamation-triangle"" aria-hidden=""true""></i><span style=""font-size:32px""><b>  Warning</b></span>
                <p style=""margin-top:15px"">Are you sure you wish to delete this Deployment?</p>
                <p>Please confirm you choice, or cancel to return.</p>
                <button id=""DeleteWarningClose"" class=""btn btn-success btn-lg"" data-dismiss=""modal"" aria-label=""Close"">Close</button>
                <button id=""DeleteWarningOK"" class=""btn btn-danger btn-lg"">Delete</button>
            </div>
        </div>
    </div>
</div>

<h2>Deployment List</h2>
<p>
    You are currently working in company: <span style=""font-weight:bold;font-size:20px;"" >");
            EndContext();
            BeginContext(992, 43, false);
#line 23 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                     Write(Html.DisplayFor(Model => Model.TennantName));

#line default
#line hidden
            EndContext();
            BeginContext(1035, 30, true);
            WriteLiteral("</span>\r\n</p>\r\n\r\n\r\n\r\n<p>\r\n    ");
            EndContext();
            BeginContext(1065, 111, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1eb4b835632a0c6263eb3afc7e5ca795c9371dea8822", async() => {
                BeginContext(1162, 10, true);
                WriteLiteral("Create New");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 29 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                 WriteLiteral(Model.TennantID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1176, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(1182, 129, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1eb4b835632a0c6263eb3afc7e5ca795c9371dea11255", async() => {
                BeginContext(1297, 10, true);
                WriteLiteral("View Queue");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 30 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                   WriteLiteral(Model.TennantID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1311, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(1317, 133, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1eb4b835632a0c6263eb3afc7e5ca795c9371dea13913", async() => {
                BeginContext(1386, 60, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-triangle-left\"></span> Back");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1450, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 33 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
      
        var deployCount = 0;

        for (var i = 0; i < Model.DeployTypes.Count(); i++)
        {
            if (Model.DeployTypes[i].DeployState != null)
            {
                deployCount++;
            }
        }
        if (deployCount > 0)
        {

#line default
#line hidden
            BeginContext(1739, 12, true);
            WriteLiteral("            ");
            EndContext();
            BeginContext(1751, 199, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1eb4b835632a0c6263eb3afc7e5ca795c9371dea15962", async() => {
                BeginContext(1893, 53, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-check\"></span> Check");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 45 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                         WriteLiteral(Model.TennantID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1950, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 46 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
        }
     

#line default
#line hidden
            BeginContext(1971, 6, true);
            WriteLiteral("</p>\r\n");
            EndContext();
#line 49 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
            BeginContext(2007, 90, true);
            WriteLiteral("    <p style=\"margin-top:10px;\">\r\n        <label class=\"col-lg-1 center\">Search: </label> ");
            EndContext();
            BeginContext(2098, 91, false);
#line 52 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                   Write(Html.TextBox("SearchString", null, new { @class = "form-control col-lg-8", id = "search" }));

#line default
#line hidden
            EndContext();
            BeginContext(2189, 170, true);
            WriteLiteral("\r\n        <input id=\"submit navbtn\" onkeyup=\"checkSearchChanged()\" class=\"btn btn-default clickable\" style=\"margin-left:10px;\" type=\"submit\" value=\"Search\" />\r\n    </p>\r\n");
            EndContext();
#line 55 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
}

#line default
#line hidden
            BeginContext(2362, 592, true);
            WriteLiteral(@"<table class=""table tablesorter-bootstrap"">
    <thead>
        <tr>
            <th>
                Name
            </th>
            <th>
                Deploy Type
            </th>
            <th>
                ID
            </th>
            <th>
                Resource Group
            </th>
            <th>
                Saved
            </th>
            <th>
                State
            </th>
            <th>
                Result
            </th>
            <th></th>
            <th></th>
        </tr>
    </thead>
    <tbody>
");
            EndContext();
#line 85 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
 foreach (var item in Model.DeployTypes) {

#line default
#line hidden
            BeginContext(2998, 48, true);
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3047, 46, false);
#line 88 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
           Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
            EndContext();
            BeginContext(3093, 39, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
            EndContext();
#line 91 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                 if (!string.IsNullOrEmpty(item.DeployName))
                {
                    

#line default
#line hidden
            BeginContext(3234, 80, false);
#line 93 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
               Write(Html.ActionLink(item.DeployName, "DeployParams", new { Id = item.DeployTypeID }));

#line default
#line hidden
            EndContext();
#line 93 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                                     
                }

#line default
#line hidden
            BeginContext(3335, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3391, 47, false);
#line 98 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
           Write(Html.DisplayFor(modelItem => item.DeployTypeID));

#line default
#line hidden
            EndContext();
            BeginContext(3438, 55, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
            EndContext();
            BeginContext(3494, 52, false);
#line 101 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
           Write(Html.DisplayFor(modelItem => item.ResourceGroupName));

#line default
#line hidden
            EndContext();
            BeginContext(3546, 39, true);
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n");
            EndContext();
#line 104 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                 if (item.DeploySaved == "Yes")
                {

#line default
#line hidden
            BeginContext(3653, 65, true);
            WriteLiteral("                   <span class=\"glyphicon glyphicon-ok\"></span>\r\n");
            EndContext();
#line 107 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                }
                else
                {

#line default
#line hidden
            BeginContext(3778, 70, true);
            WriteLiteral("                    <span class=\"glyphicon glyphicon-remove\"></span>\r\n");
            EndContext();
#line 111 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                }

#line default
#line hidden
            BeginContext(3867, 37, true);
            WriteLiteral("            </td>\r\n            <td>\r\n");
            EndContext();
#line 114 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                 if (!string.IsNullOrEmpty(item.DeployResult))
                {
                    

#line default
#line hidden
#line 116 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                     if (item.DeployResult.Contains("\"provisioningState\":\"Succeeded"))
                    {

#line default
#line hidden
            BeginContext(4101, 139, true);
            WriteLiteral("                        <span class=\"glyphicon glyphicon-ok\"><span style=\"font-family:\'Segoe UI\'; color:green\"> Deployed</span></span>   \r\n");
            EndContext();
#line 119 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                    }

#line default
#line hidden
#line 119 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                     
                    if (item.DeployResult.Contains("\"provisioningState\":\"Running"))
                    {

#line default
#line hidden
            BeginContext(4374, 141, true);
            WriteLiteral("                        <span class=\"glyphicon glyphicon-refresh\"><span style=\"font-family:\'Segoe UI\'; color:orange\"> Running</span></span>\r\n");
            EndContext();
#line 123 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                    }
                    if (item.DeployResult.Contains("\"provisioningState\":\"Accepted"))
                    {

#line default
#line hidden
            BeginContext(4650, 147, true);
            WriteLiteral("                        <span class=\"glyphicon glyphicon-thumbs-up\"><span style=\"font-family:\'Segoe UI\'; color:darkgreen\"> Accepted</span></span>\r\n");
            EndContext();
#line 127 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                    }
                    if (item.DeployResult.Contains("\"provisioningState\":\"Failed"))
                    {

#line default
#line hidden
            BeginContext(4930, 160, true);
            WriteLiteral("                        <span class=\"glyphicon glyphicon-warning-sign\"><span style=\"font-family:\'Segoe UI\'; font-weight:bold; color:red\"> Failed</span></span>\r\n");
            EndContext();
#line 131 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                    }
                    if (item.DeployResult.Contains("Queued"))
                    {

#line default
#line hidden
            BeginContext(5199, 138, true);
            WriteLiteral("                        <span class=\"glyphicon glyphicon-tasks\"><span style=\"font-family:\'Segoe UI\'; color:orange\"> Queued</span></span>\r\n");
            EndContext();
#line 135 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"

                    }
                }
                else
                {
                    

#line default
#line hidden
            BeginContext(5443, 46, false);
#line 140 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
               Write(Html.DisplayFor(modelItem => item.DeployState));

#line default
#line hidden
            EndContext();
#line 140 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                   

                }

#line default
#line hidden
            BeginContext(5512, 37, true);
            WriteLiteral("            </td>\r\n            <td>\r\n");
            EndContext();
#line 145 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                 if (!string.IsNullOrEmpty(item.DeployResult))
                {
                

#line default
#line hidden
            BeginContext(5649, 74, false);
#line 147 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
           Write(Html.ActionLink("Result", "DeployResults", new { Id = item.DeployTypeID }));

#line default
#line hidden
            EndContext();
#line 147 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                           
                }

#line default
#line hidden
            BeginContext(5744, 57, true);
            WriteLiteral("            </td>\r\n            <td>\r\n                    ");
            EndContext();
            BeginContext(5801, 198, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1eb4b835632a0c6263eb3afc7e5ca795c9371dea29275", async() => {
                BeginContext(5937, 58, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-floppy-save\"></span> Save");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 151 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                 WriteLiteral(item.DeployTypeID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5999, 103, true);
            WriteLiteral("\r\n                    <a id=\"deleteItem\" class=\"deleteItem btn btn-danger btn-default navbtn\" data-id=\"");
            EndContext();
            BeginContext(6103, 17, false);
#line 152 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
                                                                                                Write(item.DeployTypeID);

#line default
#line hidden
            EndContext();
            BeginContext(6120, 67, true);
            WriteLiteral("\"><span class=\"glyphicon glyphicon-remove\"></span> Delete</a>    \r\n");
            EndContext();
            BeginContext(6371, 34, true);
            WriteLiteral("            </td>\r\n        </tr>\r\n");
            EndContext();
#line 156 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\DeployTypes\IndexSelectedold.cshtml"
}

#line default
#line hidden
            BeginContext(6408, 26, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(6452, 1256, true);
                WriteLiteral(@"
    <script language='javascript' type='text/javascript'>
        $(document).ready(function () {
            var ua = navigator.userAgent.toLowerCase();
            var ios_devices = ua.match(/(ipad|iphone)/) ? ""touchstart"" : ""click"";
           
            var item_to_delete;
            $("".deleteItem"").bind(ios_devices, function () {
                $('#DeleteWarning').fadeIn();
                item_to_delete = $(this).data('id');
                $('#DeleteWarning').fadeIn();
            });
            $('#DeleteWarningOK').bind(ios_devices, function () {

                $.ajax({
                    type: ""POST"",
                    url: ""/DeployTypes/Delete?id="" + item_to_delete,
                    contentType: ""application/text"",
                    success: function (result) {
                        $('#DeleteWarning').fadeOut();
                        var url = window.location.href;
                        window.location.href = url;
                    }
                ");
                WriteLiteral("});\r\n\r\n            });\r\n            $(\'#DeleteWarningClose\').bind(ios_devices, function () {\r\n                $(\'#responsive\').fadeOut();\r\n                $(\'#DeleteWarning\').fadeOut();\r\n            });\r\n        });\r\n    </script>\r\n");
                EndContext();
            }
            );
            BeginContext(7711, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Deploy.ViewModel.TenantDeploys> Html { get; private set; }
    }
}
#pragma warning restore 1591
