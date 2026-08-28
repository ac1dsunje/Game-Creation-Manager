using _Game.Scripts.Interactive.Employees;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Interactive.Analytics
{
public class EmployeeAnalyticSlotUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    
    private Employee _employee;
    
    public void Construct(Employee employee)
    {
        _employee = employee;
        _name.text = _employee.ShownForm.Name;
    }
    
    
}
}