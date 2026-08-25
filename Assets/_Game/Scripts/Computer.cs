using UnityEngine;

namespace _Game.Scripts
{
public class Computer: MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;

    private bool _isActive;
    public bool IsBusy { get; private set; }
    
    private void Awake()
    {
        Off();
    }

    private void Toggle()
    {
        if (_isActive)
            Off();
        else
            On();
    }

    public void Interact()
    {
        Toggle();
    }

    public void SetBusy(bool state) => IsBusy = state;

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