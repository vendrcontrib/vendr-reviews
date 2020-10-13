using NPoco;
using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Vendr.Contrib.ProductReviews.Enums;

namespace Vendr.Contrib.ProductReviews.Persistence.Dtos
{
    [TableName(TableName)]
    [PrimaryKey("id", AutoIncrement = false)]
    [ExplicitColumns]
    public class ProductReviewDto
    {
        public const string TableName = Constants.DatabaseSchema.Tables.ProductReview;

        [Column("id")]
        [PrimaryKeyColumn]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Id { get; set; }

        [Column("storeId")]
        public Guid StoreId { get; set; }

        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        [Column("updateDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime UpdateDate { get; set; }

        [Column("status")]
        public ProductReviewStatus Status { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("productReference")]
        public string ProductReference { get; set; }

        [Column("customerReference")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string CustomerReference { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("verifiedBuyer")]
        public bool VerifiedBuyer { get; set; }

        [Column("recommendProduct")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public bool? RecommendProduct { get; set; }

        [ResultColumn]
        public List<CommentDto> Comments { get; set; }
    }
}