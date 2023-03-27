using DG.Tweening;
using UnityEngine;

public class Utilizer : MonoBehaviour
{
    [SerializeField] private AudioSource _destroyConeSound;

    private const int NumJumps = 1;
    private const float JumpPower = 2f;
    private const float Duration = 0.2f;

    public readonly float MinTime = 0.05f;

    public float Timer { get; private set; }

    private void Start()
    {
        Timer = 0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void DestroyCone(Cone cone)
    {
        _destroyConeSound.Play();

        cone.transform.DOJump((transform.position), JumpPower, NumJumps, Duration)
            .SetUpdate(UpdateType.Normal, false)
            .SetLink(cone.gameObject)
            .OnKill(() =>
            {
                cone.transform.SetParent(transform, true);
                Destroy(cone.gameObject);
                
            }
            );

        Timer = 0f;
    }
}
