using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystickArea;
    [SerializeField] private Image _joystick;

    [SerializeField] private Color _inActiveJoystickColor;
    [SerializeField] private Color _activeJoystickColor;

    [SerializeField] private Color _inActiveBackgroundColor;
    [SerializeField] private Color _activeBackgroundColor;

    private Vector2 _backgroundStartPosition;
    private bool _joystickIsActive = true;

    protected Vector2 InputVector;

    private void OnEnable()
    {
        ClickEffect();

        _backgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    private void ClickEffect()
    {
        if (_joystickIsActive == false)
        {
            _joystick.color = _activeJoystickColor;
            _joystickBackground.color = _activeBackgroundColor;
            _joystickIsActive = true;
        }
        else
        {
            _joystick.color = _inActiveJoystickColor;
            _joystickBackground.color = _inActiveBackgroundColor;
            _joystickIsActive = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, _camera, out joystickPosition))
        {
            joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
            joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

            InputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            InputVector = (InputVector.magnitude > 1f) ? InputVector.normalized : InputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x * _joystickBackground.rectTransform.sizeDelta.x / 2, InputVector.y * _joystickBackground.rectTransform.sizeDelta.y / 2);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 backgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, _camera, out backgroundPosition))
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(backgroundPosition.x, backgroundPosition.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _backgroundStartPosition;

        ClickEffect();

        InputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}
