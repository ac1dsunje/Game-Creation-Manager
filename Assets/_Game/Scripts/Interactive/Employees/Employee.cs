using System;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;
using UnityEngine.UI;
using EventType = _Game.Scripts.Interactive.Employees.Events.EventType;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Interactive.Employees
{
public class Employee: InteractiveObject
{
    // UI
    [SerializeField] private EmployeeUI _ui;
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _moodImage;
    
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
    public event Action<Employee> OnLeave;
    public event Action<Employee, int> OnPaid;
    public event Action<Employee> OnFinishedTask;
    public event Action<Employee, EventType> OnEventStarted;
    public event Action OnMoneyGiven;
    
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

    public void Fire() => Leave();
    public void Kill() => Leave();

    public void GiveSalary(int value)
    {
        if (DidntGetPaid(value))
        {
            switch (Trait)
            {
                case TraitType.Psycho:
                case TraitType.Narciss:
                    AddMood(-1);
                    break;
                case TraitType.Worker:
                    AddMood(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    public void GiveMoney()
    {
        switch (Trait)
        {
            case TraitType.Psycho:
                AddMood(1);
                break;
            case TraitType.Narciss:
                AddMood(-1);
                break;
            case TraitType.Worker:
                AddMood(1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        OnMoneyGiven?.Invoke();
    }

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
        switch (Trait)
        {
            case TraitType.Psycho:
                OnEventStarted?.Invoke(this, EventType.Kill);
                break;
            case TraitType.Narciss or  TraitType.Worker:
                break;
        }

        Leave();
    }

    private void StartDisadvantageEvent()
    {
        switch (Disadvantage)
        {
            case DisadvantageType.FartGuy:
                OnEventStarted?.Invoke(this, EventType.Fart);
                break;
            case DisadvantageType.Loud:
                OnEventStarted?.Invoke(this, EventType.Scream);
                break;
            case DisadvantageType.LowEfficiency:
                OnEventStarted?.Invoke(this, EventType.LowEfficiency);
                break;
            case DisadvantageType.Sick:
                OnEventStarted?.Invoke(this, EventType.Sneeze);
                break;
            case DisadvantageType.CryBaby:
                OnEventStarted?.Invoke(this, EventType.Cry);
                break;
            case DisadvantageType.HeartProblems:
                OnEventStarted?.Invoke(this, EventType.Insult);
                break;
        }
    }

    private void StartAdvantageEvent()
    {
        switch (Advantage)
        {
            case AdvantageType.HighEfficiency:
                OnEventStarted?.Invoke(this, EventType.HighEfficiency);
                break;
            case AdvantageType.JBL:
                OnEventStarted?.Invoke(this, EventType.Music);
                break;
        }
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