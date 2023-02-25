using UnityEngine;

public static class SceneData
{
    public static int SpawnerUpgradePrice { get; private set; } = 0;
    public static int SpeedUpgradePrice { get; private set; } = 0;
    public static int BagUpgradePrice { get; private set; } = 0;
    public static int MoneyPlayer { get; private set; } = 0;
    public static int CapacityBag { get; private set; } = 0;
    public static int CountWaveSpawner { get; private set; } = 0;
    public static float MoveSpeedPlayer { get; private set; } = 0;
    public static float SpeedAnimatorBag { get; private set; } = 0;
    public static float SpeedAnimatorPlayer { get; private set; } = 0;
    public static Vector3 RadiusPlayer { get; private set; } = Vector3.zero;

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

    public static void ChangeSpeedAnimatorBag(float speed)
    {
        SpeedAnimatorBag = speed;
    }

    public static void ChangeSpeedAnimatorPlayer(float speed)
    {
        SpeedAnimatorPlayer = speed;
    }

    public static void ChangeRadiusPlayer(Vector3 radius)
    {
        RadiusPlayer = radius;
    }
}
