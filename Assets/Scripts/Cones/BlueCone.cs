public class BlueCone : Cone
{
    private void Awake()
    {
        CountDollars = 2;
    }

    public override int GetIndex()
    {
        return 2;
    }
}
