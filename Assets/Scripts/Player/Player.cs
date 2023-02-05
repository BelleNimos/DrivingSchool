using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Bag _bag;
    [SerializeField] private TMP_Text _maxConesText;

    private Stack<Customer> _customers;
    private WaitForSeconds _waitForSeconds;

    public int CurrentCustomersCount => _customers.Count;

    private void Start()
    {
        _customers = new Stack<Customer>();
        _waitForSeconds = new WaitForSeconds(0.05f);
    }

    private void Update()
    {
        if (_bag.CurrentConesCount == _bag.MaxConesCount)
            _maxConesText.gameObject.SetActive(true);
        else
            _maxConesText.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<Utilizer>(out Utilizer utilizer))
            if (_bag.CurrentConesCount > 0)
                _bag.GiveAwayCone(utilizer);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<UpgradesStand>(out UpgradesStand upgradesStand))
            upgradesStand.DisableUpgrades();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<UpgradesStand>(out UpgradesStand upgradesStand))
            upgradesStand.EnableUpgrades();

        if (collision.TryGetComponent<ConeUpgrades>(out ConeUpgrades conesUpgrade))
            conesUpgrade.Unlock();

        if (collision.TryGetComponent<TransitionLevel>(out TransitionLevel transitionLevel))
            transitionLevel.GoToNextLevel();

        if (collision.TryGetComponent<CustomerArea>(out CustomerArea customerArea))
            if (customerArea.CurrentCustomersCount > 0)
                customerArea.EnableSlider();

        if (collision.TryGetComponent<StandWZ>(out StandWZ standWZ))
            if (CurrentCustomersCount > 0 && standWZ.CustomersCount < standWZ.MaxCustomersCount)
                standWZ.EnableSlider();

        if (collision.TryGetComponent<Spawner>(out Spawner spawner))
        {
            if (spawner.CurrentConesCount == 0)
                spawner.EnableSilder();

            if (spawner.IsReady)
                StartCoroutine(AddCones(spawner));
        }

        if (collision.TryGetComponent<StackDollars>(out StackDollars stackDollars))
        {
            if (stackDollars.IsReady == true)
            {
                stackDollars.StartMove(_moneyPoint);
                stackDollars.ChangeReadyValue();
            }
        }

        if (collision.TryGetComponent<ConePoint>(out ConePoint conePoint))
        {
            if (_bag.CurrentConesCount > 0 && conePoint.IsFree == true)
            {
                _bag.GiveAwayCone(conePoint);
            }
            else if (_bag.CurrentConesCount < _bag.MaxConesCount && conePoint.IsFree == false)
            {
                if (conePoint.CheckForConeCollision() == true)
                {
                    conePoint.GiveAwayCone(_bag);
                    conePoint.RemoveCone();
                }
            }
        }
    }

    private IEnumerator AddCones(Spawner spawner)
    {
        for (int i = 0; i < _bag.MaxConesCount; i++)
        {
            if (spawner.CurrentConesCount > 0)
                if (_bag.CurrentConesCount < _bag.MaxConesCount)
                    spawner.GiveAwayCone(_bag);
            
            yield return _waitForSeconds;
        }
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Push(customer);
    }

    public void AssignTargetCustomer(WaitingZone waitingZone)
    {
        waitingZone.SetTarget(_customers.Pop());
    }
}
