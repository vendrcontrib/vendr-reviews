using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    [PluginController("VendrReviews")]
    [Tree("commerce", "reviews", TreeTitle = "Reviews", SortOrder = 10, TreeUse = TreeUse.None)]
    public class ReviewTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            menu.Items.Add<ActionDelete>(Services.TextService).LaunchDialogView("/app_plugins/vendrreviews/backoffice/views/dialogs/delete.html", "Delete");

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            throw new System.NotImplementedException();
        }
    }
}