#pragma checksum "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "175b1840506dd58d91506a3107fc1f1763333aa4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Quoridor_Board), @"mvc.1.0.view", @"/Views/Quoridor/Board.cshtml")]
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
#line 1 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/_ViewImports.cshtml"
using Quoridor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/_ViewImports.cshtml"
using Quoridor.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"175b1840506dd58d91506a3107fc1f1763333aa4", @"/Views/Quoridor/Board.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d6523f1d92bb58a1dc5a8160c03cbb7ea3be3ab6", @"/Views/_ViewImports.cshtml")]
    public class Views_Quoridor_Board : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Game>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Quoridor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NewGame", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "PossibleMoves", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax", new global::Microsoft.AspNetCore.Html.HtmlString("true"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-method", new global::Microsoft.AspNetCore.Html.HtmlString("POST"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-update", new global::Microsoft.AspNetCore.Html.HtmlString("#movesHelper"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-ajax-mode", new global::Microsoft.AspNetCore.Html.HtmlString("replace"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
  
    ViewData["Title"] = "Quoridor";
    //Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 6 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n\n");
            WriteLiteral("\n");
#nullable restore
#line 10 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
  
    object SetFenceInvoke()
    {
        Quoridor.Controllers.QuoridorController g = new Quoridor.Controllers.QuoridorController(); //какая-то дичь
        return g.SetFence(new Cell() { X = "b", Y = 0 }, new Cell() { X = "b", Y = 1 });
        //Model.SetFence(new Cell() { X = "b", Y = 0 }, new Cell() { X = "b", Y = 1 });
        //return Model.SecondPWalls;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""buttons"">
    <button type=""button"" id=""restart"" class=""button above restart"">New game</button>
</div>

<div class=""gridcontainer0"">
    <div id=""number_of_left_walls_box"">
        <div><div class=""symbol1""></div><div class=""horizontal_wall symbol""></div><div class=""mul_sign_wall_num_container""><div class=""multiplication_sign"">×</div><div id=""num1"" class=""wall_num"">");
#nullable restore
#line 26 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                                                                                                                                                                                              Write(ViewData["fPwalls"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div></div></div>\n        <div><div class=\"symbol2\"></div><div class=\"horizontal_wall symbol\"></div><div class=\"mul_sign_wall_num_container\"><div class=\"multiplication_sign\">×</div><div id=\"num2\" class=\"wall_num\">");
#nullable restore
#line 27 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                                                                                                                                                                                              Write(ViewData["sPwalls"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div></div></div>\n    </div>\n    <div id=\"board_table_container\">\n        <div class=\"fade_box in hidden\" id=\"restart_message_box\">\n            Are you sure to start a new game?\n            <br>\n            (Current game will be lost.)\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "175b1840506dd58d91506a3107fc1f1763333aa48485", async() => {
                WriteLiteral(@"
                <div class=""button_container in_message_box"">
                    <!--<button type=""button"" class=""restart_yes_no"" id=""restart_no"">Cancel</button>
                    <button type=""button"" class=""restart_yes_no"" id=""restart_yes"">Start</button> -->
                    <input type=""button"" class=""restart_yes_no"" id=""restart_no"" value=""Cancel"">
                    <input type=""submit"" class=""restart_yes_no"" id=""restart_yes"" value=""Start"">
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </div>\n        <table id=\"board_table\" class=\"geek\" border=\"1\">\n            <tr class=\"row1\">\n                <td class=\"col cell\" ");
            WriteLiteral("></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"><div class=\"pawn pawn0\" id=\"pawn0\"></div></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral("></td>\n                <td class=\"col cell\"></td>\n                <td class=\"between_cols vwall\" ");
            WriteLiteral(@"></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
               ");
            WriteLiteral(@" <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell"" ");
            WriteLiteral(@"></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col ");
            WriteLiteral(@"hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=");
            WriteLiteral(@"""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
     ");
            WriteLiteral(@"           <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""c");
            WriteLiteral(@"ol hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""><");
            WriteLiteral(@"/td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall"">");
            WriteLiteral(@"</td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class");
            WriteLiteral(@"=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
       ");
            WriteLiteral(@"         <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwal");
            WriteLiteral(@"l""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></t");
            WriteLiteral(@"d>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
 ");
            WriteLiteral(@"           </tr>
            <tr class=""row10"">
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""><div class=""pawn pawn1"" id=""pawn1""></div></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td class=""col cell""></td>
            </tr>
        </table>
    </div>
</div>
<div>
    <div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "175b1840506dd58d91506a3107fc1f1763333aa426607", async() => {
                WriteLiteral("\n            <input type=\"submit\" name=\"btnsearch\" value=\"Search\" />\n            <div id=\"movesHelper\">\n");
                WriteLiteral("\n            </div>\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </div>\n    \n    \n");
            WriteLiteral("</div>\n\n\n");
            WriteLiteral("\n<script>\n    function moves() {\n        let myArray = ");
#nullable restore
#line 418 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                 Write(ViewData["moves"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n        console.log(myArray);\n    }\n    \n\n");
            WriteLiteral("</script>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Game> Html { get; private set; }
    }
}
#pragma warning restore 1591
