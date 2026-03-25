using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MyApp.WebMvc.Areas.Identity.Pages.Models
{

    public static class AdminManageNavPages
    {

        public static string User => "User";


        public static string Role => "Role";

      
        public static string UserNavClass(ViewContext viewContext) => PageNavClass(viewContext, User);
        public static string RoleNavClass(ViewContext viewContext) => PageNavClass(viewContext, Role);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
