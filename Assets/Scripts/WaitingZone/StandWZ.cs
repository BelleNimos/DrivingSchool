using UnityEngine;

public class StandWZ : MonoBehaviour
{
    [SerializeField] private Scale _sliderWZ;
    [SerializeField] private WaitingZone _waitingZone;
    [SerializeField] private Player _player;

    private void Update()
    {
        if (_sliderWZ.IsEmpty == true)
        {
            _sliderWZ.gameObject.SetActive(false);

            if (_waitingZone.CurrentCustomersCount < _waitingZone.MaxCountCustomers)
                _player.AssignTargetCustomer(_waitingZone);
        }
    }

    public void EnableSlider()
    {
        _sliderWZ.gameObject.SetActive(true);
    }
}