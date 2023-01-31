public static class SceneData
{
    public static int SpawnerUpgradePrice { get; private set; } = 0;
    public static int SpeedUpgradePrice { get; private set; } = 0;
    public static int BagUpgradePrice { get; private set; } = 0;
    public static int MoneyPlayer { get; private set; } = 0;
    public static int CapacityBag { get; private set; } = 0;
    public static int CountWaveSpawner { get; private set; } = 0;
    public static float MoveSpeedPlayer { get; private set; } = 0;

    public static void ChangeSpawnerUpgradePrice(int price)
    {
        SpawnerUpgradePrice = price;
    }

    public static void ChangeSpeedUpgradePrice(int price)
    {
        SpeedUpgradePrice = price;
    }

    public static void ChangeBagUpgradePrice(int price)
    {
        BagUpgradePrice = price;
    }

    public static void ChangeMoneyPlayer(int money)
    {
        MoneyPlayer = money;
    }

    public static void ChangeCapacityBag(int capacity)
    {
        CapacityBag = capacity;
    }

    public static void ChangeCountWaveSpawner(int count)
    {
        CountWaveSpawner = count;
    }

    public static void ChangeMoveSpeedPlayer(float speed)
    {
        MoveSpeedPlayer = speed;
    }
}
