using UnityEngine;

namespace _Game.Scripts
{
public class Employee: MonoBehaviour
{
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    private void Awake()
    {
        ShownForm = new Form(Random.Range(1, 6));
        RealForm = new Form(Random.Range(1, 6));
    }
}
}