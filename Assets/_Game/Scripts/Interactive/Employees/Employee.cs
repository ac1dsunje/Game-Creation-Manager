using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    [SerializeField] private EmployeeUI _ui;
    
    [SerializeField] private int _honestCoefficient;
    [SerializeField] private int _loyaltyCoefficient;
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
        _honestCoefficient = Random.Range(0, 10);
        _loyaltyCoefficient = Random.Range(0, 10);
        _moodCoefficient = Random.Range(0, 10);
        
        RealForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        ShownForm = _honestCoefficient > 5 ? 
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