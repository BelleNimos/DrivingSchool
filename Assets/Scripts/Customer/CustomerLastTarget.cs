using UnityEngine;

public class CustomerLastTarget : MonoBehaviour
{
    [SerializeField] private Exit _exit;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Customer>(out Customer customer))
        {
            if (customer.CheckPosition(transform))
            {
                if (customer.IsFinish == true)
                {
                    customer.InstantiateStackDollars();
                    customer.GoToExit();
                }
            }
        }
    }
}
