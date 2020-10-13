using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Vendr.Contrib.ProductReviews.Helpers;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Contrib.ProductReviews.Services;
using Vendr.Contrib.ProductReviews.Web.Dtos;
using Vendr.Contrib.ProductReviews.Web.Dtos.Mappers;
using Vendr.Core.Adapters;
using Notification = Umbraco.Web.Models.ContentEditing.Notification;

namespace Vendr.Contrib.ProductReviews.Web.Controllers
{
    [PluginController("VendrProductReviews")]
    public class ProductReviewApiController : UmbracoAuthorizedApiController
    {
        private readonly IProductReviewService _productReviewService;
        private readonly ILocalizedTextService _textService;
        private readonly IProductAdapter _productAdapter;

        public ProductReviewApiController(
            IProductReviewService productReviewService,
            ILocalizedTextService textService,
            IProductAdapter productAdapter)
        {
            _productReviewService = productReviewService;
            _textService = textService;
            _productAdapter = productAdapter;
        }

        [HttpGet]
        public IEnumerable<ReviewStatusDto> GetReviewStatuses()
        {
            var values = Enum.GetValues(typeof(ReviewStatus));

            var statuses = new List<ReviewStatusDto>();
            int sortOrder = 1;

            foreach (ReviewStatus val in values)
            {
                var name = val.ToString();
                var color = ProductReviewHelper.GetStatusColor(val);

                statuses.Add(new ReviewStatusDto
                {
                    Alias = name.ToLower(),
                    Color = color,
                    Id = (int)val,
                    Name = name,
                    SortOrder = sortOrder
                });

                sortOrder++;
            }

            return statuses;
        }

        [HttpGet]
        public Dictionary<string, string> GetProductData(string productReference, string languageIsoCode = null)
        {
            if (string.IsNullOrEmpty(languageIsoCode))
                languageIsoCode = Thread.CurrentThread.CurrentUICulture.Name;

            var snapshot = _productAdapter.GetProductSnapshot(productReference, languageIsoCode);
            if (snapshot == null)
                return null;

            return new Dictionary<string, string>
            {
                { "storeId", snapshot.StoreId.ToString() },
                { "sku", snapshot.Sku },
                { "name", snapshot.Name }
            };
        }

        [HttpGet]
        public ReviewEditDto GetReview(Guid id)
        {
            var entity = _productReviewService.GetReview(id);

            return ProductReviewMapper.ProductReviewEntityToEditDto(entity);
        }

        [HttpGet]
        public IEnumerable<ReviewDto> GetReviews(Guid[] ids)
        {
            return _productReviewService.GetReviews(ids)
                .Select(x => ProductReviewMapper.ProductReviewEntityToDto(x));
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForProduct(Guid storeId, string productReference, long pageNumber = 1, int pageSize = 50)
        {
            var result = _productReviewService.GetReviewsForProduct(storeId, productReference, pageNumber, pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => ProductReviewMapper.ProductReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForCustomer(Guid storeId, string customerReference, long pageNumber = 1, int pageSize = 50)
        {
            var result = _productReviewService.GetReviewsForCustomer(storeId, customerReference, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => ProductReviewMapper.ProductReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> SearchReviews(Guid storeId, [FromUri] ReviewStatus[] statuses = null, [FromUri] decimal[] ratings = null, string searchTerm = null, long pageNumber = 1, int pageSize = 50)
        {
            var result = _productReviewService.SearchReviews(storeId, statuses: statuses, ratings: ratings, searchTerm: searchTerm, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => ProductReviewMapper.ProductReviewEntityToDto(x))
            };
        }

        [HttpPost]
        public ReviewEditDto SaveReview(ReviewSaveDto review)
        {
            Review entity;

            try
            {
                entity = review.Id != Guid.Empty
                    ? _productReviewService.GetReview(review.Id)
                    : new Review(review.StoreId, review.ProductReference, review.CustomerReference);

                ProductReviewMapper.ProductReviewSaveDtoToEntity(review, entity);

                entity = _productReviewService.SaveReview(entity);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }

            var model = ProductReviewMapper.ProductReviewEntityToEditDto(entity);

            model.Notifications.Add(new Notification(_textService.Localize("speechBubbles/operationSavedHeader"), 
                string.Empty, NotificationStyle.Success));

            return model;
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteReview(Guid id)
        {
            _productReviewService.DeleteReview(id);
        }

        [HttpPost]
        public ReviewEditDto ChangeReviewStatus(ChangeReviewStatusDto model)
        {
            var entity = _productReviewService.ChangeReviewStatus(model.ReviewId, model.Status);

            return ProductReviewMapper.ProductReviewEntityToEditDto(entity);
        }

        [HttpPost]
        public CommentDto SaveComment(CommentDto comment)
        {
            var entity = comment.Id != Guid.Empty
                ? _productReviewService.GetReview(comment.ReviewId).Comments.First(x => x.Id == comment.Id)
                : new Comment(comment.StoreId, comment.ReviewId);

            entity = ProductReviewMapper.CommentDtoToEntity(comment, entity);

            _productReviewService.SaveComment(entity);

            return ProductReviewMapper.CommentEntityToDto(entity);
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteComment(Guid id)
        {
            _productReviewService.DeleteComment(id);
        }
    }
}