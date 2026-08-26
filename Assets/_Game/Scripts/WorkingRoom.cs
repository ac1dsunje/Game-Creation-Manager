using System.Collections.Generic;
using _Game.Scripts.Boss;
using _Game.Scripts.Interactive.Computers;
using _Game.Scripts.Interactive.Employees;
using UnityEngine;
using VContainer;

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
        employee.OnDeath += OnEmployeeLeave;
        employee.OnPaid += OnEmployeePaid;
        employee.OnFinishedTask += OnEmployeeFinishedTask;

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

    private void OnEmployeeLeave(Employee employee)
    {
        Debug.Log($"{employee.ShownForm.Name} left");
        _employees.Remove(employee);
    }

    private void OnEmployeeFinishedTask(Employee employee)
    {
        Debug.Log($"{employee.ShownForm.Name} finished");
        employee.GiveMoney(_boss.TakeMoney());
    }

    private void OnEmployeePaid(Employee employee, int value)
    {
        Debug.Log($"{employee.ShownForm.Name} Paid {value}$");
        _boss.AddMoney(value);
    }

    private void OnDestroy()
    {
        foreach (var employee in _employees)
        {
            employee.OnDeath -= OnEmployeeLeave;
            employee.OnPaid -= OnEmployeePaid;
            employee.OnFinishedTask -= OnEmployeeFinishedTask;
        }
    }
}
}