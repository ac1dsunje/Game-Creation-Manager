using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game.Scripts
{
public class FormSlotUI: MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _employeeExperience;

    private Employee _employee;
    
    public event Action<Employee, FormSlotUI> OnEmployeeChosen;
    
    public void SetEmployee(Employee employee)
    {
        _employee = employee;
        _employeeExperience.text = $"{_employee.ShownForm.Experience}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnEmployeeChosen?.Invoke(_employee, this);
    }
}
}