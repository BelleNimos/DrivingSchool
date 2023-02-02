using UnityEngine;

public class CustomerLastTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
        {
            if (customer.CheckPosition(transform))
            {
                if (customer.IsFinish == true)
                {
                    customer.InstantiateStackDollars();
                    customer.IsPaidTrue();
                    customer.IsFinishFalse();
                }
            }
        }
    }
}
