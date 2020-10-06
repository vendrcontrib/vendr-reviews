using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.ProductReviews.Components;

namespace Vendr.Contrib.ProductReviews.Composers
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class MigrationComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<MigrationComponent>();
        }
    }
}