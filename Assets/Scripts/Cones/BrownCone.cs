using System.Collections;
using UnityEngine;

public class BrownCone : Cone
{
    private IEnumerator InstantiateDollars(Transform transform)
    {
        CountDollars = 8;

        for (int i = 0; i < CountDollars; i++)
        {
            InstantiateDollar(transform);
            yield return WaitForSeconds;
        }
    }

    public override void CreateDollar(Transform transform)
    {
        StartCoroutine(InstantiateDollars(transform));
    }
}
