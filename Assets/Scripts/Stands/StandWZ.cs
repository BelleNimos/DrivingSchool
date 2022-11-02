using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StandWZ : MonoBehaviour
{
    [SerializeField] private Scale _sliderWZ;
    [SerializeField] private WaitingZone _waitingZone;
    [SerializeField] private Player _player;

    private void Update()
    {
        if (_sliderWZ.IsEmpty == true)
        {
            _sliderWZ.gameObject.SetActive(false);

            if (_player.CurrentCustomersCount > 0 && _waitingZone.CurrentCustomersCount < _waitingZone.MaxCountCustomers)
                _waitingZone.SetTarget(_player.GetCustomer());
        }
    }
}
