using UnityEngine;

namespace _Game.Scripts.Interactive.Computers
{
public class Computer: InteractiveObject
{
    [SerializeField] private GameObject _on;
    [SerializeField] private GameObject _off;

    public bool IsOn { get; private set; }
    public bool IsBusy { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Off();
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