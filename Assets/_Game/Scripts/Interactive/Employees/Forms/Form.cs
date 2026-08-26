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

    private List<string> _names = new() 
    {
        "Alexander", "Anastasia", "Andrew", "Anna", "Anton", "Arina", "Arthur", "Barbara", "Benjamin", "Catherine",
        "Charles", "Christina", "Christopher", "Daniel", "David", "Diana", "Dmitry", "Edward", "Elena", "Elizabeth",
        "Emily", "Emma", "Eric", "Eugene", "Felix", "Francesca", "Frank", "Gabriel", "George", "Grace",
        "Gregory", "Hannah", "Henry", "Igor", "Isabella", "Ivan", "Jack", "Jacob", "James", "Jennifer",
        "Jessica", "John", "Jonathan", "Joseph", "Julia", "Karen", "Katherine", "Kevin", "Kirill", "Laura",
        "Leonard", "Lily", "Lisa", "Lucas", "Lydia", "Maria", "Mark", "Martha", "Martin", "Matthew",
        "Maxim", "Michael", "Michelle", "Mikhail", "Natalie", "Nathan", "Nicholas", "Nicole", "Nikita", "Olga",
        "Oliver", "Patricia", "Patrick", "Paul", "Peter", "Philip", "Rachel", "Richard", "Robert", "Roman",
        "Samantha", "Samuel", "Sarah", "Sergey", "Sophia", "Stephanie", "Steven", "Tatiana", "Thomas", "Timothy",
        "Valentina", "Vasily", "Victoria", "Vladimir", "William", "Yulia", "Zachary"
    };
}
}