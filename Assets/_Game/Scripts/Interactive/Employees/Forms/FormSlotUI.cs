using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Interactive.Employees.Forms
{
public class FormSlotUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _age;
    [SerializeField] private TextMeshProUGUI _trait;
    [SerializeField] private TextMeshProUGUI _disadvantage;
    [SerializeField] private TextMeshProUGUI _advantage;
    [SerializeField] private Button _accept;
    [SerializeField] private Button _cancel;

    private Employee _employee;
    public event Action<Employee, FormSlotUI> OnEmployeeAccepted;
    public event Action<Employee, FormSlotUI> OnEmployeeDeclined;

    private void Awake()
    {
        _accept.onClick.AddListener(Accept);
        _cancel.onClick.AddListener(Decline);
    }
    
    public void SetEmployee(Employee employee)
    {
        _employee = employee;
        _name.text = _employee.ShownForm.Name;
        _age.text = $"{_employee.ShownForm.Age}";
        _trait.text =$"{_employee.ShownForm.Trait.name}";
        _disadvantage.text =$"{_employee.ShownForm.Disadvantage.name}";
        _advantage.text =$"{_employee.ShownForm.Advantage.name}";
    }

    private void Accept()
    {
        OnEmployeeAccepted?.Invoke(_employee, this);
    }

    private void Decline()
    {
        OnEmployeeDeclined?.Invoke(_employee, this);
    }

    private void OnDestroy()
    {
        _accept.onClick.RemoveAllListeners();
        _cancel.onClick.RemoveAllListeners();
    }
}
}