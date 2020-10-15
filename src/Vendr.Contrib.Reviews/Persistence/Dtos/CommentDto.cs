using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;

namespace Vendr.Contrib.Reviews.Persistence.Dtos
{
    [TableName(TableName)]
    [PrimaryKey("id", AutoIncrement = false)]
    [ExplicitColumns]
    public class CommentDto
    {
        public const string TableName = Constants.DatabaseSchema.Tables.Comment;

        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = false)]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Id { get; set; }

        [Column("storeId")]
        public Guid StoreId { get; set; }

        [Column("reviewId")]
        public Guid ReviewId { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("createDate")]
        public DateTime CreateDate { get; set; }
    }
}