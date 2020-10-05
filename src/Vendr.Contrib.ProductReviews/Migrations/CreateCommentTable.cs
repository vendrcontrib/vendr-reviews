using Umbraco.Core.Migrations;
using Vendr.Contrib.ProductReviews.Persistence.Dtos;

namespace Vendr.Contrib.ProductReviews.Migrations
{
    public class CreateCommentTable : MigrationBase
    {
        public CreateCommentTable(IMigrationContext context) : base(context) { }

        public override void Migrate()
        {
            if (TableExists(CommentDto.TableName)) return;
            Create.Table<CommentDto>().Do();
        }
    }
}