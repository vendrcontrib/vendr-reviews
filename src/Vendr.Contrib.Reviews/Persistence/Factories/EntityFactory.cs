using System.Linq;
using Vendr.Contrib.Reviews.Models;
using Vendr.Contrib.Reviews.Persistence.Dtos;
using Vendr.Core;

namespace Vendr.Contrib.Reviews.Persistence.Factories
{
    internal static class EntityFactory
    {
        public static Review BuildEntity(ReviewDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var comments = dto.Comments?.Select(BuildEntity).ToList();

            var review = new Review(dto.Id, dto.StoreId, dto.ProductReference, dto.CustomerReference)
            {
                Rating = dto.Rating,
                Title = dto.Title,
                Email = dto.Email,
                Name = dto.Name,
                Body = dto.Body,
                VerifiedBuyer = dto.VerifiedBuyer,
                RecommendProduct = dto.RecommendProduct,
                Status = (ReviewStatus)dto.Status,
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                Comments = comments,
            };

            return review;
        }

        public static ReviewDto BuildDto(Review review)
        {
            review.MustNotBeNull(nameof(review));

            var dto = new ReviewDto
            {
                Id = review.Id,
                StoreId = review.StoreId,
                CreateDate = review.CreateDate,
                UpdateDate = review.UpdateDate,
                Status = (int)review.Status,
                Rating = review.Rating,
                Title = review.Title,
                Email = review.Email,
                Name = review.Name,
                Body = review.Body,
                CustomerReference = review.CustomerReference,
                ProductReference = review.ProductReference,
                VerifiedBuyer = review.VerifiedBuyer,
                RecommendProduct = review.RecommendProduct
            };

            return dto;
        }

        public static Comment BuildEntity(CommentDto dto)
        {
            dto.MustNotBeNull(nameof(dto));

            var review = new Comment(dto.Id, dto.StoreId, dto.ReviewId)
            {
                Body = dto.Body,
                CreateDate = dto.CreateDate
            };

            return review;
        }

        public static CommentDto BuildDto(Comment comment)
        {
            comment.MustNotBeNull(nameof(comment));

            var dto = new CommentDto
            {
                Id = comment.Id,
                StoreId = comment.StoreId,
                ReviewId = comment.ReviewId,
                Body = comment.Body,
                CreateDate = comment.CreateDate
            };

            return dto;
        }
    }
}