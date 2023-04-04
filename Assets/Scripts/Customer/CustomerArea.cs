using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerArea : MonoBehaviour
{
    [SerializeField] private List<CustomerFirstTarget> _customerFirstTargets;
    [SerializeField] private StackDollars _stackDollarsPrefab;
    [SerializeField] private Scale _sliderCustomer;
    [SerializeField] private Entrance _entrance;
    [SerializeField] private CustomerLastTarget _customerLastTarget;
    [SerializeField] private MoneyTarget _moneyTarget;
    [SerializeField] private CashCounter _cashCounter;
    [SerializeField] private Exit _exit;
    [SerializeField] private Player _player;
    [SerializeField] private int _minDelay;
    [SerializeField] private int _maxDelay;
    [SerializeField] private int _moneyToWithdraw;
    [SerializeField] private float _maxWaitingSeconds;

    private List<Customer> _customers;
    private int _index;

    private const float MinDistance = 2f;

    public int CurrentCustomersCount => _customers.Count;

    private void Awake()
    {
        _customers = new List<Customer>();
    }

    private void Start()
    {
        StartCoroutine(IntantiateCustomers());
    }

    private void Update()
    {
        for (int i = 0; i < _customers.Count; i++)
            if (_customers[i].IsReadyExit == true)
                _customers.RemoveAt(i);

        if (_sliderCustomer.IsEmpty == true)
        {
            _sliderCustomer.gameObject.SetActive(false);

            _index = Random.Range(0, _customers.Count);

            if (_customers[_index].IsReadyExit == false)
            {
                _customers[_index].SetMinDistance(MinDistance);
                _customers[_index].SetTarget(_player.transform);
                _player.AddCustomer(_customers[_index]);
                _customers.RemoveAt(_index);
            }
        }
    }

    private IEnumerator IntantiateCustomers()
    {
        while (true)
        {
            int delay = Random.Range(_minDelay, _maxDelay);
            WaitForSeconds waitForSeconds = new(delay);

            int index = Random.Range(0, _customerFirstTargets.Count);
            _customers.Add(_entrance.InstantiateCustomer());
            _customers[_customers.Count - 1].SetMoneyPoint(_moneyTarget);
            _customers[_customers.Count - 1].SetCashCounter(_cashCounter);
            _customers[_customers.Count - 1].SetStackDollars(_stackDollarsPrefab);
            _customers[_customers.Count - 1].SetFirstTarget(_customerFirstTargets[index]);
            _customers[_customers.Count - 1].SetLastTarget(_customerLastTarget);
            _customers[_customers.Count - 1].SetExitTarget(_exit);
            _customers[_customers.Count - 1].SetMoneyToWithdraw(_moneyToWithdraw);
            _customers[_customers.Count - 1].SetMaxWaitingSeconds(_maxWaitingSeconds);

            yield return waitForSeconds;
        }
    }

    public void EnableSlider()
    {
        _sliderCustomer.gameObject.SetActive(true);
    }
}
