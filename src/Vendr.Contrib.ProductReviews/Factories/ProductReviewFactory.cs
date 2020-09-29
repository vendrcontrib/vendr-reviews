using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;
using Vendr.Contrib.ProductReviews.Models;
using System;

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

        public static Comment BuildComment(CommentDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var review = new Comment(dto.Id)
            {
                StoreId = dto.StoreId,
                ReviewId = dto.ReviewId,
                CreateDate = dto.CreateDate,
                Description = dto.Description
            };

            return review;
        }

        public static CommentDto BuildComment(Comment comment)
        {
            comment.MustNotBeNull(nameof(comment));

            var dto = new CommentDto
            {
                Id = comment.Id,
                StoreId = comment.StoreId,
                ReviewId = comment.ReviewId,
                CreateDate = comment.CreateDate,       
                Description = comment.Description
            };

            return dto;
        }
    }
}