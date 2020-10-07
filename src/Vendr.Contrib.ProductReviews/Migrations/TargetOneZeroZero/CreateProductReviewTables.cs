using Umbraco.Core.Migrations;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;

namespace Vendr.Contrib.ProductReviews.Migrations.TargetOneZeroZero
{
    public class CreateProductReviewTables : MigrationBase
    {
        public CreateProductReviewTables(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            if (!TableExists(ProductReviewDto.TableName))
                Create.Table<ProductReviewDto>().Do();

            if (!TableExists(CommentDto.TableName))
                Create.Table<CommentDto>().Do();
        }
    }
}