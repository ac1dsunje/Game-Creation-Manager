using System;
using UnityEngine;

namespace _Game.Scripts
{
[Serializable]
public class GamePlayConfig
{
    [field: SerializeField] public int StartMoney { get; private set; }
    [field: SerializeField] public float MaxTime { get; private set; }
}
}