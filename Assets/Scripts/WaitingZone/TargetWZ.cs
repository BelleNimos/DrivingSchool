using UnityEngine;

public class TargetWZ : MonoBehaviour
{
    public bool IsFree { get; private set; }

    private void Start()
    {
        IsFree = true;
    }

    public void Unlock()
    {
        IsFree = true;
    }

    public void Block()
    {
        IsFree = false;
    }
}
