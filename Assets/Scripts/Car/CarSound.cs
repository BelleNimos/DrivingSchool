using UnityEngine;

public class CarSound : MonoBehaviour
{
    [SerializeField] private AudioSource _soundMove;
    [SerializeField] private AudioSource _soundTurn;

    private void Start()
    {
        _soundMove.pitch = 0;
        _soundTurn.pitch = 0;
    }

    public void PlayMove()
    {
        _soundMove.pitch = Random.Range(2.8f, 3f);
    }

    public void PlayTurn()
    {
        _soundTurn.pitch = Random.Range(0.9f, 1.1f);
    }

    public void PlayIdle()
    {
        _soundMove.pitch = 1f;
        _soundTurn.pitch = 0f;
    }

    public void Stop()
    {
        _soundMove.pitch = 0f;
        _soundTurn.pitch = 0f;
    }
}
