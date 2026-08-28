using UnityEngine;

namespace _Game.Scripts.Interactive.Employees
{
[CreateAssetMenu(fileName = "WorkingConfig", menuName = "Employees/WorkingConfig")]
public class WorkingConfig: ScriptableObject
{
    [field: SerializeField] public int DefaultProgress { get; private set; }
    [field: SerializeField] public int MaxMood { get; private set; }
    [field: SerializeField] public int TraitEventMood { get; private set; }
    [field: SerializeField] public int DisadvantageEventMood { get; private set; }
    [field: SerializeField] public int AdvantageEventMood { get; private set; }
}
}