using TMPro;
using UnityEngine;

namespace _Game.Scripts
{
public class FormSlotUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _employeeExperience;

    private Employee _employee;
    
    public void SetEmployee(Employee employee)
    {
        _employee = employee;
        _employeeExperience.text = $"{_employee.ShownForm.Experience}";
    }
}
}