public class PurpleCone : Cone
{
    private void Awake()
    {
        CountDollars = 6;
    }

    public override int GetIndex()
    {
        return 6;
    }
}
