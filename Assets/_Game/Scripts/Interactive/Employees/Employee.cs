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
    [SerializeField] private int _honestCoefficient;
    [SerializeField] private int _defaultProgress = 15;
    [SerializeField] private int _maxMood = 10;
    
    private int _moodCoefficient;
    private float _currentProgress;
    private float _maxProgress;

    public bool IsHired => _computer != null;
    public int TaskDone { get; private set; }
    public int MoneyEarned { get; private set; }
    
    private Computer _computer;
    public event Action<Employee> OnLeave;
    public event Action<Employee, int> OnPaid;
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
        _moodCoefficient = Random.Range(5, _maxMood);
        
        _maxProgress = _defaultProgress;
        
        RealForm = new Form(Random.Range(18, 100));

        ShownForm = !IsLying()? 
            RealForm : 
            new Form(Random.Range(RealForm.Age, 100));

        Trait = _traitsDatabase.Personalities[Random.Range(0, _traitsDatabase.Personalities.Length)];
        Disadvantage = _traitsDatabase.Disadvantages[Random.Range(0, _traitsDatabase.Disadvantages.Length)];
        Advantage = _traitsDatabase.Advantages[Random.Range(0, _traitsDatabase.Advantages.Length)];
    }
    
    private bool IsLying()
    {
        if (Random.Range(0, 11) > _honestCoefficient) return true;
        return Random.Range(0, 12) > _moodCoefficient;
    }

    public void Fire() => Leave();
    public void Kill() => Leave();
    public void SetEventIcon(Sprite sprite)
    {
        _computer.SetIcon(sprite);
    }

    public void GiveMoney()
    {
        ChangeMood(Trait.OnMoneyReaction);
        OnMoneyGiven?.Invoke();
    }

    public void Cheer() => ChangeMood(Trait.OnCheerReaction);

    public void ChangeMood(int value)
    {
        _moodCoefficient += value;
        _moodCoefficient = Mathf.Clamp(_moodCoefficient, 0, _maxMood);
    }

    private void Update()
    {
        _ui.SetInfo(ShownForm);
        if (_computer != null && _computer.IsOn)
        {
            Work(Time.deltaTime);
        }
        _progressImage.fillAmount = _currentProgress / _maxProgress;
        _moodImage.fillAmount = (float)_moodCoefficient / _maxMood;
    }

    private void Work(float timeDelta)
    {
        _currentProgress += timeDelta;
        if (!(_currentProgress >= _maxProgress)) return;
        _currentProgress = 0;
        
        FinishTask();
        
        TryStartEvent();
    }

    public void SetMaxProgressScale(float value) => _maxProgress = value * _defaultProgress;

    private void TryStartEvent()
    {
        if (Random.Range(0, 10) < 7) return;
        
        var value = Random.Range(0, _moodCoefficient + 1);
        
        switch (value)
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
        var earn = IsLying() ? 0 : 100;
        MoneyEarned += earn;
        TaskDone++;
        ChangeMood(Trait.OnFinishedTaskReaction);
        OnPaid?.Invoke(this, earn);
        _maxProgress = _defaultProgress;
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