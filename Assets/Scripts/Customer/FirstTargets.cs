using System.Collections.Generic;
using UnityEngine;

public class FirstTargets : MonoBehaviour
{
    [SerializeField] private List<Customer> _customers;
    [SerializeField] private List<CustomerFirstTarget> _customerFirstTargets;

    private void Awake()
    {
        for (int i = 0; i < _customers.Count; i++)
            _customers[i].SetFirstTarget(_customerFirstTargets[i].transform);
    }
}
