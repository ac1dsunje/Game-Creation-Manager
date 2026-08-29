using _Game.Scripts.Interactive.Employees.Events;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Traits
{
[CreateAssetMenu(fileName = "TraitConfig", menuName = "Employees/Trait")]
public class TraitConfig: ScriptableObject
{
    [field: SerializeField] public EventConfig EventConfig { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
}
}