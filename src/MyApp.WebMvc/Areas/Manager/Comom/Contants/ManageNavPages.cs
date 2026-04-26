using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MyApp.WebMvc.Areas.Manager.Comom.Contants
{

    public static class ManageNavPages
    {
        public static string Product => "Product";


        public static string Order => "Order";

        public static string Promotion => "Promotion";

        public static string ProductNavClass(ViewContext viewContext) => PageNavClass(viewContext, Product);
        public static string OrderNavClass(ViewContext viewContext) => PageNavClass(viewContext, Order);

        public static string PromotionNavClass(ViewContext viewContext) => PageNavClass(viewContext, Promotion);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "";
        }
    }
}
