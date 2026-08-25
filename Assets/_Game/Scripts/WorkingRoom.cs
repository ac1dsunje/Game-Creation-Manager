using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
public class WorkingRoom: MonoBehaviour
{
    [SerializeField] private List<Computer> _computers;
    
    [SerializeField] private readonly List<Employee> _employees = new();

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
        employee.transform.SetParent(transform);
    }
}
}