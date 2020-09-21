using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.ProductReviews.Models;

namespace Vendr.Contrib.ProductReviews.Factories
{
    internal static class ProductReviewFactory
    {
        public static ProductReview BuildProductReview(ProductReviewDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var review = new ProductReview(dto.Id)
            {
                CreateDate = dto.CreateDate,
                Rating = dto.Rating,
                Title = dto.Title,
                Name = dto.Name,
                Description = dto.Description,
                CustomerReference = dto.CustomerReference,
                ProductReference = dto.ProductReference
            };

            return review;
        }

        public static ProductReviewDto BuildProductReview(ProductReview review)
        {
            review.MustNotBeNull(nameof(review));

            var dto = new ProductReviewDto
            {
                Id = review.Id,
                CreateDate = review.CreateDate,
                Rating = review.Rating,
                Title = review.Title,
                Name = review.Name,
                Description = review.Description,
                CustomerReference = review.CustomerReference,
                ProductReference = review.ProductReference
            };

            return dto;
        }
    }
}