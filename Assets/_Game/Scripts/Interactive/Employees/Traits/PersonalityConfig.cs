using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Traits
{
[CreateAssetMenu(fileName = "PersonalityConfig", menuName = "Employees/Personality")]
public class PersonalityConfig: TraitConfig
{
    [field: SerializeField, Range(-1, 2)] public int OnMoneyReaction { get; private set; }
    [field: SerializeField, Range(-1, 2)] public int OnCheerReaction { get; private set; }
}
}