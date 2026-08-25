using _Game.Scripts.Interactive.Employees;
using _Game.Scripts.Interactive.Employees.Forms;
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
        builder.RegisterComponentInHierarchy<WorkingRoom>();
    }
}
}