using System;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Events;
using _Game.Scripts.Interactive.Employees.Forms;
using _Game.Scripts.Interactive.Employees.Traits;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    [Inject] private TraitsDatabase _traitsDatabase;
    
    // Form
    [field: SerializeField] public Form ShownForm { get; private set; }
    [field: SerializeField] public Form RealForm { get; private set; }
    
    // Working
    [Inject] private WorkingConfig _config;
    
    private int _honestCoefficient;
    public int MoodCoefficient { get; private set; }
    public float CurrentProgress { get; private set; }
    public float MaxProgress { get; private set; }

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
        MoodCoefficient = Random.Range(5, _config.MaxMood);
        
        MaxProgress = _config.DefaultProgress;
        
        RealForm = new Form(
            Random.Range(18, 45),
            _traitsDatabase.Personalities[Random.Range(0, _traitsDatabase.Personalities.Length)],
            _traitsDatabase.Disadvantages[Random.Range(0, _traitsDatabase.Disadvantages.Length)],
            _traitsDatabase.Advantages[Random.Range(0, _traitsDatabase.Advantages.Length)]
            );

        ShownForm = !IsLying()? 
            RealForm : 
            new Form(
                Random.Range(RealForm.Age, 45),
                _traitsDatabase.Personalities[Random.Range(0, _traitsDatabase.Personalities.Length)],
                _traitsDatabase.Disadvantages[Random.Range(0, _traitsDatabase.Disadvantages.Length)],
                _traitsDatabase.Advantages[Random.Range(0, _traitsDatabase.Advantages.Length)]
                );
    }
    
    private bool IsLying()
    {
        if (Random.Range(0, 11) > _honestCoefficient) return true;
        return Random.Range(0, 12) > MoodCoefficient;
    }

    public void Fire() => Leave();
    public void Kill() => Leave();
    public void SetEventIcon(Sprite sprite) => _computer.SetIcon(sprite);

    public void GiveMoney()
    {
        ChangeMood(RealForm.Trait.OnMoneyReaction);
        OnMoneyGiven?.Invoke();
    }

    public void Cheer() => ChangeMood(RealForm.Trait.OnCheerReaction);

    public void ChangeMood(int value) => MoodCoefficient = Mathf.Clamp(MoodCoefficient + value, 0, _config.MaxMood);

    private void Update()
    {
        if (_computer != null && _computer.IsOn)
        {
            Work(Time.deltaTime);
        }
    }

    private void Work(float timeDelta)
    {
        CurrentProgress += timeDelta;
        if (!(CurrentProgress >= MaxProgress)) return;
        CurrentProgress = 0;
        
        FinishTask();
        
        TryStartEvent();
    }

    public void SetMaxProgressScale(float value) => MaxProgress = _config.DefaultProgress / value;

    private void TryStartEvent()
    {
        if (Random.Range(0, 10) < 7) return;
        
        var value = Random.Range(0, MoodCoefficient + 1);
        
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
        if (RealForm.Trait.EventConfig == null) return;
        ShownForm.UpdateTrait(RealForm.Trait);
        OnEventStarted?.Invoke(this, RealForm.Trait.EventConfig);
    }

    private void StartDisadvantageEvent()
    {
        if (RealForm.Disadvantage.EventConfig == null) return;
        ShownForm.UpdateDisadvantage(RealForm.Disadvantage);
        OnEventStarted?.Invoke(this, RealForm.Disadvantage.EventConfig);
    }

    private void StartAdvantageEvent()
    {
        if (RealForm.Advantage.EventConfig == null) return;
        ShownForm.UpdateAdvantage(RealForm.Advantage);
        OnEventStarted?.Invoke(this, RealForm.Advantage.EventConfig);
    }

    private void FinishTask()
    {
        var earn = IsLying() ? 0 : 100;
        MoneyEarned += earn;
        TaskDone++;
        ChangeMood(RealForm.Trait.OnFinishedTaskReaction);
        OnPaid?.Invoke(this, earn);
        MaxProgress = _config.DefaultProgress;
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