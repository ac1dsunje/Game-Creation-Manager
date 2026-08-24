using UnityEngine;

namespace _Game.Scripts
{
public class EmployeeSpawner: MonoBehaviour
{
    [SerializeField] private GameObject _employeePrefab;
    
    [ContextMenu("SpawnForm")]
    public void SpawnEmployee()
    {
        Instantiate(_employeePrefab, transform.position, Quaternion.identity, transform);
    }
}
}