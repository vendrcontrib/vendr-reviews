using Umbraco.Core;
using Umbraco.Core.Composing;
using Vendr.Contrib.Reviews.Components;

namespace Vendr.Contrib.Reviews.Composers
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