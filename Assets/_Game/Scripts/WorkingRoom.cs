using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
public class WorkingRoom: MonoBehaviour
{
    [SerializeField] private List<Computer> _computers;
    
    [SerializeField] private List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);

        foreach (var computer in _computers)
        {
            if (computer.IsBusy) continue;
            employee.transform.SetParent(computer.transform);
            
            var position = computer.transform.position;
            
            employee.transform.position = new Vector2(position.x, position.y - 1);
            
            computer.SetBusy(true);
            employee.SetComputer(computer);
            
            return;
        }
    }
}
}