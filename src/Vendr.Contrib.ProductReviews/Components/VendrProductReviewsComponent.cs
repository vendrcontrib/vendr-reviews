using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
using Vendr.Contrib.ProductReviews.Api;

namespace Vendr.Contrib.ProductReviews.Components
{
    public class VendrProductReviewsComponent : IComponent
    {
        private readonly ProductReviewsApi _api;

        public VendrProductReviewsComponent(ProductReviewsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            ProductReviewsApi.Instance = _api;

            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;
        }

        public void Terminate()
        {
            // Unsubscribe on shutdown
            TreeControllerBase.TreeNodesRendering -= TreeControllerBase_TreeNodesRendering;         
        }

        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (sender.TreeAlias == "vendr" && e.QueryStrings["nodeType"] == Vendr.Core.Constants.Entities.EntityTypes.Store)
            {
                var index = e.Nodes.Count;
                var mainRoute = "commerce/vendrproductreviews";

                var storeId = e.QueryStrings["id"];
                var id = Constants.Trees.ProductReviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.ProductReviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.ProductReviews.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Web.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Web.Constants.Sections.Commerce);

                e.Nodes.Insert(index, reviewsNode);
            }
        }
    }
}