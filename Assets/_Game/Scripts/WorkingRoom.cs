using System.Collections.Generic;
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
    [SerializeField] private EventsDatabase _eventsDatabase;
    private readonly List<Employee> _employees = new();

    [Inject] private BossController _boss;

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
        employee.OnLeave += OnEmployeeLeave;
        employee.OnPaid += OnEmployeePaid;
        employee.OnFinishedTask += OnEmployeeFinishedTask;
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

    private void OnEventStarted(Employee employee, EventConfig config)
    {
        Debug.Log($"{config} started by {employee.ShownForm.Name}");

        foreach (var eventConfig in _eventsDatabase.Events)
        {
            if (eventConfig != config)
                continue;

            foreach (var worker in _employees)
            {
                if (worker == employee)
                {
                    worker.SetMaxProgress(eventConfig.ChangeProgress);
                    worker.SetEventIcon(eventConfig.Sprite);
                    continue;
                }

                foreach (var reaction in eventConfig.Reactions)
                {
                    if (reaction.Trait != worker.Trait)
                        continue;

                    worker.AddMood(reaction.MoodChange);
                    worker.SetEventIcon(reaction.Sprite);
                    break;
                }
            }

            break;
        }
    }

    private void OnEmployeeLeave(Employee employee)
    {
        DeleteEmployee(employee);
    }

    private void OnEmployeeFinishedTask(Employee employee)
    {
        employee.GiveSalary(_boss.TakeMoney());
    }

    private void OnEmployeePaid(Employee employee, int value)
    {
        _boss.AddMoney(value);
    }

    private void DeleteEmployee(Employee employee)
    {
        _employees.Remove(employee);
        Unsubscribe(employee);
    }

    private void OnMoneyGiven()
    {
        _boss.Pay();
    }

    private void Unsubscribe(Employee employee)
    {
        employee.OnLeave -= OnEmployeeLeave;
        employee.OnPaid -= OnEmployeePaid;
        employee.OnFinishedTask -= OnEmployeeFinishedTask;
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
    }
}
}