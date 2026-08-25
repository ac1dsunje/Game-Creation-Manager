using UnityEngine;

namespace _Game.Scripts
{
public class Computer: MonoBehaviour
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;

    private bool _isActive;
    public bool IsBusy { get; private set; }
    private Employee _employee;
    
    private void Awake()
    {
        Off();
    }

    public void Toggle()
    {
        if (_isActive)
            Off();
        else
            On();
    }

    public void SetEmployee(Employee employee)
    {
        _employee = employee;
        IsBusy = true;
    }

    private void Off()
    {
        _off.SetActive(true);
        _on.SetActive(false);
        _isActive = false;
    }

    private void On()
    {
        _off.SetActive(false);
        _on.SetActive(true);
        _isActive = true;
    }
}
}