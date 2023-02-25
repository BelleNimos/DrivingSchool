using UnityEngine;

public class Utilizer : MonoBehaviour
{
    [SerializeField] private AudioSource _destroyConeSound;

    public void DestroyCone(Cone cone)
    {
        _destroyConeSound.Play();
        Destroy(cone.gameObject);
    }
}
