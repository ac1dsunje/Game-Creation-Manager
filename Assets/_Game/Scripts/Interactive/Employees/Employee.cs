using System;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    // UI
    [SerializeField] private EmployeeUI _ui;
    [SerializeField] private Image _progressImage;
    
    // Form
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    [field: SerializeField] public TraitType Trait { get; private set; }
    [field: SerializeField] public DisadvantageType Disadvantage { get; private set; }
    [field: SerializeField] public AdvantageType Advantage { get; private set; }
    
    // Working
    [SerializeField] private bool _liedAboutForm;
    [SerializeField] private int _honestCoefficient;
    [SerializeField] private int _moodCoefficient;
    private float _currentProgress;
    private float _maxProgress;
    
    private Computer _computer;
    public event Action<Employee> OnDeath;
    public event Action<Employee, int> OnPaid;
    public event Action<Employee> OnFinishedTask;
    
    protected override void Awake()
    {
        base.Awake();

        InitializeStats();
        
        gameObject.name = ShownForm.Name;
    }

    private void InitializeStats()
    {
        _honestCoefficient = Random.Range(0, 10);
        
        _maxProgress = 30f;
        _liedAboutForm = IsLying();
        
        _moodCoefficient = Random.Range(3, 8);
        
        RealForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        ShownForm = !_liedAboutForm? 
            RealForm : 
            new Form(Random.Range(RealForm.Efficiency, 6), Random.Range(RealForm.Age, 100));

        Trait = (TraitType)Random.Range(0, Enum.GetValues(typeof(TraitType)).Length);
        Disadvantage = (DisadvantageType)Random.Range(0, Enum.GetValues(typeof(DisadvantageType)).Length);
        Advantage = (AdvantageType)Random.Range(0, Enum.GetValues(typeof(AdvantageType)).Length);
    }
    
    private bool IsLying() => Random.Range(0, 11) > _honestCoefficient;

    public void Fire() => Die();

    public void GiveMoney(int value)
    {
        if (value == 0)
        {
            AddMood(-1);
        }
    }

    private void AddMood(int value)
    {
        _moodCoefficient += value;
        _moodCoefficient = Mathf.Clamp(_moodCoefficient, 0, 10);
    }

    private void Update()
    {
        _ui.SetInfo(ShownForm);
        if (_computer != null && _computer.IsOn)
        {
            Work(Time.deltaTime);
        }
        _progressImage.fillAmount = _currentProgress / _maxProgress;
    }

    private void Work(float timeDelta)
    {
        _currentProgress += timeDelta;
        if (!(_currentProgress >= _maxProgress)) return;
        _currentProgress = 0;
        
        if (IsLying())
        {
            if (Random.value > 0.5f)
            {
                OnPaid?.Invoke(this, 100);
            }
        }
        else
        {
            OnPaid?.Invoke(this, 100);
        }
        OnFinishedTask?.Invoke(this);
    }

    public void SetComputer(Computer computer) => _computer = computer;

    private void Die()
    {
        _computer.SetBusy(false);
        Destroy(gameObject);
        OnDeath?.Invoke(this);
    }
}
}