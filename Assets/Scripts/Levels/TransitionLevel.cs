using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    [SerializeField] private AudioSource _yesSound;
    [SerializeField] private AudioSource _noSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private NextLevelUI _nextLevelUI;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Bag _bag;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;
    [SerializeField] private int _idNextLevel;

    private bool _isEmpty;

    private void Start()
    {
        _idNextLevel -= 1;
        _priceText.text = _price.ToString();
        _isEmpty = false;
    }

    private void Update()
    {
        if (_isEmpty == true)
        {
            ChangeOptionsNextLevel();
            SceneManager.LoadScene(_idNextLevel);
            Time.timeScale = 1;
        }
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
        SceneData.ChangeSpeedAnimatorBag(_bag.SpeedAnimator);
        SceneData.ChangeSpeedAnimatorPlayer(_playerAnimator.SpeedAnimator);
        SceneData.ChangeRadiusPlayer(_characterMovement.Radius);
    }

    public void OpenPanel()
    {
        if (_nextLevelUI.IsActive == false)
            _nextLevelUI.OpenPanel();
    }

    public void ClosePanel()
    {
        if (_nextLevelUI.IsActive == true)
        {
            _noSound.Play();
            _nextLevelUI.ClosePanel();
        }
    }

    public void GoToNextLevel()
    {
        if (_moneyPoint.CurrentDollarsCount >= _price)
        {
            _yesSound.Play();
            _moneyPoint.SpendMoney(_price);
            _isEmpty = true;
        }
        else
        {
            _crashSound.Play();
            _isEmpty = false;
        }
    }
}
