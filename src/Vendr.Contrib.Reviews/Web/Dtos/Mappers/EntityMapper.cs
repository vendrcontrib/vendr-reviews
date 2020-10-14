using System.Collections.Generic;
using System.Linq;
using Vendr.Contrib.Reviews.Helpers;
using Vendr.Contrib.Reviews.Models;

namespace Vendr.Contrib.Reviews.Web.Dtos.Mappers
{
    public static class EntityMapper
    {
        public static ReviewDto ReviewEntityToDto(Review entity, ReviewDto dto = null)
        {
            if (entity == null) return null;

            dto = dto ?? new ReviewDto();

            dto.Id = entity.Id;
            dto.StoreId = entity.StoreId;
            dto.ProductReference = entity.ProductReference;
            dto.CustomerReference = entity.CustomerReference;
            dto.Rating = entity.Rating;
            dto.Title = entity.Title;
            dto.Email = entity.Email;
            dto.Name = entity.Name;
            dto.Body = entity.Body;
            dto.VerifiedBuyer = entity.VerifiedBuyer;
            dto.RecommendProduct = entity.RecommendProduct;
            dto.CreateDate = entity.CreateDate;
            dto.UpdateDate = entity.UpdateDate;
            dto.Status = ReviewStatusToDto(entity.Status);

            dto.Comments = entity.Comments?.Select(x => CommentEntityToDto(x)).ToList() ?? new List<CommentDto>();

            return dto;
        }

        public static ReviewEditDto ReviewEntityToEditDto(Review entity, ReviewEditDto dto = null)
        {
            if (entity == null) return null;

            dto = dto ?? new ReviewEditDto();

            ReviewEntityToDto(entity, dto);

            return dto;
        }

        public static Review ReviewSaveDtoToEntity(ReviewSaveDto dto, Review entity)
        {
            entity.Name = dto.Name;
            entity.ProductReference = dto.ProductReference;
            entity.CustomerReference = dto.CustomerReference;
            entity.Rating = dto.Rating;
            entity.Title = dto.Title;
            entity.Name = dto.Name;
            entity.Email = dto.Email;
            entity.Body = dto.Body;
            entity.VerifiedBuyer = dto.VerifiedBuyer;
            entity.RecommendProduct = dto.RecommendProduct;

            return entity;
        }

        public static ReviewStatusDto ReviewStatusToDto(ReviewStatus reviewStatus)
        {
            var name = reviewStatus.ToString();
            var color = ReviewHelper.GetStatusColor(reviewStatus);

            return new ReviewStatusDto
            {
                Alias = name.ToLower(),
                Id = (int)reviewStatus,
                Color = color,
                Name = name,
                SortOrder = 0
            };
        }

        public static CommentDto CommentEntityToDto(Comment entity, CommentDto dto = null)
        {
            if (entity == null)
                return null;

            dto = dto ?? new CommentDto();

            dto.Id = entity.Id;
            dto.StoreId = entity.StoreId;
            dto.ReviewId = entity.ReviewId;
            dto.Body = entity.Body;
            dto.CreateDate = entity.CreateDate;

            return dto;
        }

        public static Comment CommentDtoToEntity(CommentDto dto, Comment entity)
        {
            entity.Body = dto.Body;

            return entity;
        }
    }
}