using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    [SerializeField] private EmployeeUI _ui;
    
    [SerializeField] private int _moodCoefficient;
    
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    private Computer _computer;
    
    protected override void Awake()
    {
        base.Awake();

        InitializeStats();
        
        gameObject.name = ShownForm.Name;
    }

    private void InitializeStats()
    {
        _moodCoefficient = Random.Range(3, 8);
        
        RealForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        ShownForm = Random.Range(0, 10) > 5 ? 
            RealForm : 
            new Form(Random.Range(RealForm.Efficiency, 6), Random.Range(RealForm.Age, 100));
    }

    public void Fire()
    {
        _computer.SetBusy(false);
        Destroy(gameObject);
    }

    private void Update()
    {
        _ui.SetInfo(ShownForm);
        if (_computer != null && _computer.IsOn)
        {
            Debug.Log($"{gameObject.name} is working");
        }
    }

    public void SetComputer(Computer computer) => _computer = computer;
}
}