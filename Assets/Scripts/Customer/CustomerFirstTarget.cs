using UnityEngine;

public class CustomerFirstTarget : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
            if (customer.CheckPosition(transform))
                customer.StopMove();
    }
}
