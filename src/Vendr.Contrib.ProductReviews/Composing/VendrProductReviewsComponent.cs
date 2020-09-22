using System;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Trees;
using Vendr.Core.Services;

namespace Vendr.Contrib.ProductReviews.Composing
{
    public class VendrProductReviewsComponent : IComponent
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private readonly IStoreService _storeService;

        public VendrProductReviewsComponent(IUmbracoContextFactory umbracoContextFactory, IStoreService storeService)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _storeService = storeService;
        }

        public void Initialize()
        {
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        {
            // unsubscribe on shutdown
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;
        }

        // the event listener method:
        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            // normally you will want to target a specific tree, this can be done by checking the
            // tree alias of by checking the tree type (casting 'sender')
            if (sender.TreeAlias == "vendr" && e.QueryStrings["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var index = e.Nodes.Count; // e.Nodes.Count - 1;
                var mainRoute = "commerce/vendrproductreviews";

                var storeId = e.QueryStrings["id"];
                var id = "100"; //Guid.NewGuid().ToString();

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.Icons.Review, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = "Review";

                //reviewsNode.AdditionalData.Add("uniqueId", id);
                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Web.Constants.Trees.Stores.Alias);

                e.Nodes.Insert(index, reviewsNode);

                //var stores = e.Nodes.Where(n => n.NodeType == Vendr.Core.Constants.Entities.EntityTypes.Store).ToList();

                //foreach (var node in e.Nodes)
                //{
                //    var parentId = node.Id.ToString();
                //    if (parentId == "b1e61994-b83b-420a-903e-63a7a15942dc")
                //    {
                //        var id = Guid.NewGuid().ToString();
                //        var childNode = sender.CreateTreeNode(id, parentId, e.QueryStrings, "Reviews", "icon-rate", false, $"{mainRoute}/review-list/{id}");
                //        childNode.Path = $"-1,{parentId},{id}";
                //        childNode.NodeType = "Review";

                //        e.Nodes.Insert(index, childNode);
                //    }
                //}

                //var stores = e.Nodes.Where(n => n.NodeType == "Store").ToList();

                //if (stores != null && stores.Any())
                //{
                //    var mainRoute = "commerce/vendr";

                //    foreach (var store in stores)
                //    {
                //        var parentId = store.Id.ToString();
                //        var id = Guid.NewGuid().ToString();
                //        var childNode = sender.CreateTreeNode(id, parentId, e.QueryStrings, "Reviews", "icon-rate", false, $"{mainRoute}/reviews");
                //        childNode.Path = $"-1,{parentId},{id}";
                //        e.Nodes.Insert(0, childNode);
                //    }
                //}

                //var stores = _storeService.GetStores();
                //if (stores != null && stores.Any())
                //{
                //    var mainRoute = "/commerce/vendr";

                //    foreach (var store in stores)
                //    {
                //        var childNode = sender.CreateTreeNode("reviews", store.Id.ToString(), e.QueryStrings, "Reviews", "icon-rate", false, $"{mainRoute}/reviews");
                //        e.Nodes.Insert(e.Nodes.Count - 1, childNode);
                //    }
                //}

            }
        }
    }
}