using UnityEngine;

namespace _Game.Scripts.Interactive.Computers
{
public class Computer: MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;
    [SerializeField] private ComputerUI _ui;

    private bool _isActive;
    public bool IsOn { get; private set; }
    public bool IsBusy { get; private set; }
    
    private void Awake()
    {
        Off();
        _ui.gameObject.SetActive(false);
    }

    public void Interact()
    {
        _isActive = !_isActive;
        _ui.gameObject.SetActive(_isActive);
    }

    public void SetBusy(bool state) => IsBusy = state;

    public void Off()
    {
        _off.SetActive(true);
        _on.SetActive(false);
        IsOn = false;
    }

    public void On()
    {
        _off.SetActive(false);
        _on.SetActive(true);
        IsOn = true;
    }
}
}