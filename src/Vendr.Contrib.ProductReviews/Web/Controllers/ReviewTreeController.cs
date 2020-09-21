using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Vendr.Core.Services;

namespace Vendr.Contrib.ProductReviews.Web.Controllers
{
    using Constants = Umbraco.Core.Constants;

    //[Tree("commerce", "review", TreeTitle = "Reviews", SortOrder = 10)]
    [PluginController("VendrProductReviews")]
    public class ReviewTreeController : TreeController
    {
        private readonly IStoreService _storeService;

        public ReviewTreeController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
            }
            else
            {
                var stores = _storeService.GetStores();

                if (id == "vendr")
                {
                    var mainRoute = "/commerce/vendr";

                    if (stores != null && stores.Any())
                    {
                        foreach (var store in stores)
                        {
                            var childNode = CreateTreeNode(Guid.NewGuid().ToString(), store.Id.ToString(), queryStrings, "Reviews", "icon-rate", false, $"{mainRoute}/view/reviews");
                            nodes.Add(childNode);
                        }
                    }
                }
            }

            return nodes;

            //this tree doesn't suport rendering more than 1 level
            throw new NotSupportedException();
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                menu.Items.Add(new CreateChildEntity(Services.TextService));
                // add refresh menu item (note no dialog)
                menu.Items.Add(new RefreshNode(Services.TextService, true));
                return menu;
            }

            // add a delete action to each individual item
            //menu.Items.Add<ActionDelete>(Services.TextService, true, opensDialog: true);

            return menu;
        }

        //protected override TreeNode CreateRootNode(FormDataCollection queryStrings)
        //{
        //    var root = base.CreateRootNode(queryStrings);
        //    root.

        //    root.Icon = "icon-rate";
        //    root.HasChildren = false;
        //    root.MenuUrl = null;

        //    return null;
        //}
    }
}