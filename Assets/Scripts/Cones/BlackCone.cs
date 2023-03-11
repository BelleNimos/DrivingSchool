public class BlackCone : Cone
{
    private void Awake()
    {
        CountDollars = 10;
    }

    public override int GetIndex()
    {
        return 10;
    }
}
