using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ConePoint : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;
    [SerializeField] private MoneyPoint _moneyPoint;

    private Renderer _renderer;
    private Cone _cone;

    private const float PowerJumpCone = 1f;
    private const float DurationJumpCone = 0.2f;
    private const int NumJumpsCone = 1;

    public bool IsFree { get; private set; }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        IsFree = true;
    }

    public void AddCone(Cone cone)
    {
        _cone = cone;
        Vector3 nextRotation = new Vector3(0, -90, 0);

        _cone.transform.DOJump(transform.position, PowerJumpCone, NumJumpsCone, DurationJumpCone)
            .OnComplete(() =>
            {
                _cone.transform.SetParent(transform, true);
                _cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
                _cone.StartFallAnimation();
            }
            );

        _renderer.material = _newMaterial;
        IsFree = false;
        _cone.CreateDollar(_moneyPoint, transform);
    }

    public void UnlockPhysics()
    {
        _cone.OffKinematic();
        _cone.OffTrigger();
    }

    public void ResetConePosition()
    {
        Vector3 nextRotation = new Vector3(0, -90, 0);
        _cone.transform.position = transform.position;
        _cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
    }
}