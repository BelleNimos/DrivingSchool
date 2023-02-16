using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text _dollarsCount;

    private Stack<Dollar> _dollars;

    public int CurrentDollarsCount => _dollars.Count;

    private void Start()
    {
        _dollars = new Stack<Dollar>();

        if (SceneData.MoneyPlayer > 0)
            for (int i = 0; i < SceneData.MoneyPlayer; i++)
                _dollars.Push(new Dollar());
    }

    private void Update()
    {
        _dollarsCount.text = CurrentDollarsCount.ToString();
    }

    public void AddDollar(Dollar dollar)
    {
        _dollars.Push(dollar);
    }

    public void SpendMoney(int price)
    {
        if (_dollars.Count >= price)
            for (int i = 0; i < price; i++)
                _dollars.Pop();
    }
}
