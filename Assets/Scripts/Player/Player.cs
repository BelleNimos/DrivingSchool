using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bag _bag;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Cone>(out Cone cone) && _bag.CurrentCones < _bag.MaxCones)
            _bag.AddCone(cone);
    }
}
