using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bag))]
public class Player : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private Bag _bag;

    private void Start()
    {
        _bag = GetComponent<Bag>();
    }

    private IEnumerator AddCones()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        for (int i = 0; i < _bag.MaxConesCount; i++)
        {
            if (_spawner.CurrentConesCount > 0)
                if (_bag.CurrentConesCount < _bag.MaxConesCount)
                    _bag.AddCone(_spawner.GetCone());
            
            yield return waitForSeconds;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Stand>(out Stand stand))
        {
            StartCoroutine(AddCones());
        }

        if (collision.TryGetComponent<ConePoint>(out ConePoint coneLiesPoint))
        {
            if (_bag.CurrentConesCount > 0 && coneLiesPoint.CurrentConesCount <= 0)
                coneLiesPoint.AddCone(_bag.GetCone());
        }
    }
}
