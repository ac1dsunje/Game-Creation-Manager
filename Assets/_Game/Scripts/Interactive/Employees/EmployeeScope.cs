using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Interactive.Employees
{
public class EmployeeScope: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(GetComponent<Employee>());
        builder.RegisterComponent(GetComponentInChildren<EmployeeUI>());
    }
}
}