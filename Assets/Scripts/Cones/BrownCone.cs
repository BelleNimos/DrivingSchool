public class BrownCone : Cone
{
    private void Awake()
    {
        CountDollars = 8;
    }

    public override int GetIndex()
    {
        return 8;
    }
}
