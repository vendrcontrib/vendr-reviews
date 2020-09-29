using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Vendr.Contrib.ProductReviews.Web.Dtos;
using Vendr.Core.Exceptions;
using Vendr.Core.Web.Api;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Contrib.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Web.Controllers
{
    public class ProductReviewController : SurfaceController, IRenderController
    {
        private readonly IVendrApi _vendrApi;
        private readonly IProductReviewService _productReviewService;

        public ProductReviewController(IVendrApi vendrAPi, IProductReviewService productReviewService)
        {
            _vendrApi = vendrAPi;
            _productReviewService = productReviewService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductReview(ProductReviewDto model)
        {
            try
            {
                using (var uow = _vendrApi.Uow.Create())
                {
                    var review = new ProductReview()
                    {
                        StoreId = model.StoreId,
                        ProductReference = model.ProductReference,
                        CustomerReference = model.CustomerReference,
                        Rating = model.Rating,
                        Title = model.Title,
                        Email = model.Email,
                        Name = model.Name,
                        Description = model.Description,
                        RecommendProduct = model.RecommendProduct
                    };

                    _productReviewService.AddProductReview(review);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to submit product review: " + ex.Message);

                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}