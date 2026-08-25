using _Game.Scripts.Interactive.Computers;
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
    
    private bool _isActive;
    private Computer _computer;
    
    private void Awake()
    {
        _visual.gameObject.SetActive(false);
        
        _honestCoefficient = Random.Range(0, 10);
        
        ShownForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        RealForm = _honestCoefficient > 5 ? 
            ShownForm : 
            new Form(Random.Range(1, ShownForm.Experience + 1), Random.Range(ShownForm.Age, 100));
        
        gameObject.name = ShownForm.Name;
    }

    public void Interact()
    {
        _isActive = !_isActive;
        _visual.gameObject.SetActive(_isActive);
    }

    public void Fire()
    {
        _computer.SetBusy(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        _visual.SetInfo(ShownForm);
        if (_computer != null && _computer.IsOn)
        {
            Debug.Log($"{gameObject.name} is working");
        }
    }

    public void SetComputer(Computer computer) => _computer = computer;
}
}