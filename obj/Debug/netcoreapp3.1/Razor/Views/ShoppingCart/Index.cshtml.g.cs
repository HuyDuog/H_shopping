#pragma checksum "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2f5550eb943f6f255053b107465543f62519c701"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ShoppingCart_Index), @"mvc.1.0.view", @"/Views/ShoppingCart/Index.cshtml")]
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
#line 1 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\_ViewImports.cshtml"
using HShopping;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\_ViewImports.cshtml"
using HShopping.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2f5550eb943f6f255053b107465543f62519c701", @"/Views/ShoppingCart/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"575147e1e7d6b835d1099656b34e1423edc7f689", @"/Views/_ViewImports.cshtml")]
    public class Views_ShoppingCart_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<HShopping.ModelView.CartItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("javascript:void(0)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main class=""main-content"">
    <div class=""breadcrumb-area breadcrumb-height"" data-bg-image=""images/slideshow/8qo4c.jpg"">
        <div class=""container h-100"">
            <div class=""row h-100"">
                <div class=""col-lg-12"">
                    <div class=""breadcrumb-item"">
                        <h2 class=""breadcrumb-heading"">Giỏ hàng</h2>
                        <ul>
                            <li>
                                <a href=""/"">Trang chủ <i class=""pe-7s-angle-right""></i></a>
                            </li>
                            <li>Giỏ hàng</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""cart-area section-space-y-axis-100"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-12"">
");
#nullable restore
#line 29 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                     if (Model != null && Model.Count() > 0)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2f5550eb943f6f255053b107465543f62519c7015257", async() => {
                WriteLiteral(@"
                            <div class=""table-content table-responsive"">
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th class=""product_remove"">Xóa</th>
                                            <th class=""product-thumbnail"">Ảnh </th>
                                            <th class=""cart-product-name"">Sản phẩm</th>
                                            <th class=""product-price"">Unit Đơn giá</th>
                                            <th class=""product-quantity"">Số lượng</th>
                                            <th class=""product-subtotal"">Thành tiền</th>
                                        </tr>
                                    </thead>
                                    <tbody>
");
#nullable restore
#line 45 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                         if (Model != null && Model.Count() > 0)
                                        {
                                            foreach (var item in Model)
                                            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                                <tr>
                                                    <td class=""product_remove"">
                                                            <input type=""button"" value=""X"" class=""removecart btn btn-primary"" data-mahh=""");
#nullable restore
#line 51 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                                                                                    Write(item.product.ProductId);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""" />

                                                    </td>
                                                    <td class=""product-thumbnail"">
                                                        <a href=""javascript:void(0)"">
                                                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "2f5550eb943f6f255053b107465543f62519c7017805", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 2895, "~/images/products/", 2895, 18, true);
#nullable restore
#line 56 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
AddHtmlAttributeValue("", 2913, item.product.Thumb, 2913, 19, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 56 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
AddHtmlAttributeValue("", 2939, item.product.Title, 2939, 19, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                                        </a>\r\n                                                    </td>\r\n                                                    <td class=\"product-name\"><a href=\"javascript:void(0)\">");
#nullable restore
#line 59 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                                                     Write(item.product.ProductName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</a></td>\r\n                                                    <td class=\"product-price\"><span class=\"amount\">");
#nullable restore
#line 60 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                                              Write(item.product.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n                                                    <td class=\"quantity\">\r\n                                                        <div class=\"cart-plus-minus\">\r\n                                                            <input data-mahh=\"");
#nullable restore
#line 63 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                         Write(item.product.ProductId);

#line default
#line hidden
#nullable disable
                WriteLiteral("\" data-dongia=\"");
#nullable restore
#line 63 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                                                               Write(item.product.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral("\" class=\"cartItem cart-plus-minus-box\"");
                BeginWriteAttribute("value", " value=\"", 3692, "\"", 3712, 1);
#nullable restore
#line 63 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
WriteAttributeValue("", 3700, item.amount, 3700, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"number\" min=\"1\">\r\n                                                        </div>\r\n                                                    </td>\r\n                                                    <td class=\"product-subtotal\"><span class=\"amount\">");
#nullable restore
#line 66 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                                                                 Write(item.TotalMoney);

#line default
#line hidden
#nullable disable
                WriteLiteral("</span></td>\r\n                                                </tr>\r\n");
#nullable restore
#line 68 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                            }
                                        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                    </tbody>
                                </table>
                            </div>
                            <div class=""row"">
                                <div class=""col-12"">
                                    <div class=""coupon-all"">
                                        <div class=""coupon"">
                                            <input id=""coupon_code"" class=""input-text"" name=""coupon_code""");
                BeginWriteAttribute("value", " value=\"", 4594, "\"", 4602, 0);
                EndWriteAttribute();
                WriteLiteral(@" placeholder=""Coupon code"" type=""text"">
                                            <input class=""button mt-xxs-30"" name=""apply_coupon"" value=""Apply coupon"" type=""submit"">
                                        </div>
                                        <div class=""coupon2"">
                                            <input class=""button"" name=""update_cart"" value=""Update cart"" type=""submit"">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""col-md-5 ml-auto"">
                                    <div class=""cart-page-total"">
                                        <h2>Tổng đơn hàng</h2>
                                        <ul>
                                            <li>Thành tiền <span>");
#nullable restore
#line 92 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                                                            Write(Model.Sum(x => x.TotalMoney).ToString());

#line default
#line hidden
#nullable disable
                WriteLiteral(@" VNĐ</span></li>
                                        </ul>
                                        <a href=""javascript:void(0)"">Thanh toán</a>
                                    </div>
                                </div>
                            </div>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 99 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <p>Chưa có mặt hàng trong giỏ hàng </p>\r\n");
#nullable restore
#line 103 "C:\Users\Lenovo\OneDrive\Máy tính\Rookies\HShopping\Views\ShoppingCart\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</main>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(function () {
            function loadHeaderCart() {
                $('#miniCart').load(""/AjaxContent/HeaderCart"");
                $('#numberCart').load(""/AjaxContent/NumberCart"");
            }
            $("".removecart"").click(function () {
                var productid = $(this).attr(""data-mahh"");
                $.ajax({
                    url: ""/cart/remove"",
                    type: ""POST"",
                    datatype: ""JSON"",
                    data: {
                        productID: productid
                    },
                    success: function (result) {
                        if (result.success) {
                            loadHeaderCart();
                            window.location = ""cart.html""
                        }
                    },
                    error: function (error) {
                        alert(""Xảy ra lỗi khi cập nhật số lượng"");
                    }
                });
            });
            $(");
                WriteLiteral(@""".cartItem"").click(function () {
                var productid = $(this).attr(""data-mahh"");
                var soluong = parseInt($(this).val());
                $.ajax({
                    url: ""/cart/update"",
                    type: ""POST"",
                    datatype: ""JSON"",
                    data: {
                        productID: productid,
                        amount: soluong
                    },
                    success: function (result) {
                        if (result.success) {
                            loadHeaderCart();
                            window.location = ""cart.html""
                        }
                    },
                    error: function (rs) {
                        alert(""Xảy ra lỗi khi cập nhật số lượng"");
                    }
                });
            });
        });
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<HShopping.ModelView.CartItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591