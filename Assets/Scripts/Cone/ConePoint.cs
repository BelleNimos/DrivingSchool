using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConePoint : MonoBehaviour
{
    [SerializeField] private Material _NewMaterial;

    private Renderer _renderer;

    private List<Cone> _cones = new List<Cone>();
    private const float JumpPower = 2f;
    private const float Duration = 0.2f;
    private const int NumsJump = 1;

    public int CurrentConesCount => _cones.Count;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _cones = new List<Cone>();
    }

    public void AddCone(Cone cone)
    {
        Vector3 nextRotation = new Vector3(0, -90, 0);

        cone.transform.DOJump(transform.position, JumpPower, NumsJump, Duration)
            .OnComplete(() =>
            {
                cone.transform.SetParent(transform, true);
                cone.transform.localRotation = Quaternion.LookRotation(nextRotation);
                cone.StartFallAnimation();
            }
            );

        _cones.Add(cone);
        
        _renderer.material = _NewMaterial;
    }
}
