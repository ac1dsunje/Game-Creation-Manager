using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace _Game.Scripts
{
public class FormsUI: MonoBehaviour
{
    [SerializeField] private FormSlotUI _slot;
    [SerializeField] private Transform _container;
    
    private List<FormSlotUI> _slots = new();
    
    private EmployeeSpawner _spawner;
    
    public event Action<Employee> OnEmployeeChosen;

    [Inject]
    private void Construct(EmployeeSpawner spawner)
    {
        _spawner = spawner;
        _spawner.OnEmployeeSpawned += CreateFormUI;
    }

    private void CreateFormUI(Employee employee)
    {
        var slot = Instantiate(_slot, _container);
        slot.SetEmployee(employee);
        _slots.Add(slot);
        slot.OnEmployeeChosen += OnEmployeeChosen;
    }

    private void OnDestroy()
    {
        _spawner.OnEmployeeSpawned -= CreateFormUI;
    }
}
}