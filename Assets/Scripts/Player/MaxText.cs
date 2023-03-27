using UnityEngine;

public class MaxText : InternationalText
{
    private void Start()
    {
        Disable();
    }

    public void Enable()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Disable()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
