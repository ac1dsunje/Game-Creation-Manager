using UnityEngine;
using VContainer;

namespace _Game.Scripts
{
public class FormsUI: MonoBehaviour
{
    [SerializeField] private GameObject _slot;
    [SerializeField] private Transform _container;
    
    private EmployeeSpawner _spawner;

    [Inject]
    private void Construct(EmployeeSpawner spawner)
    {
        _spawner = spawner;
        _spawner.OnEmployeeSpawned += CreateFormUI;
    }

    private void CreateFormUI(Employee employee)
    {
        var slot = Instantiate(_slot, _container).GetComponent<FormSlotUI>();
        slot.SetEmployee(employee);
    }

    private void OnDestroy()
    {
        _spawner.OnEmployeeSpawned -= CreateFormUI;
    }
}
}