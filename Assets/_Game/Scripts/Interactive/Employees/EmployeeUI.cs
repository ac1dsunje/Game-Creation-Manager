using _Game.Scripts.Interactive.Employees.Forms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Interactive.Employees
{
public class EmployeeUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Button _hire;
    [SerializeField] private Button _giveMoney;
    [SerializeField] private Button _cheer;
    
    [SerializeField] private Employee _employee;

    private void Awake()
    {
        _hire.onClick.AddListener(Fire);
        _giveMoney.onClick.AddListener(GiveMoney);
        _cheer.onClick.AddListener(Cheer);
    }

    private void Update()
    {
        SetInfo(_employee.ShownForm);
    }

    private void Fire() => _employee.Fire();

    private void GiveMoney() => _employee.GiveMoney();
    private void Cheer() => _employee.Cheer();

    private void SetInfo(Form form)
    {
        _name.text = form.Name;
    }

    private void OnDestroy()
    {
        _hire.onClick.RemoveAllListeners();
        _giveMoney.onClick.RemoveAllListeners();
        _cheer.onClick.RemoveAllListeners();
    }
}
}