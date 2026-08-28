using _Game.Scripts.Interactive.Employees.Forms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Game.Scripts.Interactive.Employees
{
public class EmployeeUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _trait;
    [SerializeField] private TextMeshProUGUI _disadvantage;
    [SerializeField] private TextMeshProUGUI _advantage;
    
    
    [SerializeField] private Button _hire;
    [SerializeField] private Button _giveMoney;
    [SerializeField] private Button _cheer;
    
    [SerializeField] private Image _progressImage;
    [SerializeField] private Image _moodImage;
    
    [Inject] private Employee _employee;
    [Inject] private WorkingConfig _config;

    private void Awake()
    {
        _hire.onClick.AddListener(Fire);
        _giveMoney.onClick.AddListener(GiveMoney);
        _cheer.onClick.AddListener(Cheer);
    }

    private void Update()
    {
        SetInfo(_employee.ShownForm);
        _progressImage.fillAmount = _employee.CurrentProgress / _employee.MaxProgress;
        _moodImage.fillAmount = (float)_employee.MoodCoefficient / _config.MaxMood;
    }

    private void Fire() => _employee.Fire();
    private void GiveMoney() => _employee.GiveMoney();
    private void Cheer() => _employee.Cheer();

    private void SetInfo(Form form)
    {
        _name.text = form.Name;
        _trait.text =$"{form.Trait.name}";
        _disadvantage.text =$"{form.Disadvantage.name}";
        _advantage.text =$"{form.Advantage.name}";
    }

    private void OnDestroy()
    {
        _hire.onClick.RemoveAllListeners();
        _giveMoney.onClick.RemoveAllListeners();
        _cheer.onClick.RemoveAllListeners();
    }
}
}