using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpgradesPanel : MonoBehaviour
{
    [SerializeField] private SouthButtonsPanel _southButtonsPanel;
    [SerializeField] private AudioSource _departure;

    private Animator _animator;

    private const string Open = "Open";
    private const string Close = "Close";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenPanel()
    {
        _southButtonsPanel.gameObject.SetActive(false);
        _departure.Play();
        _animator.SetTrigger(Open);
    }

    public void ClosePanel()
    {
        _southButtonsPanel.gameObject.SetActive(true);
        _animator.SetTrigger(Close);
    }
}
