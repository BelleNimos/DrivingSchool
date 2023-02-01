using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private TargetsCar _targetsCar;

    public void TransferTargetCar(ref TargetsCar targetsCar)
    {
        targetsCar = _targetsCar;
    }
}
