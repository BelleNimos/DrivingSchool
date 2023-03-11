public class WhiteCone : Cone
{
    private void Awake()
    {
        CountDollars = 11;
    }

    public override int GetIndex()
    {
        return 11;
    }
}
