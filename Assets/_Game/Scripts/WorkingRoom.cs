using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Boss;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees;
using UnityEngine;
using VContainer;
using EventType = _Game.Scripts.Interactive.Employees.EventType;

namespace _Game.Scripts
{
public class WorkingRoom: MonoBehaviour
{
    [SerializeField] private List<Computer> _computers;
    
    [SerializeField] private List<Employee> _employees = new();

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

    private void OnEventStarted(Employee employee, EventType eventType)
    {
        Debug.Log($"{eventType} started by {employee.ShownForm.Name}");
        switch (eventType)
        {
            case EventType.LowEfficiency:
                employee.SetMaxProgress(60);
                break;
            case EventType.HighEfficiency:
                employee.SetMaxProgress(10);
                break;
            case EventType.Scream:
                foreach (var worker in _employees.Where(worker => worker != employee))
                {
                    switch (employee.Trait)
                    {
                        case TraitType.Psycho:
                            worker.AddMood(1);
                            break;
                        case TraitType.Narciss:
                            worker.AddMood(-1);
                            break;
                        case TraitType.Worker:
                            worker.AddMood(-1);
                            break;
                    }
                }
                break;
            case EventType.Fart:
                break;
            case EventType.Sneeze:
                break;
            case EventType.Cry:
                break;
            case EventType.Music:
                break;
            case EventType.Kill:
                break;
            case EventType.Insult:
                employee.Kill();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
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