#pragma checksum "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Queue_EditOld), @"mvc.1.0.view", @"/Views/Queue/EditOld.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Queue/EditOld.cshtml", typeof(AspNetCore.Views_Queue_EditOld))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd", @"/Views/Queue/EditOld.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61fc30401d5c31f8b5904ac5f2a4b15299047601", @"/Views/_ViewImports.cshtml")]
    public class Views_Queue_EditOld : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Deploy.ViewModel.QueueViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeployfromQueue", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-lg navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-force", "True", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "DeployTypes", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-lg navbtn"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
  
    ViewData["Title"] = "Edit Queue";

#line default
#line hidden
            BeginContext(88, 914, true);
            WriteLiteral(@"
<div id=""DeleteWarning"" class=""modal"" tabindex=""1"" style=""display:none;"">
    <div class=""modal-dialog"">
        <div class=""modal-content"">
            <div style=""font-size:18px;"">
                <i class=""fa fa-2x fa-exclamation-triangle"" aria-hidden=""true""></i><span style=""font-size:32px""><b>  Warning</b></span>
                <p style=""margin-top:15px"">Are you sure you wish to delete this queue item?</p>
                <p>Please confirm you choice, or cancel to return to the queue.</p>
                <button id=""DeleteWarningClose"" class=""btn btn-success btn-lg"" data-dismiss=""modal"" aria-label=""Close"">Close</button>
                <button id=""DeleteWarningOK"" class=""btn btn-danger btn-lg"">Delete</button>
            </div>
        </div>
    </div>
</div>

<h2>Deployment Queue</h2>
<p>
    You are currently working in company: <span style=""font-weight:bold;font-size:20px;"">");
            EndContext();
            BeginContext(1003, 67, false);
#line 23 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                                                                                    Write(Html.DisplayFor(Model => Model.Queues.FirstOrDefault().TennantName));

#line default
#line hidden
            EndContext();
            BeginContext(1070, 17, true);
            WriteLiteral("</span>\r\n</p>\r\n\r\n");
            EndContext();
#line 26 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
  
    var QueueRunning = 0;

    for (var i = 0; i < Model.Queues.Count(); i++)
    {
        if (Model.Queues[i].status == "Running" || Model.Queues[i].status == "Complete" || Model.Queues[i].status == "Failed")
        {
            QueueRunning++;
        }
    }

#line default
#line hidden
#line 37 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
 if (Model.Queues.FirstOrDefault().DeployTypeID != 0 && QueueRunning == 0) 
{

#line default
#line hidden
            BeginContext(1448, 214, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd8377", async() => {
                BeginContext(1597, 61, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-cloud-upload\"></span> Deploy");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 39 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                                                                        WriteLiteral(Model.Queues.FirstOrDefault().TennantID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-force", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["force"] = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1662, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 40 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
}

#line default
#line hidden
            BeginContext(1667, 209, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd11544", async() => {
                BeginContext(1812, 60, true);
                WriteLiteral("<span class=\"glyphicon glyphicon-triangle-left\"></span> Back");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 41 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                                                     WriteLiteral(Model.Queues.FirstOrDefault().TennantID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1876, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1878, 4317, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd14262", async() => {
                BeginContext(1902, 101, true);
                WriteLiteral("\r\n    <div class=\"form-horizontal\">\r\n        <h4>Edit Deployment Queue</h4>\r\n        <hr />\r\n        ");
                EndContext();
                BeginContext(2003, 66, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a2c0c4dd7e1a68823dc7b2c2cb5cd0392d4c7ebd14755", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#line 46 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2069, 721, true);
                WriteLiteral(@"
        <table class=""table tablesorter-bootstrap"" id=""queue"">
            <thead>
                <tr id=""header"">
                    <th>
                        Name
                    </th>
                    <th>
                        Deploy Type
                    </th>
                    <th>
                        Tenant Name
                    </th>
                    <th>
                        Resource Group
                    </th>
                    <th>
                        Status
                    </th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
");
                EndContext();
#line 71 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                 if (Model.Queues.FirstOrDefault().DeployTypeID ==  0)
                {

#line default
#line hidden
                BeginContext(2881, 63, true);
                WriteLiteral("                    <tr><td>No items in Queue. . . </td></tr>\r\n");
                EndContext();
#line 74 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                }
                else
                {
                    

#line default
#line hidden
#line 77 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                     for (var i = 0; i < Model.Queues.Count(); i++)
                    {

#line default
#line hidden
                BeginContext(3096, 27, true);
                WriteLiteral("                        <tr");
                EndContext();
                BeginWriteAttribute("id", " id=", 3123, "", 3129, 1);
#line 79 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
WriteAttributeValue("", 3127, i, 3127, 2, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3129, 69, true);
                WriteLiteral(">\r\n                            <td>\r\n                                ");
                EndContext();
                BeginContext(3199, 153, false);
#line 81 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].Description, new { name = "Queues[" + i + "].Description", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(3352, 103, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
                EndContext();
                BeginContext(3456, 151, false);
#line 84 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].DeployName, new { name = "Queues[" + i + "].DeployName", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(3607, 103, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
                EndContext();
                BeginContext(3711, 153, false);
#line 87 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].TennantName, new { name = "Queues[" + i + "].TennantName", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(3864, 103, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
                EndContext();
                BeginContext(3968, 157, false);
#line 90 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].resourcegroup, new { name = "Queues[" + i + "].resourcegroup", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(4125, 103, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
                EndContext();
                BeginContext(4229, 143, false);
#line 93 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].status, new { name = "Queues[" + i + "].status", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(4372, 133, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td class=\"order\" hidden=\"hidden\">\r\n                                ");
                EndContext();
                BeginContext(4506, 141, false);
#line 96 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                           Write(Html.EditorFor(modelItem => Model.Queues[i].Order, new { name = "Queues[" + i + "].Order", htmlAttributes = new { @readonly = "readonly" } }));

#line default
#line hidden
                EndContext();
                BeginContext(4647, 184, true);
                WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                <a id=\"deleteItem\" class=\"deleteItem btn btn-danger btn-default navbtn\" data-id=\"");
                EndContext();
                BeginContext(4832, 23, false);
#line 99 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                                                                                                            Write(Model.Queues[i].QueueID);

#line default
#line hidden
                EndContext();
                BeginContext(4855, 127, true);
                WriteLiteral("\">Delete</a>\r\n                            </td>\r\n                            <td hidden=\"hidden\">\r\n                            ");
                EndContext();
                BeginContext(4983, 104, false);
#line 102 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].azuredeploy, new { name = "Queues[" + i + "].azuredeploy" }));

#line default
#line hidden
                EndContext();
                BeginContext(5087, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5118, 106, false);
#line 103 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].DeployTypeID, new { name = "Queues[" + i + "].DeployTypeID" }));

#line default
#line hidden
                EndContext();
                BeginContext(5224, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5255, 104, false);
#line 104 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].DeployTypes, new { name = "Queues[" + i + "].DeployTypes" }));

#line default
#line hidden
                EndContext();
                BeginContext(5359, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5390, 96, false);
#line 105 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].QueueID, new { name = "Queues[" + i + "].QueueID" }));

#line default
#line hidden
                EndContext();
                BeginContext(5486, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5517, 98, false);
#line 106 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].resource, new { name = "Queues[" + i + "].resource" }));

#line default
#line hidden
                EndContext();
                BeginContext(5615, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5646, 110, false);
#line 107 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].subscriptionID, new { name = "Queues[" + i + "].subscriptionID" }));

#line default
#line hidden
                EndContext();
                BeginContext(5756, 30, true);
                WriteLiteral("\r\n                            ");
                EndContext();
                BeginContext(5787, 100, false);
#line 108 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].TennantID, new { name = "Queues[" + i + "].TennantID" }));

#line default
#line hidden
                EndContext();
                BeginContext(5887, 31, true);
                WriteLiteral(")\r\n                            ");
                EndContext();
                BeginContext(5919, 101, false);
#line 109 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                       Write(Html.HiddenFor(modelItem => Model.Queues[i].DependsOn, new {  name = "Queues[" + i + "].DependsOn" }));

#line default
#line hidden
                EndContext();
                BeginContext(6020, 68, true);
                WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
                EndContext();
#line 112 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"

                    }

#line default
#line hidden
#line 113 "C:\Users\james.tighe\source\repos\Deploy\Deploy\Views\Queue\EditOld.cshtml"
                     
                }

#line default
#line hidden
                BeginContext(6132, 56, true);
                WriteLiteral("            </tbody>\r\n        </table>\r\n\r\n\r\n    </div>\r\n");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6195, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(6217, 2594, true);
                WriteLiteral(@"
    <script language='javascript' type='text/javascript'>
        $(document).ready(function () {
            var tRow = document.getElementById('queue').getElementsByTagName('tr');
            var count = 0;
            for (var i = 1; i < tRow.length; i++) {
                var td = tRow[i].cells;
                for (var y = 0; y < td.length; y++) {
                    if (td[y].innerHTML.match(/status/)) {
                        if (td[y].innerHTML.match(/OK to Deploy/)) {
                            count++;
                        }
                    }
                }


            }
            if (count < 1) {
                $('#queue tbody').sortable({
                    update: function (event, ui) {

                        var tRow = document.getElementById('queue').getElementsByTagName('tr');

                        for (var i = 1; i < tRow.length; i++) {
                            var td = tRow[i].cells;
                            for (var y = 0; y < td.length");
                WriteLiteral(@"; y++) {
                                if (td[y].innerHTML.match(/Order/)) {
                                    html = td[y].getElementsByTagName('input')[0]
                                    html.value = i
                                }

                            }

                        }
                        $('form').submit();

                    }


                });
            }

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
                    url: ""/Queue/Delete?id="" + item_to_delete");
                WriteLiteral(@",
                    contentType: ""application/text"",
                    success: function(result) {
                        $('#DeleteWarning').fadeOut();
                        var url = window.location.href;
                        window.location.href = url;
                    }
                });

            });
            $('#DeleteWarningClose').bind(ios_devices, function () {
                $('#responsive').fadeOut();
                $('#DeleteWarning').fadeOut();
            });


    });

     </script>
");
                EndContext();
            }
            );
            BeginContext(8814, 4, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Deploy.ViewModel.QueueViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
