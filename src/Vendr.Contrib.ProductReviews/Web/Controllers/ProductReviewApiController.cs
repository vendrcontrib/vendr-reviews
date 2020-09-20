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

        public ProductReviewApiController(/*IVendrApi vendrApi, */IProductReviewService productReviewService)
        {
            //_vendrApi = vendrApi;
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
        public PagedResult<ProductReview> GetPagedProductReviews(long pageNumber = 1, int pageSize = 50)
        {
            long total;
            var items = _productReviewService.GetPagedResults(pageNumber - 1, pageSize, out total);

            return new PagedResult<ProductReview>(total, pageNumber, pageSize)
            {
                Items = items
            };
        }
    }
}