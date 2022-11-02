using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
public class ConePoint : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;
    [SerializeField] private Dollar _dollarPrefab;

    private Dollar _dollar;
    private Renderer _renderer;
    private Cone _cone;

    private const float PowerJumpCone = 1f;
    private const float DurationJumpCone = 0.2f;
    private const int NumJumpsCone = 1;

    private const float PowerFallDollar = 1f;
    private const float DurationFallDollar = 0.6f;
    private const int NumsFallsDollar = 1;

    private const float PowerFlightDollar = 1f;
    private const float DurationFlightDollar = 1f;
    private const int NumFlightsDollar = 1;

    public bool IsFree { get; private set; }

    private void Start()
    {
        IsFree = true;
        _renderer = GetComponent<Renderer>();
    }

    private void InstantiateDollar()
    {
        _dollar = Instantiate(_dollarPrefab, transform.position, Quaternion.identity);
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

        InstantiateDollar();

        _dollar.StartTurn();

        _dollar.transform.DOJump(_dollar.GetTarget(), PowerFallDollar, NumsFallsDollar, DurationFallDollar)
            .OnComplete(() =>
            {
                _dollar.transform.DOJump(_dollar.GetCollectionPoint(), PowerFlightDollar, NumFlightsDollar, DurationFlightDollar)
                    .OnComplete(() =>
                    {
                        Destroy(_dollar.gameObject);
                    }
                    );
            }
            );
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
