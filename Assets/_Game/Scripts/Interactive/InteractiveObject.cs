using UnityEngine;

namespace _Game.Scripts.Interactive
{
public abstract class InteractiveObject: MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    
    protected bool IsActive;

    protected virtual void Awake()
    {
        _visual.SetActive(IsActive);
    }

    public virtual void Interact()
    {
        IsActive = !IsActive;
        _visual.gameObject.SetActive(IsActive);
    }
}
}