using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
//using Vendr.Core.Models;
using Vendr.Core.Web.Api;
using Vendr.ProductReviews.Models;
using Vendr.ProductReviews.Services;

namespace Vendr.Contrib.ProductReviews.Web.Controllers
{
    [PluginController("VendrProductReviews")]
    public class ProductReviewApiController : UmbracoAuthorizedApiController
    {
        //private readonly IVendrApi _vendrApi;
        private readonly IProductReviewService _productReviewService;

        public ProductReviewApiController(IProductReviewService productReviewService)
        {
            _productReviewService = productReviewService;
        }

        [HttpGet]
        public ProductReview GetProductReview(Guid id)
        {
            return _productReviewService.GetProductReview(id);
        }

        [HttpGet]
        public IEnumerable<ProductReview> GetProductReview(Guid[] ids)
        {
            return _productReviewService.GetProductReviews(ids);
        }

        [HttpGet]
        public PagedResult<ProductReview> GetProductReviews(Guid storeId, string productReference, long pageNumber = 1, int pageSize = 50)
        {
            long total;
            var items = _productReviewService.GetProductReviews(storeId, productReference, pageNumber, pageSize, out total);

            return new PagedResult<ProductReview>(total, pageNumber, pageSize)
            {
                Items = items
            };
        }

        [HttpGet]
        public PagedResult<ProductReview> GetProductReviewsForCustomer(Guid storeId, string customerReference, long pageNumber = 1, int pageSize = 50)
        {
            long total;
            var items = _productReviewService.GetProductReviewsForCustomer(storeId, customerReference, pageNumber, pageSize, out total);

            return new PagedResult<ProductReview>(total, pageNumber, pageSize)
            {
                Items = items
            };
        }

        [HttpGet]
        public PagedResult<ProductReview> SearchProductReviews(Guid storeId, long pageNumber = 1, int pageSize = 50, [FromUri] string[] statuses = null, string searchTerm = null)
        {
            long total;
            var items = _productReviewService.SearchProductReviews(storeId, pageNumber, pageSize, out total, statuses: statuses, searchTerm: searchTerm);

            return new PagedResult<ProductReview>(total, pageNumber, pageSize)
            {
                Items = items
            };
        }

        [HttpPost]
        public void SaveReview(ProductReview review)
        {
            _productReviewService.SaveProductReview(review);
        }

        [HttpDelete]
        public void DeleteReview(Guid id)
        {
            _productReviewService.DeleteProductReview(id);
        }
        
    }
}