using _Game.Scripts.Interactive.Employees;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Interactive.Analytics
{
public class EmployeeAnalyticSlotUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _tasksDone;
    [SerializeField] private TextMeshProUGUI _moneyEarned;
    
    private Employee _employee;
    
    public void Construct(Employee employee)
    {
        _employee = employee;
        _name.text = _employee.ShownForm.Name;
        _tasksDone.text = $"Tasks: {employee.TaskDone}";
        _moneyEarned.text = $"{employee.MoneyEarned}$";
    }
}
}