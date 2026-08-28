using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Interactive.Analytics
{
public class AnalyticsTable: InteractiveObject
{
    [SerializeField] private GameObject _employeeSlotUI;
    [SerializeField] private Transform _slotsContainer;
    [Inject] private WorkingRoom _room;

    private readonly List<GameObject> _slots = new();

    private void Update()
    {
        if (!_isActive) return;

        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        
        _slots.Clear();
        foreach (var employee in _room.GetHiredEmployees())
        {
            var slot = Instantiate(_employeeSlotUI, _slotsContainer);
            _slots.Add(slot);
            slot.GetComponent<EmployeeAnalyticSlotUI>().Construct(employee);
        }
    }
}
}