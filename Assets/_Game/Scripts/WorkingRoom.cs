using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Boss;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees;
using _Game.Scripts.Interactive.Employees.Events;
using UnityEngine;
using VContainer;

namespace _Game.Scripts
{
public class WorkingRoom: MonoBehaviour
{
    [SerializeField] private List<Computer> _computers;
    private readonly List<Employee> _employees = new();
    [SerializeField] private SoundData _computerOnData;

    public List<Employee> GetHiredEmployees() => _employees.Where(employee => employee.IsHired).ToList();

    [Inject] private BossController _boss;
    [Inject] private EventsDatabase _eventsDatabase;
    [Inject] private GamePlayConfig _config;
    [Inject] private AudioManager _audioManager;
    
    private float _timer;

    private void Awake()
    {
        foreach (var computer in _computers)
        {
            computer.OnChange += OnComputerStateChanged;
        }
    }

    private void OnComputerStateChanged(Computer computer)
    {
        if (computer.IsOn)
        {
            _audioManager.PlaySound(_computerOnData, computer.transform.position);
        }
    }

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
        employee.OnLeave += OnEmployeeLeave;
        employee.OnPaid += OnEmployeePaid;
        employee.OnEventStarted += OnEventStarted;
        employee.OnMoneyGiven += OnMoneyGiven;

        foreach (var computer in _computers)
        {
            if (computer.IsBusy) continue;
            employee.transform.SetParent(computer.transform);
            
            var position = computer.transform.position;
            
            employee.transform.position = new Vector2(position.x, position.y - 0.5f);
            
            computer.SetBusy(true);
            employee.SetComputer(computer);
            
            return;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _config.QuotaConfigs[0].MaxTime)
        {
            Debug.Log($"Time left! You earned {_boss.Money}$/{_config.QuotaConfigs[0].MoneyGoal}$");
        }
    }

    private bool CanStart(int amount) => (amount > 0 && amount > _employees.Count - 1) || (amount == 0);

    private void OnEventStarted(Employee employee, EventConfig config)
    {
        if (!CanStart(config.ColleaguesAmount)) return;

        var colleagues = new List<Employee>();

        foreach (var eventConfig in _eventsDatabase.Events.Where(eventConfig => eventConfig == config))
        {
            foreach (var worker in _employees)
            {
                if (worker == employee)
                {
                    worker.SetMaxProgressScale(eventConfig.ProgressScale);
                    worker.SetEventIcon(eventConfig.Sprite);
                    continue;
                }
                
                colleagues.Add(worker);

                foreach (var reaction in eventConfig.Reactions.Where(reaction => reaction.Trait == worker.RealForm.Trait))
                {
                    worker.ChangeMood(reaction.MoodChange);
                    worker.SetEventIcon(reaction.Sprite);
                    break;
                }
            }

            break;
        }
        
        _audioManager.PlaySound(config.SoundData, employee.transform.position);
        
        if (config.Leave) employee.Fire();

        if (config.Kill) colleagues[Random.Range(0, colleagues.Count)].Kill();
    }

    private void OnEmployeeLeave(Employee employee) => DeleteEmployee(employee);

    private void OnEmployeePaid(Employee employee, int value) => _boss.AddMoney(value);

    private void DeleteEmployee(Employee employee)
    {
        _employees.Remove(employee);
        Unsubscribe(employee);
    }

    private void OnMoneyGiven() => _boss.Pay();
    
    private void Unsubscribe(Employee employee)
    {
        employee.OnLeave -= OnEmployeeLeave;
        employee.OnPaid -= OnEmployeePaid;
        employee.OnEventStarted -= OnEventStarted;
        employee.OnMoneyGiven -= OnMoneyGiven;
    }

    private void OnDestroy()
    {
        foreach (var employee in _employees)
        {
            Unsubscribe(employee);
        }
        _employees.Clear();
        
        foreach (var computer in _computers)
        {
            computer.OnChange -= OnComputerStateChanged;
        }
    }
}
}