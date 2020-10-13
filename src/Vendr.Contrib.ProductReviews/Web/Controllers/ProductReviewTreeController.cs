using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace Vendr.Contrib.ProductReviews.Web.Controllers
{
    using Constants = Umbraco.Core.Constants;

    [Tree("commerce", "reviews", TreeTitle = "Reviews", SortOrder = 10, TreeUse = TreeUse.None)]
    [PluginController("VendrProductReviews")]
    public class ProductReviewTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {

            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            //MenuItem deleteAction = new MenuItem("delete", Services.TextService)
            //{
            //    Icon = "delete",
            //    OpensDialog = true,
            //};

            //deleteAction.AdditionalData.Add("parentId", id);
            //deleteAction.AdditionalData.Add("actionView", "/app_plugins/vendr/views/entity/delete.html");
            //deleteAction.NavigateToRoute("/path/to/view/delete/0");
            //menu.Items.Add(deleteAction);

            menu.Items.Add<ActionDelete>(Services.TextService).LaunchDialogView("/app_plugins/vendrproductreviews/backoffice/views/dialogs/delete.html", "Delete");
            //menu.Items.Add<ActionDelete>(Services.TextService, true, opensDialog: true);

            return menu;
        }

    }
}