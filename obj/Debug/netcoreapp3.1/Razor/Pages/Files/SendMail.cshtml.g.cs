#pragma checksum "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bba5bf7754945f73c00e3ffcd461b1aace756293"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_Files_SendMail), @"mvc.1.0.razor-page", @"/Pages/Files/SendMail.cshtml")]
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
#line 3 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "{id?}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bba5bf7754945f73c00e3ffcd461b1aace756293", @"/Pages/Files/SendMail.cshtml")]
    public class Pages_Files_SendMail : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("OutputMessage"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("hidden", new global::Microsoft.AspNetCore.Html.HtmlString("hidden"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("ValidaOutPut"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formPost"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
  
    Layout = "_Layout";
    ViewData["Message.Modal.Title"] = "Mensagem";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bba5bf7754945f73c00e3ffcd461b1aace7562936348", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 10 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.OutputMessage);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" \r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bba5bf7754945f73c00e3ffcd461b1aace7562937931", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 11 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ValidaOutPut);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@" 

<!-- Content Wrapper. Contains page content -->
<div class=""content-wrapper"">
<!-- Content Header (Page header) -->
<section class=""content-header"">
    <div class=""container-fluid"">
    <div class=""row mb-2"">
        <div class=""col-sm-6"">
        <h4><i class=""fas fa-cloud-upload-alt mr-1""></i> Envia E-mail de Boleto Cadastrado</h4>
        </div>
    </div>
    </div>
</section>

<!-- Main content -->
<section class=""content"">
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bba5bf7754945f73c00e3ffcd461b1aace7562939981", async() => {
                WriteLiteral("\r\n        <div class=\"container-fluid\">\r\n            <div class=\"row\">\r\n                <!-- left column -->\r\n                <div class=\"col-md-12\">\r\n\r\n");
#nullable restore
#line 34 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                     if (!ModelState.IsValid)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <div class=\"col-md-10\">\r\n                            <h3 class=\"text-danger\">Erros de valida????o</h3>\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bba5bf7754945f73c00e3ffcd461b1aace75629310814", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 38 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </div>\r\n");
#nullable restore
#line 40 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                    <!-- general form elements -->
                    <div class=""card card-secondary card-outline"">
                        <div class=""card-header"">
                            <h3 class=""card-title"">LUC</h3>
                        </div>
                        <!-- /.card-header -->
                        <!-- form start -->
                        <div class=""card-body table-responsive"">
                            <!-- Main content -->
                            <section class=""content"">
                                <div class=""container-fluid"">

                                <div class=""row"">

                                    <div class=""col-12 "">
                                        
");
#nullable restore
#line 57 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                         if(Model.lstLuc.Count() > 0){


#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                            <div class=""card card-tabs m-0"">
                                            <div class=""card-header p-0 pt-1"">
                                                <ul class=""nav nav-tabs"" id=""custom-tabs-one-tab"" role=""tablist"">
                                                    <li class=""nav-item"">
                                                        <a class=""nav-link active"" id=""custom-tabs-one-all-tab"" data-toggle=""pill"" href=""#custom-tabs-one-all"" role=""tab"" aria-controls=""custom-tabs-one-all"" aria-selected=""true"">Todos os boletos</a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class=""card-body table-responsive p-1"">
                                                <div class=""tab-content""  id=""custom-tabs-one-tabContent"">
                                                    <div class=");
                WriteLiteral(@"""tab-pane fade show active p-3"" id=""custom-tabs-one-all"" role=""tabpanel"" aria-labelledby=""custom-tabs-one-all-tab"">
                                                        <div class=""d-flex mailbox-controls mb-2"">
                                                            <div class=""btn-group"">
");
                WriteLiteral(@"                                                            </div>
                                                        </div>
                                                        <table class=""table table-striped table-hover table-bordered table-sm dataTable resultTable"" id=""resultTable"">
                                                            <thead>
                                                                <tr class=""bg-gray"">
                                                                    <th class=""text-center align-middle"">
                                                                        <div class=""icheck-primary text-center"">
                                                                            <input type=""checkbox"" class=""checkbox-toggle-all"">
                                                                        </div>
                                                                    </th>
                                                                ");
                WriteLiteral(@"    <th class=""text-center align-middle"">LUC</th>
                                                                    <th class=""text-center align-middle"">Nome Fantasia</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
");
#nullable restore
#line 88 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                  var userId = User.Claims.FirstOrDefault(x => x.Type == "Id").Value;

#line default
#line hidden
#nullable disable
#nullable restore
#line 89 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                 foreach(var item in Model.lstLuc)
                                                                {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                                                    <tr>
                                                                        <td class=""align-middle"">
                                                                            <div class=""icheck-primary text-center"">
                                                                                <input type=""checkbox""");
                BeginWriteAttribute("value", " value=", 5873, "", 5891, 1);
#nullable restore
#line 94 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
WriteAttributeValue("", 5880, item.Idluc, 5880, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" class=""checkbox-all"">
                                                                            </div>
                                                                        </td>
                                                                        <td class=""text-center align-middle text-dark p-0"">");
#nullable restore
#line 97 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                                                                      Write(item.IdlucNavigation.Txluc);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>                                  \r\n                                                                        <td class=\"text-left align-middle text-dark p-0 pl-2\">");
#nullable restore
#line 98 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                                                                         Write(item.IdlucNavigation.Txfantasia);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                                                    </tr>\r\n");
#nullable restore
#line 100 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                                            </tbody>
                                                        </table>
                                                    </div>
                                                
                                                </div>                      
                                            </div>
                                            </div>
");
#nullable restore
#line 108 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                        }else{

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                        <div class=""card-body"">
                                            <ul class=""list-unstyled"">
                                            <li>
                                                Sem LUC para mostrar.    
                                            </li>
                                            </ul>
                                        </div>
");
#nullable restore
#line 116 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                    </div>
                                    <!-- /.col -->

                                    

                                </div>
                                
                                </div>
                            </section>
                            <!-- /.content -->

                        </div>
");
                WriteLiteral(@"                        <div class=""card-footer"">
                            <button id=""saveBtn"" class=""btn btn-primary mr-1"">
                                <i class=""fas fa-cloud-upload-al mr-1""></i>Enviar e-mail
                            </button>
                        </div>
                    </div>
                    <!-- /.card -->

                </div>
                <!--/.col (left) -->
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
        
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    \r\n</section>\r\n\r\n<!-- Output Messages Modal -->\r\n");
#nullable restore
#line 151 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
 if (HttpContext.Session.GetString("OutputMessage") != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""modal"" id=""messageModal"" tabindex=""1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel""><i class=""far fa-comments text-primary""></i> ");
#nullable restore
#line 157 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                                                                                           Write(ViewData["Message.Modal.Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    <strong>Mensagem: </strong> ");
#nullable restore
#line 163 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
                                           Write(HttpContext.Session.GetString("OutputMessage"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-primary"" data-dismiss=""modal""><i class=""fas fa-exclamation-circle""></i> Ok</button>
                </div>
            </div>
        </div>
    </div>
");
#nullable restore
#line 171 "D:\Projeto Real\pWPortalLogista\Pages\Files\SendMail.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
<!-- /.content -->
<script>
    $(function () {
        $(""#menu_Sys_SendMail"").addClass(""active"")

        $('.resultTable').DataTable({
            ""searching"" : true,
            ""bSort"" : false           
        });

        $("".checkbox-toggle-all"").click(function () {
            $(""input:checkbox.checkbox-all"").prop('checked', $(this).prop(""checked""));
        });

        $(""#saveBtn"").on(""click"", function(){
            var arrayInputs = """"
            $(""input:checked:not(.checkbox-toggle)"").each(function(){
                if(arrayInputs.length > 0){
                    arrayInputs = arrayInputs + "";"" + $(this).val()
                }else{
                    arrayInputs = $(this).val()
                }
            });

            $.ajax({
                type: ""POST"",
                url: ""/API/PostEmail/"" + arrayInputs,
                success: function (response) {
                    if (response.length > 0) {
                    sessionStorage.setItem(""OutputM");
                WriteLiteral(@"essage"", response);
                    location.reload()
                    }
                    
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        });

        var valueOutput = $(""#OutputMessage"").val();
        var validaOutPut = $(""#ValidaOutPut"").val();
        
        if(valueOutput != """"){
            if(validaOutPut == ""SUCCESS""){
                iconOutput = ""success""
            }else{
                iconOutput = ""error""
            }   

            var Toast = Swal.mixin({
                customClass : {
                  title: 'swal2-title-content'
                },
                toast: true,
                position: 'left-bottom',
                showConfirmButton: false,
                timer: 3000
            });
            Toast.fire({
                icon: iconOutput,
                title: valueOutput
            })

            $("".swal2-title-conte");
                WriteLiteral("nt\").css(\"color\",\"#595959\");\r\n        }\r\n    });\r\n</script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<pWPortalLogista.Pages.Files.SendMailModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<pWPortalLogista.Pages.Files.SendMailModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<pWPortalLogista.Pages.Files.SendMailModel>)PageContext?.ViewData;
        public pWPortalLogista.Pages.Files.SendMailModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
