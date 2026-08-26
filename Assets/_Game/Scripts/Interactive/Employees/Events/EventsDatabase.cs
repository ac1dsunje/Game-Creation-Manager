using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Events
{
[CreateAssetMenu(
    fileName = "EventsDatabase",
    menuName = "Employees/Events Database"
)]
public class EventsDatabase: ScriptableObject
{
    [field: SerializeField] public List<EventConfig> Events { get; private set; }
}
}