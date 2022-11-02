using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Scale _sliderCone;
    [SerializeField] private Scale _sliderCustomer;
    [SerializeField] private Scale _sliderWZN;
    [SerializeField] private Scale _sliderWZS;
    [SerializeField] private TMP_Text _maxConesText;
    [SerializeField] private Bag _bag;

    private Stack<Customer> _customers;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.05f);

    public int CurrentCustomersCount => _customers.Count;

    private void Start()
    {
        _customers = new Stack<Customer>();
    }

    private void Update()
    {
        if (_bag.CurrentConesCount == _bag.MaxConesCount)
            _maxConesText.gameObject.SetActive(true);
        else
            _maxConesText.gameObject.SetActive(false);
    }

    private IEnumerator AddCones(Spawner spawner)
    {

        for (int i = 0; i < _bag.MaxConesCount; i++)
        {
            if (spawner.CurrentConesCount > 0)
                if (_bag.CurrentConesCount < _bag.MaxConesCount)
                    _bag.AddCone(spawner.GetCone());
            
            yield return _waitForSeconds;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Spawner>(out Spawner spawner))
        {
            StartCoroutine(AddCones(spawner));

            if (spawner.CurrentConesCount == 0)
                _sliderCone.gameObject.SetActive(true);
        }

        if (collision.TryGetComponent<CustomerArea>(out CustomerArea customerArea))
        {
            if (customerArea.CurrentCustomersCount > 0)
                _sliderCustomer.gameObject.SetActive(true);
        }

        if (collision.TryGetComponent<ConePoint>(out ConePoint conePoint))
        {
            if (_bag.CurrentConesCount > 0 && conePoint.IsFree == true)
                conePoint.AddCone(_bag.GetCone());
        }

        if (collision.TryGetComponent<StandWZN>(out StandWZN standWZN))
        {
            _sliderWZN.gameObject.SetActive(true);
        }

        if (collision.TryGetComponent<StandWZS>(out StandWZS standWZS))
        {
            _sliderWZS.gameObject.SetActive(true);
        }

        if (collision.TryGetComponent<StackDollars>(out StackDollars stackDollars))
        {
            if (stackDollars.IsReady == true)
            {
                stackDollars.StartMove();
                stackDollars.ChangeValue();
            }
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Push(customer);
    }

    public Customer GetCustomer()
    {
        return _customers.Pop();
    }
}
