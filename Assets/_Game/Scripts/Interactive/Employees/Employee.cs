using System;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Events;
using _Game.Scripts.Interactive.Employees.Forms;
using _Game.Scripts.Interactive.Employees.Traits;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    [SerializeField] private TraitsDatabase _traitsDatabase;
    // UI
    [SerializeField] private EmployeeUI _ui;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _moodImage;
    
    // Form
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    [field: SerializeField] public PersonalityConfig Trait { get; private set; }
    [field: SerializeField] public TraitConfig Disadvantage { get; private set; }
    [field: SerializeField] public TraitConfig Advantage { get; private set; }
    
    // Working
    [SerializeField] private bool _liedAboutForm;
    [SerializeField] private int _honestCoefficient;
    [SerializeField] private int _moodCoefficient;
    private float _currentProgress;
    private float _maxProgress;
    
    private Computer _computer;
    public event Action<Employee> OnLeave;
    public event Action<Employee, int> OnPaid;
    public event Action<Employee> OnFinishedTask;
    public event Action<Employee, EventConfig> OnEventStarted;
    public event Action OnMoneyGiven;
    
    protected override void Awake()
    {
        base.Awake();

        InitializeStats();
        
        gameObject.name = ShownForm.Name;
    }

    private void InitializeStats()
    {
        _honestCoefficient = Random.Range(1, 10);
        
        _maxProgress = 30f;
        _liedAboutForm = IsLying();
        
        _moodCoefficient = Random.Range(3, 8);
        
        RealForm = new Form(Random.Range(1, 6), Random.Range(18, 100));

        ShownForm = !_liedAboutForm? 
            RealForm : 
            new Form(Random.Range(RealForm.Efficiency, 6), Random.Range(RealForm.Age, 100));

        Trait = _traitsDatabase.Personalities[Random.Range(0, _traitsDatabase.Personalities.Length)];
        Disadvantage = _traitsDatabase.Disadvantages[Random.Range(0, _traitsDatabase.Disadvantages.Length)];
        Advantage = _traitsDatabase.Advantages[Random.Range(0, _traitsDatabase.Advantages.Length)];
    }
    
    private bool IsLying() => Random.Range(0, 11) > _honestCoefficient;

    public void Fire() => Leave();
    public void Kill() => Leave();
    public void SetEventIcon(Sprite sprite) => _computer.SetIcon(sprite);

    public void GiveSalary(int value)
    {
        if (DidntGetPaid(value))
        {
            AddMood(Trait.OnSalaryReaction);
        }
    }
    
    public void GiveMoney()
    {
        AddMood(Trait.OnMoneyReaction);
        OnMoneyGiven?.Invoke();
    }

    public void Cheer() => AddMood(Trait.OnCheerReaction);

    private bool DidntGetPaid(int value) => value == 0;

    public void AddMood(int value)
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
        _moodImage.fillAmount = _moodCoefficient / 10f;
    }

    private void Work(float timeDelta)
    {
        _currentProgress += timeDelta;
        if (!(_currentProgress >= _maxProgress)) return;
        _currentProgress = 0;
        
        FinishTask();
        
        TryStartEvent();
    }

    public void SetMaxProgress(int value)
    {
        _maxProgress = value;
    }

    private void TryStartEvent()
    {
        if (Random.Range(0, 10) < 8) return;
        
        switch (_moodCoefficient)
        {
            case <= 2:
                StartTraitEvent();
                break;
            case <= 5:
                StartDisadvantageEvent();
                break;
            case >= 8 and <= 10:
                StartAdvantageEvent();
                break;
        }
    }

    private void StartTraitEvent()
    {
        if (Trait.EventConfig == null) return;
        OnEventStarted?.Invoke(this, Trait.EventConfig);
    }

    private void StartDisadvantageEvent()
    {
        if (Disadvantage.EventConfig == null) return;
        OnEventStarted?.Invoke(this, Disadvantage.EventConfig);
    }

    private void StartAdvantageEvent()
    {
        if (Advantage.EventConfig == null) return;
        OnEventStarted?.Invoke(this, Advantage.EventConfig);
    }

    private void FinishTask()
    {
        OnPaid?.Invoke(this, IsLying() ? 0 : 100);
        OnFinishedTask?.Invoke(this);
        _maxProgress = 30;
    }

    public void SetComputer(Computer computer) => _computer = computer;

    private void Leave()
    {
        _computer.SetBusy(false);
        Destroy(gameObject);
        OnLeave?.Invoke(this);
    }
}
}