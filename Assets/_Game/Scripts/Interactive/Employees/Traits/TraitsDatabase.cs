using UnityEngine;

namespace _Game.Scripts.Interactive.Employees.Traits
{
[CreateAssetMenu(fileName = "TraitsDatabase", menuName = "Employees/TraitsDatabase")]
public class TraitsDatabase: ScriptableObject
{
    [field: SerializeField] public PersonalityConfig[] Personalities { get; private set; }
    [field: SerializeField] public TraitConfig[] Disadvantages { get; private set; }
    [field: SerializeField] public TraitConfig[] Advantages { get; private set; }
}
}