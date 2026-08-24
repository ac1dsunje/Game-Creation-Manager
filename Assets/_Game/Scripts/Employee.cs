using UnityEngine;

namespace _Game.Scripts
{
public class Employee: MonoBehaviour
{
    [SerializeField] private int _honestCoefficient;
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    private void Awake()
    {
        _honestCoefficient = Random.Range(0, 10);
        
        ShownForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        RealForm = _honestCoefficient > 5 ? 
            ShownForm : 
            new Form(Random.Range(1, ShownForm.Experience + 1), Random.Range(ShownForm.Age, 100));
    }
}
}