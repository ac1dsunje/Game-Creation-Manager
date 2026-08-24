using System;
using UnityEngine;

namespace _Game.Scripts
{
[Serializable]
public class Form
{
    [field: SerializeField] public int Experience { get; private set; }
    
    public Form(int experience)
    {
        Experience = experience;
    }
}
}