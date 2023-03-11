using UnityEngine;

public class Utilizer : MonoBehaviour
{
    [SerializeField] private AudioSource _destroyConeSound;

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
        Destroy(cone.gameObject);
        Timer = 0f;
    }
}
