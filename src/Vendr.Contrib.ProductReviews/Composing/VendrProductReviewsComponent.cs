using System;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Actions;
using Umbraco.Web.Trees;
using Vendr.Core.Services;

namespace Vendr.Contrib.ProductReviews.Composing
{
    public class VendrProductReviewsComponent : IComponent
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IStoreService _storeService;
        private readonly ILocalizedTextService _textService;

        public VendrProductReviewsComponent(
            IUmbracoContextFactory umbracoContextFactory,
            IStoreService storeService)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _storeService = storeService;
        }

        public void Initialize()
        {
            //TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        {
            // unsubscribe on shutdown
            //TreeControllerBase.MenuRendering -= TreeControllerBase_MenuRendering;
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;         
        }
        void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            if (sender.TreeAlias == "vendr")
            {
                var permissions = sender.Services.UserService.GetPermissions(sender.Security.CurrentUser, e.NodeId);

                if (permissions.AssignedPermissions.Any(x => x.Contains(ActionDelete.ActionLetter)) == false)
                {
                    return;
                }

                e.Menu.Items.Add<ActionDelete>(sender.Services.TextService, true, opensDialog: true);

                // creates a menu action that will open /umbraco/currentSection/itemAlias.html
                //var i = new Umbraco.Web.Models.Trees.ActionMenuItem .MenuItem("itemAlias", "Item name");

                //// optional, if you want to load a legacy page, otherwise it will follow convention
                //i.AdditionalData.Add("actionUrl", "my/long/url/to/webformshorror.aspx");

                //// optional, if you don't want to follow the naming conventions, but do want to use a angular view
                //// you can also use a direct path "../App_Plugins/my/long/url/to/view.html"
                //i.AdditionalData.Add("actionView", "my/long/url/to/view.html");

                //// sets the icon to icon-wine-glass
                //i.Icon = "wine-glass";

                //// insert at index 5
                //e.Menu.Items.Insert(5, i);
            }
        }

        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            // normally you will want to target a specific tree, this can be done by checking the
            // tree alias of by checking the tree type (casting 'sender')
            if (sender.TreeAlias == "vendr" && e.QueryStrings["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var index = e.Nodes.Count;
                var mainRoute = "commerce/vendrproductreviews";

                var storeId = e.QueryStrings["id"];
                var id = Constants.Trees.Reviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.Reviews.NodeType;

                //reviewsNode.AdditionalData.Add("uniqueId", id);
                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Web.Constants.Trees.Stores.Alias);

                e.Nodes.Insert(index, reviewsNode);
            }
        }
    }
}