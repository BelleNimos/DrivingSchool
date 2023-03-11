public class RedCone : Cone
{
    private void Awake()
    {
        CountDollars = 3;
    }

    public override int GetIndex()
    {
        return 3;
    }
}
