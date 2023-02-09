using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] List<Customer> _customerPrefabs;
    [SerializeField] private Transform _startTransform;

    public Customer InstantiateCustomer()
    {
        int index = Random.Range(0, _customerPrefabs.Count);

        return Instantiate(_customerPrefabs[index], _startTransform.position, Quaternion.identity);
    }
}
