using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DollarsCountPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _countDollarsText;

    private Animator _animator;

    private const string AddMoneyText = "AddMoney";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimationAddMoney()
    {
        _animator.SetTrigger(AddMoneyText);
    }

    public void SetCountDollars(int count)
    {
        _countDollarsText.text = count.ToString();
    }
}
