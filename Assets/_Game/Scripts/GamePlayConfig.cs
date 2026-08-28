using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
[Serializable]
public class GamePlayConfig
{
    [field: SerializeField] public int StartMoney { get; private set; }
    [field: SerializeField] public List<QuotaConfig> QuotaConfigs { get; private set; }
}

[Serializable]
public class QuotaConfig
{
    [field: SerializeField] public float MaxTime { get; private set; }
    [field: SerializeField] public float MoneyGoal { get; private set; }
}
}