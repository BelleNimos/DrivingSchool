using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
            if (customer.IsReadyExit == true)
                Destroy(customer.gameObject);
    }
}
