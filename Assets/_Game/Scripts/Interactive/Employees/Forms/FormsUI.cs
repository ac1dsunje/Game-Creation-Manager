using System.Collections.Generic;
using _Game.Scripts.Boss;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.Interactive.Employees.Forms
{
public class FormsUI: MonoBehaviour
{
    [SerializeField] private GameObject _slot;
    [SerializeField] private Transform _container;
    
    private readonly List<FormSlotUI> _slots = new();
    
    private EmployeeSpawner _spawner;
    private WorkingRoom _workingRoom;
    private BossController _bossController;

    [Inject]
    private void Construct(EmployeeSpawner spawner, WorkingRoom workingRoom, BossController boss)
    {
        _spawner = spawner;
        _spawner.OnEmployeeSpawned += CreateFormUI;
        
        _workingRoom = workingRoom;
        _bossController = boss;
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