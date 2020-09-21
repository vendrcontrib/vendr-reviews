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
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_RootNodeRendering;
        }

        public void Terminate()
        {
            // unsubscribe on shutdown
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_RootNodeRendering;
        }

        // the event listener method:
        void TreeControllerBase_RootNodeRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            // normally you will want to target a specific tree, this can be done by checking the
            // tree alias of by checking the tree type (casting 'sender')
            if (sender.TreeAlias == "vendr")
            {
                var mainRoute = "commerce/vendr";

                var stores = e.Nodes.Where(n => n.NodeType == Vendr.Core.Constants.Entities.EntityTypes.Store).ToList();

                foreach (var node in stores)
                {
                    var parentId = node.Id.ToString();
                    if (parentId == "b1e61994-b83b-420a-903e-63a7a15942dc")
                    {
                        var id = Guid.NewGuid().ToString();
                        var childNode = sender.CreateTreeNode(id, parentId, e.QueryStrings, "Reviews", "icon-rate", false, $"{mainRoute}/reviews/{id}");
                        childNode.Path = $"-1,{parentId},{id}";
                        e.Nodes.Insert(e.Nodes.Count - 1, childNode);
                    }
                }

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