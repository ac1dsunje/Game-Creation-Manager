using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: MonoBehaviour, IInteractable
{
    [SerializeField] private EmployeeUI _visual;
    
    [SerializeField] private int _honestCoefficient;
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    private bool _isInteracted;
    private Computer _computer;
    
    private void Awake()
    {
        _visual.gameObject.SetActive(false);
        
        _honestCoefficient = Random.Range(0, 10);
        
        ShownForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        RealForm = _honestCoefficient > 5 ? 
            ShownForm : 
            new Form(Random.Range(1, ShownForm.Experience + 1), Random.Range(ShownForm.Age, 100));
    }

    public void Interact()
    {
        _isInteracted = !_isInteracted;
        _visual.gameObject.SetActive(_isInteracted);
        _visual.SetInfo(ShownForm);
    }

    public void Fire()
    {
        _computer.SetBusy(false);
        Destroy(gameObject);
    }

    public void SetComputer(Computer computer)
    {
        _computer = computer;
    }
}
}