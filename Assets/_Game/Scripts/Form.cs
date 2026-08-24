using System;
using UnityEngine;

namespace _Game.Scripts
{
[Serializable]
public class Form
{
    [field: SerializeField] public string Name { get; private set; } = "John Doe";
    [field: SerializeField] public int Age { get; private set; }
    [field: SerializeField] public int Experience { get; private set; }
    
    public Form(int experience, int age)
    {
        Experience = experience;
        Age = age;
    }
}
}