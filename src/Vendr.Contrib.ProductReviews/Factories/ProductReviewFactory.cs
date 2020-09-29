using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.Contrib.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Factories
{
    internal static class ProductReviewFactory
    {
        public static ProductReview BuildProductReview(ProductReviewDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var review = new ProductReview(dto.Id)
            {
                StoreId = dto.StoreId,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                Status = dto.Status,
                Rating = dto.Rating,
                Title = dto.Title,
                Email = dto.Email,
                Name = dto.Name,
                Description = dto.Description,
                CustomerReference = dto.CustomerReference,
                ProductReference = dto.ProductReference,
                VerifiedBuyer = dto.VerifiedBuyer,
                RecommendProduct = dto.RecommendProduct
            };

            return review;
        }

        public static ProductReviewDto BuildProductReview(ProductReview review)
        {
            review.MustNotBeNull(nameof(review));

            var dto = new ProductReviewDto
            {
                Id = review.Id,
                StoreId = review.StoreId,
                CreateDate = review.CreateDate,
                UpdateDate = review.UpdateDate,
                Status = review.Status,
                Rating = review.Rating,
                Title = review.Title,
                Email = review.Email,
                Name = review.Name,
                Description = review.Description,
                CustomerReference = review.CustomerReference,
                ProductReference = review.ProductReference,
                VerifiedBuyer = review.VerifiedBuyer,
                RecommendProduct = review.RecommendProduct
            };

            return dto;
        }
    }
}