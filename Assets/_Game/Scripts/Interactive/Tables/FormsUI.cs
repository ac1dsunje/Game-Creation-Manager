using System.Collections.Generic;
using _Game.Scripts.Interactive.Employees;
using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Interactive.Tables
{
public class FormsUI: MonoBehaviour
{
    [SerializeField] private GameObject _slot;
    [SerializeField] private Transform _container;
    
    private readonly List<FormSlotUI> _slots = new();
    
    private EmployeeSpawner _spawner;
    private WorkingRoom _workingRoom;

    [Inject]
    private void Construct(EmployeeSpawner spawner, WorkingRoom workingRoom)
    {
        _spawner = spawner;
        _spawner.OnEmployeeSpawned += CreateFormUI;
        
        _workingRoom = workingRoom;
    }

    private void CreateFormUI(Employee employee)
    {
        var slot = Instantiate(_slot, _container).GetComponent<FormSlotUI>();
        slot.SetEmployee(employee);
        _slots.Add(slot);
        slot.OnEmployeeAccepted += ApplyForm;
        slot.OnEmployeeDeclined += DeclineForm;
    }

    private void ApplyForm(Employee employee, FormSlotUI slot)
    {
        _workingRoom.AddEmployee(employee);
        Destroy(slot.gameObject);
        _slots.Remove(slot);
    }

    private void DeclineForm(Employee employee, FormSlotUI slot)
    {
        Destroy(slot.gameObject);
        _slots.Remove(slot);
        Destroy(employee.gameObject);
    }

    private void OnDestroy()
    {
        _spawner.OnEmployeeSpawned -= CreateFormUI;
        foreach (var slot in _slots)
        {
            slot.OnEmployeeAccepted -= ApplyForm;
            slot.OnEmployeeDeclined -= DeclineForm;
        }
    }
}
}