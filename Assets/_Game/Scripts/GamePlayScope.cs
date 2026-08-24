using VContainer;
using VContainer.Unity;

namespace _Game.Scripts
{
public class GamePlayScope: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<EmployeeSpawner>();
        builder.RegisterComponentInHierarchy<FormsUI>();
    }
}
}