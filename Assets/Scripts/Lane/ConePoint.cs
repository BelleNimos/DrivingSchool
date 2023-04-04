using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ConePoint : MonoBehaviour
{
    [SerializeField] private Material _eldenMaterial;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Transform _positionSpawnDollar;
    [SerializeField] private Dollar _dollarPrefab;
    [SerializeField] private MoneyTarget _moneyPoint;
    [SerializeField] private CashCounter _cashCounter;

    private WaitForSeconds _waitForSeconds;
    private Renderer _renderer;
    private Cone _cone;

    private const float PowerJumpCone = 1f;
    private const float DurationJumpCone = 0.2f;
    private const int NumJumpsCone = 1;

    public bool IsFree { get; private set; }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _waitForSeconds = new WaitForSeconds(0.2f);
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

    private void CreateDollars(Transform transform, int count)
    {
        StartCoroutine(InstantiateDollars(transform, count));
    }

    private void InstantiateDollar(Transform transform)
    {
        Dollar dollar = Instantiate(_dollarPrefab, transform.position, Quaternion.identity);
        dollar.SetMoneyPoint(_moneyPoint);
        dollar.SetCashCounter(_cashCounter);
        dollar.StartMove();
    }

    private IEnumerator InstantiateDollars(Transform transform, int count)
    {
        for (int i = 0; i < count; i++)
        {
            InstantiateDollar(transform);
            yield return _waitForSeconds;
        }
    }

    public void AddCone(Cone cone)
    {
        Vector3 nextRotation = new(0, -90, 0);

        cone.transform.DOJump(transform.position, PowerJumpCone, NumJumpsCone, DurationJumpCone)
            .SetUpdate(UpdateType.Normal, false)
            .SetLink(cone.gameObject)
            .OnKill(() =>
            {
                cone.transform.SetParent(transform, true);
                cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
                CreateDollars(_positionSpawnDollar, cone.CountDollarsSpawned);
                cone.StartFallAnimation();
                cone.PlayFallSound();
            }
            );

        _cone = cone;
        IsFree = false;
    }
}
