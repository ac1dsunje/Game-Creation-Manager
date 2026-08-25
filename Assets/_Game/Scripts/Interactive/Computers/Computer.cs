using UnityEngine;

namespace _Game.Scripts.Interactive.Computers
{
public class Computer: MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;
    [SerializeField] private ComputerUI _ui;

    private bool _isActive;
    public bool IsBusy { get; private set; }
    
    private void Awake()
    {
        Off();
        _ui.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (_isActive)
        {
            _ui.gameObject.SetActive(false);
            _isActive = false;
        }
            
        else
        {
            _ui.gameObject.SetActive(true);
            _isActive = true;
        }
    }

    public void SetBusy(bool state) => IsBusy = state;

    public void Off()
    {
        _off.SetActive(true);
        _on.SetActive(false);
        _isActive = false;
    }

    public void On()
    {
        _off.SetActive(false);
        _on.SetActive(true);
        _isActive = true;
    }
}
}