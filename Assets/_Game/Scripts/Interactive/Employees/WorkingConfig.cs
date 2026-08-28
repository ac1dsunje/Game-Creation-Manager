using UnityEngine;

namespace _Game.Scripts.Interactive.Employees
{
[CreateAssetMenu(fileName = "WorkingConfig", menuName = "Employees/WorkingConfig")]
public class WorkingConfig: ScriptableObject
{
    [field: SerializeField] public int DefaultProgress { get; private set; } = 15;
    [field: SerializeField] public int MaxMood { get; private set; } = 10;
}
}