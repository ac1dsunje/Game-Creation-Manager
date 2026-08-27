using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Traits
{
[CreateAssetMenu(fileName = "PersonalityConfig", menuName = "Employees/Personality")]
public class PersonalityConfig: TraitConfig
{
    [field: SerializeField] public int OnMoneyReaction { get; private set; }
    [field: SerializeField] public int OnSalaryReaction { get; private set; }
}
}