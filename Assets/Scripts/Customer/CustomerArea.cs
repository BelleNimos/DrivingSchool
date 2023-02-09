using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArea : MonoBehaviour
{
    [SerializeField] private List<CustomerFirstTarget> _customerFirstTargets;
    [SerializeField] private Entrance _entrance;
    [SerializeField] private Scale _sliderCustomer;
    [SerializeField] private Player _player;
    [SerializeField] private int _minDelay;
    [SerializeField] private int _maxDelay;

    private List<Customer> _customers;
    private WaitForSeconds _waitForSeconds;
    private int _index;

    private const float MinDistance = 2f;

    public int CurrentCustomersCount => _customers.Count;

    private void Awake()
    {
        _customers = new List<Customer>();
        StartCoroutine(IntantiateCustomers());
    }

    private void Update()
    {
        if (_sliderCustomer.IsEmpty == true)
        {
            _sliderCustomer.gameObject.SetActive(false);

            _index = Random.Range(0, _customers.Count);

            _customers[_index].SetMinDistance(MinDistance);
            _customers[_index].SetTarget(_player.transform);

            _customers[_index].StopTimer();
            _player.AddCustomer(_customers[_index]);
            _customers.Remove(_customers[_index]);
        }
    }

    private IEnumerator IntantiateCustomers()
    {
        while (true)
        {
            int delay = Random.Range(_minDelay, _maxDelay);
            _waitForSeconds = new WaitForSeconds(delay);

            int index = Random.Range(0, _customerFirstTargets.Count);
            _customers.Add(_entrance.InstantiateCustomer());
            _customers[_customers.Count - 1].SetFirstTarget(_customerFirstTargets[index].transform);

            yield return _waitForSeconds;
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }

    public void EnableSlider()
    {
        _sliderCustomer.gameObject.SetActive(true);
    }
}
