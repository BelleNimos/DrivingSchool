using UnityEngine;
using TMPro;

public class MoneyPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text _dollarsCount;
    [SerializeField] private AudioSource _spendDollarsSound;

    private int _countDollars;

    public int CurrentDollarsCount => _countDollars;

    private void Start()
    {
        _countDollars = 0;

        if (PlayerPrefs.HasKey(KeysData.PlayerDollarsCount) == true)
            _countDollars = PlayerPrefs.GetInt(KeysData.PlayerDollarsCount);

        _dollarsCount.text = _countDollars.ToString();
    }

    public void AddDollar()
    {
        _countDollars++;
        _dollarsCount.text = _countDollars.ToString();
    }

    public void AddDollars(int count)
    {
        for (int i = 0; i < count; i++)
            AddDollar();
    }

    public void SpendMoney(int price)
    {
        _spendDollarsSound.Play();
        _countDollars -= price;
        _dollarsCount.text = _countDollars.ToString();
    }
}
