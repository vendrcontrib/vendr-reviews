using Umbraco.Core.Composing;
using Umbraco.Web;
using Umbraco.Web.Trees;

namespace Vendr.Contrib.ProductReviews.Composing
{
    public class VendrProductReviewsComponent : IComponent
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public VendrProductReviewsComponent(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public void Initialize()
        {
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
                var id = Constants.Trees.Reviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.Reviews.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Web.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Web.Constants.Sections.Commerce);

                e.Nodes.Insert(index, reviewsNode);
            }
        }
    }
}