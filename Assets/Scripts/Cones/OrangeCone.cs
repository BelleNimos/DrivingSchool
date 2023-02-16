using UnityEngine;

public class OrangeCone : Cone
{
    public override void CreateDollar(Transform transform)
    {
        InstantiateDollar(transform);
    }
}
