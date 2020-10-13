using Umbraco.Core.Migrations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Umbraco.Core.Persistence.SqlSyntax;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;

namespace Vendr.Contrib.ProductReviews.Migrations.V_1_0_0
{
    public class CreateProductReviewCommentTable : MigrationBase
    {
        public CreateProductReviewCommentTable(IMigrationContext context) 
            : base(context) 
        { }

        public override void Migrate()
        {
            var commentTableName = CommentDto.TableName;
            var reviewTableName = ReviewDto.TableName;
            var storeTableName = Core.Constants.DatabaseSchema.Tables.Store;

            if (!TableExists(commentTableName))
            {
                var nvarcharMaxType = SqlSyntax is SqlCeSyntaxProvider
                    ? "NTEXT"
                    : "NVARCHAR(MAX)";

                // Create table
                Create.Table(commentTableName)
                    .WithColumn("id").AsGuid().NotNullable().WithDefault(SystemMethods.NewGuid).PrimaryKey($"PK_{reviewTableName}")
                    .WithColumn("storeId").AsGuid().NotNullable()
                    .WithColumn("reviewId").AsGuid().NotNullable()
                    .WithColumn("body").AsCustom(nvarcharMaxType).NotNullable()
                    .WithColumn("createDate").AsDateTime().NotNullable()
                    .Do();

                // Foreign key constraints
                Create.ForeignKey($"FK_{commentTableName}_{storeTableName}")
                    .FromTable(commentTableName).ForeignColumn("storeId")
                    .ToTable(storeTableName).PrimaryColumn("id")
                    .Do();

                Create.ForeignKey($"FK_{commentTableName}_{reviewTableName}")
                    .FromTable(commentTableName).ForeignColumn("reviewId")
                    .ToTable(reviewTableName).PrimaryColumn("id")
                    .Do();
            }
        }
    }
}