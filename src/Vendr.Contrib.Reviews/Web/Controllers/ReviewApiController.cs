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
using Vendr.Contrib.Reviews.Helpers;
using Vendr.Contrib.Reviews.Models;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Web.Dtos;
using Vendr.Contrib.Reviews.Web.Dtos.Mappers;
using Vendr.Core.Adapters;
using Notification = Umbraco.Web.Models.ContentEditing.Notification;

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    [PluginController("VendrReviews")]
    public class ReviewApiController : UmbracoAuthorizedApiController
    {
        private readonly IReviewService _reviewService;
        private readonly ILocalizedTextService _textService;
        private readonly IProductAdapter _productAdapter;

        public ReviewApiController(
            IReviewService reviewService,
            ILocalizedTextService textService,
            IProductAdapter productAdapter)
        {
            _reviewService = reviewService;
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
                var color = ReviewHelper.GetStatusColor(val);

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
            var entity = _reviewService.GetReview(id);

            return EntityMapper.ReviewEntityToEditDto(entity);
        }

        [HttpGet]
        public IEnumerable<ReviewDto> GetReviews(Guid[] ids)
        {
            return _reviewService.GetReviews(ids)
                .Select(x => EntityMapper.ReviewEntityToDto(x));
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForProduct(Guid storeId, string productReference, long pageNumber = 1, int pageSize = 50)
        {
            var result = _reviewService.GetReviewsForProduct(storeId, productReference, pageNumber, pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> GetReviewsForCustomer(Guid storeId, string customerReference, long pageNumber = 1, int pageSize = 50)
        {
            var result = _reviewService.GetReviewsForCustomer(storeId, customerReference, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpGet]
        public PagedResult<ReviewDto> SearchReviews(Guid storeId, [FromUri] ReviewStatus[] statuses = null, [FromUri] decimal[] ratings = null, string searchTerm = null, long pageNumber = 1, int pageSize = 50)
        {
            var result = _reviewService.SearchReviews(storeId, statuses: statuses, ratings: ratings, searchTerm: searchTerm, pageNumber: pageNumber, pageSize: pageSize);

            return new PagedResult<ReviewDto>(result.TotalItems, result.PageNumber, result.PageSize)
            {
                Items = result.Items.Select(x => EntityMapper.ReviewEntityToDto(x))
            };
        }

        [HttpPost]
        public ReviewEditDto SaveReview(ReviewSaveDto review)
        {
            Review entity;

            try
            {
                entity = review.Id != Guid.Empty
                    ? _reviewService.GetReview(review.Id)
                    : new Review(review.StoreId, review.ProductReference, review.CustomerReference);

                EntityMapper.ReviewSaveDtoToEntity(review, entity);

                entity = _reviewService.SaveReview(entity);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex));
            }

            var model = EntityMapper.ReviewEntityToEditDto(entity);

            model.Notifications.Add(new Notification(_textService.Localize("speechBubbles/operationSavedHeader"), 
                string.Empty, NotificationStyle.Success));

            return model;
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteReview(Guid id)
        {
            _reviewService.DeleteReview(id);
        }

        [HttpPost]
        public ReviewEditDto ChangeReviewStatus(ChangeReviewStatusDto model)
        {
            var entity = _reviewService.ChangeReviewStatus(model.ReviewId, model.Status);

            return EntityMapper.ReviewEntityToEditDto(entity);
        }

        [HttpPost]
        public CommentDto SaveComment(CommentDto comment)
        {
            var entity = comment.Id != Guid.Empty
                ? _reviewService.GetReview(comment.ReviewId).Comments.First(x => x.Id == comment.Id)
                : new Comment(comment.StoreId, comment.ReviewId);

            entity = EntityMapper.CommentDtoToEntity(comment, entity);

            _reviewService.SaveComment(entity);

            return EntityMapper.CommentEntityToDto(entity);
        }

        [HttpDelete]
        [HttpPost]
        public void DeleteComment(Guid id)
        {
            _reviewService.DeleteComment(id);
        }
    }
}