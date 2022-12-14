using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArea : MonoBehaviour
{
    [SerializeField] private List<Customer> _customers;
    [SerializeField] private Scale _sliderCustomer;
    [SerializeField] private Player _player;

    private int _index;

    private const float MinDistance = 2f; 

    public int CurrentCustomersCount => _customers.Count;

    private void Update()
    {
        if (_sliderCustomer.IsEmpty == true)
        {
            _sliderCustomer.gameObject.SetActive(false);

            _index = Random.Range(0, _customers.Count);

            _customers[_index].SetMinDistance(MinDistance);
            _customers[_index].SetTarget(_player.transform);

            _player.AddCustomer(_customers[_index]);
            _customers.Remove(_customers[_index]);
        }
    }
}
