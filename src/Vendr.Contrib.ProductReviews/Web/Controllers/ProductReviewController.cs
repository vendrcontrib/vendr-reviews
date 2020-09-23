using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Vendr.Contrib.ProductReviews.Web.Dtos;
using Vendr.Core.Exceptions;
using Vendr.Core.Web.Api;
using Vendr.Contrib.ProductReviews.Services;

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
                    _productReviewService.AddProductReview(model.StoreId, "", "", model.Rating, model.Title, model.Name, model.Description);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to redeem discount code: " + ex.Message);

                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}