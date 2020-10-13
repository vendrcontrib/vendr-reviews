using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;

namespace Vendr.Contrib.ProductReviews.Persistence.Dtos
{
    [TableName(TableName)]
    [PrimaryKey("id", AutoIncrement = false)]
    [ExplicitColumns]
    public class CommentDto
    {
        public const string TableName = Constants.DatabaseSchema.Tables.ProductReviewComment;

        [Column("id")]
        [PrimaryKeyColumn]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Id { get; set; }

        [Column("storeId")]
        public Guid StoreId { get; set; }

        [Column("reviewId")]
        public Guid ReviewId { get; set; }

        [Column("createDate")]
        [Constraint(Default = SystemMethods.CurrentDateTime)]
        public DateTime CreateDate { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}