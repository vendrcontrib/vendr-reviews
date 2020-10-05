using Umbraco.Core.Migrations;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;

namespace Vendr.Contrib.ProductReviews.Migrations
{
    public class CreateProductReviewTable : MigrationBase
    {
        public CreateProductReviewTable(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            if (TableExists(ProductReviewDto.TableName)) return;
            Create.Table<ProductReviewDto>().Do();
        }
    }
}