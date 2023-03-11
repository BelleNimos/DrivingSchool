public class GreenCone : Cone
{
    private void Awake()
    {
        CountDollars = 4;
    }

    public override int GetIndex()
    {
        return 4;
    }
}
