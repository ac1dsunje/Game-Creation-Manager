using System;
using UnityEngine;

namespace _Game.Scripts
{
public class EmployeeSpawner: MonoBehaviour
{
    [SerializeField] private Employee _employeePrefab;
    [SerializeField] private int _employeeCount = 15;
    
    public event Action<Employee> OnEmployeeSpawned;
    
    private void Start()
    {
        for (var i = 0; i < _employeeCount; i++)
        {
            SpawnEmployee();
        }
    }
    
    private void SpawnEmployee()
    {
        var newEmployee = Instantiate(_employeePrefab, transform.position, Quaternion.identity, transform);
        OnEmployeeSpawned?.Invoke(newEmployee);
    }
}
}