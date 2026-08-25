using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Interactive.Employees.Forms
{
[Serializable]
public class Form
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Age { get; private set; }
    [field: SerializeField] public float Efficiency { get; private set; }
    
    public Form(float efficiency, int age)
    {
        Name = _names[Random.Range(0, _names.Count)];
        Efficiency = efficiency;
        Age = age;
    }

    private List<string> _names = new() {"John Doe", "Nigga", "1e", "2", "3", "4",
        "5e", "John D6oe", "Joh7n Doe", "Jo8hn Doe"};
}
}