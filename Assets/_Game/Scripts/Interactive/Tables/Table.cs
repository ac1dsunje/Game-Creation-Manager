using _Game.Scripts.Interactive.Employees.Forms;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Interactive.Tables
{
public class Table: MonoBehaviour, IInteractable
{
    [Inject] private FormsUI _formsUI;
    private bool _isActive;
    
    public void Interact()
    {
        Toggle();
    }

    private void Awake()
    {
        Hide();
    }

    private void Toggle()
    {
        if (_isActive) 
            Hide();
        else
            Show();
    }

    private void Show()
    {
        _formsUI.gameObject.SetActive(true);
        _isActive = true;
    }

    private void Hide()
    {
        _formsUI.gameObject.SetActive(false);
        _isActive = false;
    }
}
}