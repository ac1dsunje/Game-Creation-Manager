using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.Interactive.Employees
{
public class EmployeeScope: LifetimeScope
{
    [SerializeField] private WorkingConfig _config;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_config);
        
        builder.RegisterComponent(GetComponent<Employee>());
        builder.RegisterComponent(GetComponentInChildren<EmployeeUI>());
    }
}
}