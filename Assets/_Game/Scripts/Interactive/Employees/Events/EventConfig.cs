using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Events
{
[CreateAssetMenu(
    fileName = "EmployeeEvent",
    menuName = "Employees/Event Config"
)]
public class EventConfig : ScriptableObject
{
    [field: SerializeField] public EventType EventType { get; private set; }

    [field: SerializeField] public List<TraitReaction> Reactions { get; private set; } = new();
    [field: SerializeField] public int ChangeProgress { get; private set; }
    [field: SerializeField] public int KillAmount { get; private set; }
}

[Serializable]
public class TraitReaction
{
    [field: SerializeField] public TraitType Trait { get; private set; }

    [field: SerializeField] public int MoodChange { get; private set; }
}
}