using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Bag _bag;
    [SerializeField] private TMP_Text _maxConesText;

    private List<Customer> _customers;
    private MoneyPoint _moneyPoint;

    public int CurrentCustomersCount => _customers.Count;

    private void Start()
    {
        _customers = new List<Customer>();
        _moneyPoint = FindObjectOfType<MoneyPoint>();
    }

    private void Update()
    {
        for (int i = 0; i < _customers.Count; i++)
            if (_customers[i].IsReadyExit == true)
                _customers.RemoveAt(i);

        if (_bag.CurrentConesCount == _bag.MaxConesCount)
            _maxConesText.gameObject.SetActive(true);
        else
            _maxConesText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<ConePoint>(out ConePoint conePoint))
            if (_bag.CurrentConesCount > 0 && conePoint.IsFree == true)
                _bag.GiveAwayCone(conePoint);

        if (collision.TryGetComponent<Cone>(out Cone cone))
            if (_bag.CurrentConesCount < _bag.MaxConesCount)
                if (cone.IsCollision == true)
                    _bag.AddCone(cone);

        if (collision.TryGetComponent<ConeUpgrades>(out ConeUpgrades conesUpgrade))
            if (_moneyPoint.CurrentDollarsCount >= conesUpgrade.Price)
                conesUpgrade.Unlock();

        if (collision.TryGetComponent<TransitionLevel>(out TransitionLevel transitionLevel))
            transitionLevel.OpenPanel();

        if (collision.TryGetComponent<UpgradesStand>(out UpgradesStand upgradesStand))
            upgradesStand.EnableUpgradesPanel();

        if (collision.TryGetComponent<StackDollars>(out StackDollars stackDollars))
            stackDollars.StartMove();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<UpgradesStand>(out UpgradesStand upgradesStand))
            upgradesStand.DisableUpgradesPanel();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<Spawner>(out Spawner spawner))
        {
            if (spawner.CurrentConesCount == 0)
                spawner.EnableSilder();

            if (spawner.IsReady && _bag.CurrentConesCount < _bag.MaxConesCount)
                spawner.GiveAwayCone(_bag);
        }

        if (collision.TryGetComponent<Utilizer>(out Utilizer utilizer))
            if (_bag.CurrentConesCount > 0)
                _bag.GiveAwayCone(utilizer);

        if (collision.TryGetComponent<CustomerArea>(out CustomerArea customerArea))
            if (customerArea.CurrentCustomersCount > 0)
                customerArea.EnableSlider();

        if (collision.TryGetComponent<StandWZ>(out StandWZ standWZ))
            if (CurrentCustomersCount > 0 && standWZ.CustomersCount < standWZ.MaxCustomersCount)
                standWZ.EnableSlider();
    }

    public void AddCustomer(Customer customer)
    {
        _customers.Add(customer);
    }

    public void AssignTargetCustomer(WaitingZone waitingZone)
    {
        int index = Random.Range(0, _customers.Count);
        waitingZone.SetTarget(_customers[index]);
        _customers[index].StopTimer();
        _customers.RemoveAt(index);
    }
}
