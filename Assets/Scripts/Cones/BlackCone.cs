using System.Collections;
using UnityEngine;

public class BlackCone : Cone
{
    private IEnumerator InstantiateDollars(Transform transform)
    {
        CountDollars = 10;

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
