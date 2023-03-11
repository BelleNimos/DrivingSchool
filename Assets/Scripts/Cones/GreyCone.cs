public class GreyCone : Cone
{
    private void Awake()
    {
        CountDollars = 9;
    }

    public override int GetIndex()
    {
        return 9;
    }
}
