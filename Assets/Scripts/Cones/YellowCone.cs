public class YellowCone : Cone
{
    private void Awake()
    {
        CountDollars = 5;
    }

    public override int GetIndex()
    {
        return 5;
    }
}
