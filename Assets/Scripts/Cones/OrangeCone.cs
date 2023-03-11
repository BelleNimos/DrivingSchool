public class OrangeCone : Cone
{
    private void Awake()
    {
        CountDollars = 1;
    }

    public override int GetIndex()
    {
        return 1;
    }
}
