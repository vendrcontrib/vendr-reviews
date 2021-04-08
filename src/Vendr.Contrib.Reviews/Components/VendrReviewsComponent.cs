using Umbraco.Core.Composing;
using Umbraco.Web.Trees;
using Vendr.Contrib.Reviews.Api;

namespace Vendr.Contrib.Reviews.Components
{
    public class VendrReviewsComponent : IComponent
    {
        private readonly VendrReviewsApi _api;

        public VendrReviewsComponent(VendrReviewsApi api)
        {
            _api = api;
        }

        public void Initialize()
        {
            VendrReviewsApi.Instance = _api;

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
                var mainRoute = "commerce/vendrreviews";

                var storeId = e.QueryStrings["id"];
                var id = Constants.Trees.Reviews.Id;

                var reviewsNode = sender.CreateTreeNode(id, storeId, e.QueryStrings, "Reviews", Constants.Trees.Reviews.Icon, false, $"{mainRoute}/review-list/{storeId}");

                reviewsNode.Path = $"-1,{storeId},{id}";
                reviewsNode.NodeType = Constants.Trees.Reviews.NodeType;

                reviewsNode.AdditionalData.Add("storeId", storeId);
                reviewsNode.AdditionalData.Add("tree", Vendr.Web.Constants.Trees.Stores.Alias);
                reviewsNode.AdditionalData.Add("application", Vendr.Web.Constants.Sections.Commerce);

                var optNodeIndex = e.Nodes.FindIndex(x => x.NodeType == "Options");
                var index = optNodeIndex >= 0 ? optNodeIndex : e.Nodes.Count; 

                e.Nodes.Insert(index, reviewsNode);
            }
        }
    }
}