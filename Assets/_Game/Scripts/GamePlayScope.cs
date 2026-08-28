using _Game.Scripts.Boss;
using _Game.Scripts.Interactive.Analytics;
using _Game.Scripts.Interactive.Employees;
using _Game.Scripts.Interactive.Employees.Events;
using _Game.Scripts.Interactive.Employees.Traits;
using _Game.Scripts.Interactive.Tables;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts
{
public class GamePlayScope: LifetimeScope
{
    [SerializeField] private TraitsDatabase _traitsDatabase;
    [SerializeField] private GamePlayConfig _gamePlayConfig;
    [SerializeField] private EventsDatabase _eventsDatabase;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_traitsDatabase);
        builder.RegisterInstance(_gamePlayConfig);
        builder.RegisterInstance(_eventsDatabase);
        
        builder.RegisterComponentInHierarchy<EmployeeSpawner>();
        builder.RegisterComponentInHierarchy<FormsUI>();
        builder.RegisterComponentInHierarchy<WorkingRoom>();
        builder.RegisterComponentInHierarchy<Table>();
        builder.RegisterComponentInHierarchy<AnalyticsTable>();
        builder.RegisterComponentInHierarchy<BossController>();
    }
}
}