using System;
using System.Collections.Generic;
using _Game.Scripts.Interactive.Employees.Traits;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Events
{
[CreateAssetMenu(
    fileName = "EmployeeEvent",
    menuName = "Employees/Event Config"
)]
public class EventConfig : ScriptableObject
{
    [field: SerializeField] public List<TraitReaction> Reactions { get; private set; } = new();
    [field: SerializeField] public int ChangeProgress { get; private set; }
    [field: SerializeField] public int ColleaguesAmount { get; private set; }
    [field: SerializeField] public int KillAmount { get; private set; }
    [field: SerializeField] public bool Leave { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}

[Serializable]
public class TraitReaction
{
    [field: SerializeField] public PersonalityConfig Trait { get; private set; }

    [field: SerializeField] public int MoodChange { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}