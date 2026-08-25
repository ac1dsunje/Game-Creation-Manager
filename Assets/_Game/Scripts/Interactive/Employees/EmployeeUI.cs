using _Game.Scripts.Interactive.Employees.Forms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Interactive.Employees
{
public class EmployeeUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _experience;
    [SerializeField] private Button _hire;
    
    [SerializeField] private Employee _employee;

    private void Awake()
    {
        _hire.onClick.AddListener(Fire);
    }

    private void Fire()
    {
        _employee.Fire();
    }

    public void SetInfo(Form form)
    {
        _name.text = form.Name;
        _experience.text = form.Experience.ToString();
    }

    private void OnDestroy()
    {
        _hire.onClick.RemoveAllListeners();
    }
}
}