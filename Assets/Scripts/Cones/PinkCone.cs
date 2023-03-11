public class PinkCone : Cone
{
    private void Awake()
    {
        CountDollars = 7;
    }

    public override int GetIndex()
    {
        return 7;
    }
}
