using UnityEngine;

public class OrangeCone : Cone
{
    public override void CreateDollar(MoneyPoint moneyPoint, Transform transform)
    {
        InstantiateDollar(moneyPoint, transform);
    }
}
