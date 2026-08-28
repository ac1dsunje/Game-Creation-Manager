using System.Collections.Generic;
using _Game.Scripts.Boss;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Interactive.Analytics
{
public class AnalyticsTable: InteractiveObject
{
    [SerializeField] private GameObject _employeeSlotUI;
    [SerializeField] private Transform _slotsContainer;
    [SerializeField] private TextMeshProUGUI _balance;
    [Inject] private WorkingRoom _room;
    [Inject] private BossController _boss;

    private readonly List<GameObject> _slots = new();

    private void Update()
    {
        if (!IsActive) return;
        _balance.text = $"Balance: {_boss.Money}";

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