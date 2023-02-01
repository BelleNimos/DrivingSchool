using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private Bag _bag;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;
    [SerializeField] private int _idNextLevel;

    private bool _isUse;

    private void Start()
    {
        _priceText.text = _price.ToString();
        _isUse = false;
        _idNextLevel -= 1;
    }

    private void ChangeOptionsNextLevel()
    {
        SceneData.ChangeCapacityBag(_bag.MaxConesCount);
        SceneData.ChangeCountWaveSpawner(_spawner.CountWave);
        SceneData.ChangeMoveSpeedPlayer(_characterMovement.MoveSpeed);
        SceneData.ChangeMoneyPlayer(_moneyPoint.CurrentDollarsCount);
        SceneData.ChangeSpawnerUpgradePrice(_upgrades.SpawnerPrice);
        SceneData.ChangeSpeedUpgradePrice(_upgrades.SpeedPrice);
        SceneData.ChangeBagUpgradePrice(_upgrades.BagPrice);
    }

    public void GoToNextLevel()
    {
        if (_moneyPoint.CurrentDollarsCount >= _price && _isUse == false)
        {
            _moneyPoint.SpendMoney(_price);
            _isUse = true;
            ChangeOptionsNextLevel();
            SceneManager.LoadScene(_idNextLevel);
        }
    }
}
