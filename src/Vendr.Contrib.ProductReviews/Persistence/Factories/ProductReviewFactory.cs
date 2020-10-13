using System;
using System.Linq;
using Vendr.Contrib.ProductReviews.Enums;
using Vendr.Contrib.ProductReviews.Helpers;
using Vendr.Contrib.ProductReviews.Models;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;
using Vendr.Core;

namespace Vendr.Contrib.ProductReviews.Persistence.Factories
{
    internal static class ProductReviewFactory
    {
        public static ProductReview BuildProductReview(ProductReviewDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var comments = dto.Comments?.Select(x => new Comment
            {
                Id = x.Id,
                ReviewId = x.ReviewId,
                StoreId = x.StoreId,
                CreateDate = x.CreateDate,
                Description = x.Description
            }).ToList();

            var status = BuildStatus(dto);

            var review = new ProductReview(dto.Id)
            {
                StoreId = dto.StoreId,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                Status = status,
                Rating = dto.Rating,
                Title = dto.Title,
                Email = dto.Email,
                Name = dto.Name,
                Description = dto.Description,
                CustomerReference = dto.CustomerReference,
                ProductReference = dto.ProductReference,
                VerifiedBuyer = dto.VerifiedBuyer,
                RecommendProduct = dto.RecommendProduct,
                Comments = comments
            };

            return review;
        }

        public static ProductReviewDto BuildProductReview(ProductReview review)
        {
            review.MustNotBeNull(nameof(review));

            var status = Enum.TryParse(review.Status?.Id.ToString(), out ProductReviewStatus s) ? s : default(ProductReviewStatus);

            var dto = new ProductReviewDto
            {
                Id = review.Id,
                StoreId = review.StoreId,
                CreateDate = review.CreateDate,
                UpdateDate = review.UpdateDate,
                Status = status,
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

        private static Status BuildStatus(ProductReviewDto dto)
        {
            var name = dto.Status.ToString();
            var color = ProductReviewHelper.GetStatusColor(dto.Status);

            return new Status
            {
                Alias = name.ToLower(),
                Id = (int)dto.Status,
                Color = color,
                Name = name,
                SortOrder = 0,
                StoreId = dto.StoreId
            };
        }
    }
}