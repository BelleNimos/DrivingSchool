using System.Collections;
using UnityEngine;

public class BlackCone : Cone
{
    private IEnumerator InstantiateDollars(MoneyPoint moneyPoint, Transform transform)
    {
        CountDollars = 10;

        for (int i = 0; i < CountDollars; i++)
        {
            InstantiateDollar(moneyPoint, transform);
            yield return WaitForSeconds;
        }
    }

    public override void CreateDollar(MoneyPoint moneyPoint, Transform transform)
    {
        StartCoroutine(InstantiateDollars(moneyPoint, transform));
    }
}
