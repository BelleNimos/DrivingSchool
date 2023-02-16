using System.Collections;
using UnityEngine;

public class YellowCone : Cone
{
    private IEnumerator InstantiateDollars(Transform transform)
    {
        CountDollars = 5;

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
