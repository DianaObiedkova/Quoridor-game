#pragma checksum "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "107170907005dff91ffda3de1bc33a28438f777e"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"107170907005dff91ffda3de1bc33a28438f777e", @"/Views/Quoridor/Board.cshtml")]
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

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>");
#nullable restore
#line 5 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<div class=""buttons"">
    <button type=""button"" id=""restart"" class=""button above restart"">New game</button>
</div>

<div class=""gridcontainer0"">
    <div id=""number_of_left_walls_box"">
        <div><div class=""symbol1""></div><div class=""horizontal_wall symbol""></div><div class=""mul_sign_wall_num_container""><div class=""multiplication_sign"">??</div><div id=""num1"" class=""wall_num"">");
#nullable restore
#line 13 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                                                                                                                                                                                              Write(ViewData["fPwalls"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div></div></div>\n        <div><div class=\"symbol2\"></div><div class=\"horizontal_wall symbol\"></div><div class=\"mul_sign_wall_num_container\"><div class=\"multiplication_sign\">??</div><div id=\"num2\" class=\"wall_num\">");
#nullable restore
#line 14 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                                                                                                                                                                                              Write(ViewData["sPwalls"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div></div></div>\n    </div>\n    <div id=\"board_table_container\">\n        <div class=\"fade_box in hidden\" id=\"restart_message_box\">\n            Are you sure to start a new game?\n            <br>\n            (Current game will be lost.)\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "107170907005dff91ffda3de1bc33a28438f777e7875", async() => {
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
            WriteLiteral(@"
        </div>
        <table id=""board_table"" class=""geek"" border=""1"">
            <tr class=""row1"">
                <td id=""a0"" class=""col cell""></td>
                <td cell1=""b0"" cell2=""b1"" class=""between_cols vwall""></td>
                <td id=""b0"" class=""col cell""></td>
                <td cell1=""c0"" cell2=""c1"" class=""between_cols vwall""></td>
                <td id=""c0"" class=""col cell""></td>
                <td cell1=""d0"" cell2=""d1"" class=""between_cols vwall""></td>
                <td id=""d0"" class=""col cell""></td>
                <td cell1=""e0"" cell2=""e1"" class=""between_cols vwall""></td>
                <td id=""e0"" class=""col cell""><div class=""pawn pawn0"" id=""pawn0""></div></td>
                <td cell1=""f0"" cell2=""f1"" class=""between_cols vwall""></td>
                <td id=""f0"" class=""col cell""></td>
                <td cell1=""g0"" cell2=""g1"" class=""between_cols vwall""></td>
                <td id=""g0"" class=""col cell""></td>
                <td cell1=""h0"" cell2=""h1"" class=""between_cols vwall""></td");
            WriteLiteral(@">
                <td id=""h0"" class=""col cell""></td>
                <td cell1=""i0"" cell2=""i1"" class=""between_cols vwall""></td>
                <td id=""i0"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a0"" cell2=""b0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b0"" cell2=""c0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c0"" cell2=""d0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d0"" cell2=""e0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e0"" cell2=""f0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f0"" cell2=""g0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g0"" cell2=""h0"" class=""col hwall""></td>
                <td class=""between_cols""></td");
            WriteLiteral(@">
                <td cell1=""h0"" cell2=""i0"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a1"" class=""col cell""></td>
                <td cell1=""b1"" cell2=""b2"" class=""between_cols vwall""></td>
                <td id=""b1"" class=""col cell""></td>
                <td cell1=""c1"" cell2=""c2"" class=""between_cols vwall""></td>
                <td id=""c1"" class=""col cell""></td>
                <td cell1=""d1"" cell2=""d2"" class=""between_cols vwall""></td>
                <td id=""d1"" class=""col cell""></td>
                <td cell1=""e1"" cell2=""e2"" class=""between_cols vwall""></td>
                <td id=""e1"" class=""col cell""></td>
                <td cell1=""f1"" cell2=""f2"" class=""between_cols vwall""></td>
                <td id=""f1"" class=""col cell""></td>
                <td cell1=""g1"" cell2=""g2"" class=""between_cols vwall""></td>
                <td id=""g1"" class=""col cell""></td>
                <td cel");
            WriteLiteral(@"l1=""h1"" cell2=""h2"" class=""between_cols vwall""></td>
                <td id=""h1"" class=""col cell""></td>
                <td cell1=""i1"" cell2=""i2"" class=""between_cols vwall""></td>
                <td id=""i1"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a1"" cell2=""b1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b1"" cell2=""c1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c1"" cell2=""d1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d1"" cell2=""e1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e1"" cell2=""f1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f1"" cell2=""g1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g1"" cell2=""h1"" class=""col hwall""><");
            WriteLiteral(@"/td>
                <td class=""between_cols""></td>
                <td cell1=""h1"" cell2=""i1"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a2"" class=""col cell""></td>
                <td cell1=""b2"" cell2=""b3"" class=""between_cols vwall""></td>
                <td id=""b2"" class=""col cell""></td>
                <td cell1=""c2"" cell2=""c3"" class=""between_cols vwall""></td>
                <td id=""c2"" class=""col cell""></td>
                <td cell1=""d2"" cell2=""d3"" class=""between_cols vwall""></td>
                <td id=""d2"" class=""col cell""></td>
                <td cell1=""e2"" cell2=""e3"" class=""between_cols vwall""></td>
                <td id=""e2"" class=""col cell""></td>
                <td cell1=""f2"" cell2=""f3"" class=""between_cols vwall""></td>
                <td id=""f2"" class=""col cell""></td>
                <td cell1=""g2"" cell2=""g3"" class=""between_cols vwall""></td>
                <td id=""");
            WriteLiteral(@"g2"" class=""col cell""></td>
                <td cell1=""h2"" cell2=""h3"" class=""between_cols vwall""></td>
                <td id=""h2"" class=""col cell""></td>
                <td cell1=""i2"" cell2=""i3"" class=""between_cols vwall""></td>
                <td id=""i2"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a2"" cell2=""b2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b2"" cell2=""c2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c2"" cell2=""d2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d2"" cell2=""e2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e2"" cell2=""f2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f2"" cell2=""g2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
           ");
            WriteLiteral(@"     <td cell1=""g2"" cell2=""h2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h2"" cell2=""i2"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a3"" class=""col cell""></td>
                <td cell1=""b3"" cell2=""b4"" class=""between_cols vwall""></td>
                <td id=""b3"" class=""col cell""></td>
                <td cell1=""c3"" cell2=""c4"" class=""between_cols vwall""></td>
                <td id=""c3"" class=""col cell""></td>
                <td cell1=""d3"" cell2=""d4"" class=""between_cols vwall""></td>
                <td id=""d3"" class=""col cell""></td>
                <td cell1=""e3"" cell2=""e4"" class=""between_cols vwall""></td>
                <td id=""e3"" class=""col cell""></td>
                <td cell1=""f3"" cell2=""f4"" class=""between_cols vwall""></td>
                <td id=""f3"" class=""col cell""></td>
                <td cell1=""g3"" cell2=""g4"" class=""");
            WriteLiteral(@"between_cols vwall""></td>
                <td id=""g3"" class=""col cell""></td>
                <td cell1=""h3"" cell2=""h4"" class=""between_cols vwall""></td>
                <td id=""h3"" class=""col cell""></td>
                <td cell1=""i3"" cell2=""i4"" class=""between_cols vwall""></td>
                <td id=""i3"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a3"" cell2=""b3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b3"" cell2=""c3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c3"" cell2=""d3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d3"" cell2=""e3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e3"" cell2=""f3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f3"" cell2=""g3"" class=""col hwall""></td>
        ");
            WriteLiteral(@"        <td class=""between_cols""></td>
                <td cell1=""g3"" cell2=""h3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h3"" cell2=""i3"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a4"" class=""col cell""></td>
                <td cell1=""b4"" cell2=""b5"" class=""between_cols vwall""></td>
                <td id=""b4"" class=""col cell""></td>
                <td cell1=""c4"" cell2=""c5"" class=""between_cols vwall""></td>
                <td id=""c4"" class=""col cell""></td>
                <td cell1=""d4"" cell2=""d5"" class=""between_cols vwall""></td>
                <td id=""d4"" class=""col cell""></td>
                <td cell1=""e4"" cell2=""e5"" class=""between_cols vwall""></td>
                <td id=""e4"" class=""col cell""></td>
                <td cell1=""f4"" cell2=""f5"" class=""between_cols vwall""></td>
                <td id=""f4"" class=""col cell""></td>");
            WriteLiteral(@"
                <td cell1=""g4"" cell2=""g5"" class=""between_cols vwall""></td>
                <td id=""g4"" class=""col cell""></td>
                <td cell1=""h4"" cell2=""h5"" class=""between_cols vwall""></td>
                <td id=""h4"" class=""col cell""></td>
                <td cell1=""i4"" cell2=""i5"" class=""between_cols vwall""></td>
                <td id=""i4"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a4"" cell2=""b4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b4"" cell2=""c4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c4"" cell2=""d4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d4"" cell2=""e4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e4"" cell2=""f4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell");
            WriteLiteral(@"1=""f4"" cell2=""g4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g4"" cell2=""h4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h4"" cell2=""i4"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a5"" class=""col cell""></td>
                <td cell1=""b5"" cell2=""b6"" class=""between_cols vwall""></td>
                <td id=""b5"" class=""col cell""></td>
                <td cell1=""c5"" cell2=""c6"" class=""between_cols vwall""></td>
                <td id=""c5"" class=""col cell""></td>
                <td cell1=""d5"" cell2=""d6"" class=""between_cols vwall""></td>
                <td id=""d5"" class=""col cell""></td>
                <td cell1=""e5"" cell2=""e6"" class=""between_cols vwall""></td>
                <td id=""e5"" class=""col cell""></td>
                <td cell1=""f5"" cell2=""f6"" class=""between_cols vwall""></td>
");
            WriteLiteral(@"                <td id=""f5"" class=""col cell""></td>
                <td cell1=""g5"" cell2=""g6"" class=""between_cols vwall""></td>
                <td id=""g5"" class=""col cell""></td>
                <td cell1=""h5"" cell2=""h6"" class=""between_cols vwall""></td>
                <td id=""h5"" class=""col cell""></td>
                <td cell1=""i5"" cell2=""i6"" class=""between_cols vwall""></td>
                <td id=""i5"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a5"" cell2=""b5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b5"" cell2=""c5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c5"" cell2=""d5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d5"" cell2=""e5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e5"" cell2=""f5"" class=""col hwall""></td>
                <td c");
            WriteLiteral(@"lass=""between_cols""></td>
                <td cell1=""f5"" cell2=""g5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g5"" cell2=""h5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h5"" cell2=""i5"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a6"" class=""col cell""></td>
                <td cell1=""b6"" cell2=""b7"" class=""between_cols vwall""></td>
                <td id=""b6"" class=""col cell""></td>
                <td cell1=""c6"" cell2=""c7"" class=""between_cols vwall""></td>
                <td id=""c6"" class=""col cell""></td>
                <td cell1=""d6"" cell2=""d7"" class=""between_cols vwall""></td>
                <td id=""d6"" class=""col cell""></td>
                <td cell1=""e6"" cell2=""e7"" class=""between_cols vwall""></td>
                <td id=""e6"" class=""col cell""></td>
                <td cell1");
            WriteLiteral(@"=""f6"" cell2=""f7"" class=""between_cols vwall""></td>
                <td id=""f6"" class=""col cell""></td>
                <td cell1=""g6"" cell2=""g7"" class=""between_cols vwall""></td>
                <td id=""g6"" class=""col cell""></td>
                <td cell1=""h6"" cell2=""h7"" class=""between_cols vwall""></td>
                <td id=""h6"" class=""col cell""></td>
                <td cell1=""i6"" cell2=""i7"" class=""between_cols vwall""></td>
                <td id=""i6"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a6"" cell2=""b6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b6"" cell2=""c6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c6"" cell2=""d6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d6"" cell2=""e6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""e6"" cell2=");
            WriteLiteral(@"""f6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f6"" cell2=""g6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g6"" cell2=""h6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h6"" cell2=""i6"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr>
                <td id=""a7"" class=""col cell""></td>
                <td cell1=""b7"" cell2=""b8"" class=""between_cols vwall""></td>
                <td id=""b7"" class=""col cell""></td>
                <td cell1=""c7"" cell2=""c8"" class=""between_cols vwall""></td>
                <td id=""c7"" class=""col cell""></td>
                <td cell1=""d7"" cell2=""d8"" class=""between_cols vwall""></td>
                <td id=""d7"" class=""col cell""></td>
                <td cell1=""e7"" cell2=""e8"" class=""between_cols vwall""></td>
                <td id=""e7");
            WriteLiteral(@""" class=""col cell""></td>
                <td cell1=""f7"" cell2=""f8"" class=""between_cols vwall""></td>
                <td id=""f7"" class=""col cell""></td>
                <td cell1=""g7"" cell2=""g8"" class=""between_cols vwall""></td>
                <td id=""g7"" class=""col cell""></td>
                <td cell1=""h7"" cell2=""h8"" class=""between_cols vwall""></td>
                <td id=""h7"" class=""col cell""></td>
                <td cell1=""i7"" cell2=""i8"" class=""between_cols vwall""></td>
                <td id=""i7"" class=""col cell""></td>
            </tr>
            <tr class=""between_rows"">
                <td cell1=""a7"" cell2=""b7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""b7"" cell2=""c7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""c7"" cell2=""d7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""d7"" cell2=""e7"" class=""col hwall""></td>
                <td class=""between");
            WriteLiteral(@"_cols""></td>
                <td cell1=""e7"" cell2=""f7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""f7"" cell2=""g7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""g7"" cell2=""h7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td cell1=""h7"" cell2=""i7"" class=""col hwall""></td>
                <td class=""between_cols""></td>
                <td class=""col hwall""></td>
            </tr>
            <tr class=""row10"">
                <td id=""a8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""b8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""c8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""d8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""e8"" class=""col cell""><div ");
            WriteLiteral(@"class=""pawn pawn1"" id=""pawn1""></div></td>
                <td class=""between_cols vwall""></td>
                <td id=""f8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""g8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""h8"" class=""col cell""></td>
                <td class=""between_cols vwall""></td>
                <td id=""i8"" class=""col cell""></td>
            </tr>
        </table>
    </div>
</div>
<div>
    <div>
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "107170907005dff91ffda3de1bc33a28438f777e29960", async() => {
                WriteLiteral("\n            <input type=\"submit\" name=\"btnsearch\" value=\"Search\" />\n            <div id=\"movesHelper\">\n            </div>\n        ");
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
            WriteLiteral("\n    </div>\n    \n</div>\n\n\n<script>\n    function moves() {\n        let myArray = ");
#nullable restore
#line 371 "/home/cybergoose/Quoridor-game/Quoridor/Quoridor/Views/Quoridor/Board.cshtml"
                 Write(ViewData["moves"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n        console.log(myArray);\n    }\n    \n\n</script>\n");
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
