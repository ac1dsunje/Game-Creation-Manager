using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Interactive.Computers
{
public class ComputerUI: MonoBehaviour
{
    [SerializeField] private Button _on;
    [SerializeField] private Button _off;
    [SerializeField] private Computer _computer;

    private void Awake()
    {
        _on.onClick.AddListener(On);
        _off.onClick.AddListener(Off);
    }

    private void On()
    {
        _computer.On();
    }

    private void Off()
    {
        _computer.Off();
    }

    private void OnDestroy()
    {
        _on.onClick.RemoveAllListeners();
        _off.onClick.RemoveAllListeners();
    }
}
}