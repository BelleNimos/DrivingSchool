using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ConePoint : MonoBehaviour
{
    [SerializeField] private Material _eldenMaterial;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Transform _positionSpawnDollar;

    private Renderer _renderer;
    private Cone _cone;

    private const float PowerJumpCone = 1f;
    private const float DurationJumpCone = 0.2f;
    private const int NumJumpsCone = 1;

    public bool IsFree { get; private set; }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_cone == null)
            IsFree = true;
        else
            IsFree = false;

        if (_cone != null)
            if (_cone.IsCollision == true)
                _cone = null;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<Cone>(out Cone cone))
        {
            if (_cone != null && _cone == cone)
            {
                _renderer.material = _newMaterial;
                cone.UnlockPhysics();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Cone>(out Cone cone))
            _renderer.material = _eldenMaterial;
    }

    public void AddCone(Cone cone)
    {
        _cone = cone;
        Vector3 nextRotation = new Vector3(0, -90, 0);

        cone.transform.DOJump(transform.position, PowerJumpCone, NumJumpsCone, DurationJumpCone)
            .SetLink(cone.gameObject)
            .OnKill(() =>
            {
                cone.transform.SetParent(transform, true);
                cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
                cone.StartFallAnimation();
                cone.CreateDollar(_positionSpawnDollar);
                IsFree = false;
            }
            );
    }
}
