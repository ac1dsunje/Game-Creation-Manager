using UnityEngine;

namespace _Game.Scripts.Interactive
{
public abstract class InteractiveObject: MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    
    protected bool _isActive;

    protected virtual void Awake()
    {
        _visual.SetActive(_isActive);
    }

    public virtual void Interact()
    {
        _isActive = !_isActive;
        _visual.gameObject.SetActive(_isActive);
    }
}
}