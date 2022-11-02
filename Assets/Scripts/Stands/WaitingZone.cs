using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingZone : MonoBehaviour
{
    [SerializeField] private List<TargetWZ> _targets;
    [SerializeField] private TMP_Text _countCustomersText;
    [SerializeField] private TMP_Text _maxCountCustomersText;
    [SerializeField] private Car _car;

    private int _counter;
    private int _index;
    private int _currentCustomersCount;

    private const float MinDistance = 0.1f;
    private const int MaxCustomersCount = 4;

    public int MaxCountCustomers => MaxCustomersCount;
    public int CurrentCustomersCount => _currentCustomersCount;

    private void Update()
    {
        _counter = 0;
        _maxCountCustomersText.text = MaxCustomersCount.ToString();
        _countCustomersText.text = _currentCustomersCount.ToString();

        if (_currentCustomersCount > 0 && _car.IsFree == true)
        {
            _index = Random.Range(0, _targets.Count);

            _car.AddCustomer(_targets[_index].GetCustomer());
            _targets[_index].RemoveCustomer();
        }

        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].IsFree == false)
                _counter++;
        }

        _currentCustomersCount = _counter;
    }

    public void SetTarget(Customer customer)
    {
        customer.SetMinDistance(MinDistance);

        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i].IsFree == true)
            {
                customer.SetTarget(_targets[i].transform);
                break;
            }
        }
    }
}
