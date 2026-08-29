using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Interactive.Computers
{
public class Computer: InteractiveObject
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _transparent;

    public bool IsOn { get; private set; }
    public bool IsBusy { get; private set; }
    
    public event Action<Computer> OnChange;

    protected override void Awake()
    {
        base.Awake();
        Off();
    }

    public void SetIcon(Sprite sprite)
    {
        if (sprite == null)
        {
            sprite = _transparent;
        }
        _icon.sprite = sprite;
    }

    public void SetBusy(bool state) => IsBusy = state;

    public void Off()
    {
        _off.SetActive(true);
        _on.SetActive(false);
        IsOn = false;
        OnChange(this);
    }

    public void On()
    {
        _off.SetActive(false);
        _on.SetActive(true);
        IsOn = true;
        OnChange(this);
    }
}
}