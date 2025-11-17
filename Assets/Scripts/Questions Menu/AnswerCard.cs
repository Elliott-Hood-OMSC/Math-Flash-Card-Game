// Name: Elliott Hood - Noah Vu
// Student ID: 2422722 - 2424329
// Email: dhood@chapman.edu - novu@chapman.edu
// Course: GAME 245-01

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Invokes an event on click.
/// A glorified button that shakes and animates when hovered or selected.
/// </summary>
[RequireComponent(typeof(PointerSelectableButton))]
public class AnswerCard : MonoBehaviour
{
    [SerializeField] private RectTransform _visualsAnchor;
    [SerializeField] private float _clickShakeIntensity = 10f;
    [SerializeField] private int _clickShakeVibrato = 20;
    [SerializeField] private float _shakeRotationIntensity = 10f;
    [SerializeField] private float _selectionAnimationDuration = 0.2f;
    [SerializeField] private int _selectionAnimationVibrato = 30;
    [SerializeField] private float _scaleSelectedMultiplier = 1.1f;
    [SerializeField] private TextMeshProUGUI _text;

    private PointerSelectableButton _button;
    private Quaternion _originalRotation;
    private Vector3 _originalAnchorPos;
    private Vector3 _originalScale;

    public readonly UnityEvent OnClick = new UnityEvent();

    private void Awake()
    {
        _button = GetComponent<PointerSelectableButton>();
        _button.onClick.AddListener(OnButtonClick);
        _button.OnSelectEvent.AddListener(OnSelect);
        _button.OnDeselectEvent.AddListener(OnDeselect);

        // cache original state
        _originalRotation = _visualsAnchor.localRotation;
        _originalAnchorPos = _visualsAnchor.anchoredPosition;
        _originalScale = _visualsAnchor.localScale;
    }

    private void OnEnable()
    {
        // kill any leftover tweens in case object was disabled mid-animation
        _visualsAnchor.DOKill();

        // reset to original state
        _visualsAnchor.localRotation = _originalRotation;
        _visualsAnchor.anchoredPosition = _originalAnchorPos;
        _visualsAnchor.localScale = _originalScale;
    }

    private void OnSelect(BaseEventData arg0)
    {
        _visualsAnchor.DOKill();
        _visualsAnchor.DOScale(Vector3.one * _scaleSelectedMultiplier, _selectionAnimationDuration)
            .SetEase(Ease.OutBack);

        DOTween.Sequence()
            .Append(_visualsAnchor.DOShakeRotation(
                _selectionAnimationDuration,
                _shakeRotationIntensity,
                _selectionAnimationVibrato,
                randomnessMode: ShakeRandomnessMode.Harmonic))
            .Append(_visualsAnchor.DOLocalRotateQuaternion(_originalRotation, 0.05f)); // smooth reset
    }

    private void OnDeselect(BaseEventData arg0)
    {
        _visualsAnchor.DOKill();
        _visualsAnchor.DOScale(_originalScale, _selectionAnimationDuration)
            .SetEase(Ease.OutBack);

        // ensure rotation reset if it was mid-shake
        _visualsAnchor.DOLocalRotateQuaternion(_originalRotation, 0.05f);
    }

    private void OnButtonClick()
    {
        DOTween.Sequence()
            .Append(_visualsAnchor.DOShakeAnchorPos(
                _selectionAnimationDuration,
                _clickShakeIntensity,
                vibrato: _clickShakeVibrato))
            .Append(_visualsAnchor.DOAnchorPos(_originalAnchorPos, 0.05f)); // smooth reset

        OnClick?.Invoke();
    }

    public void SetValue(int value)
    {
        _text.text = value.ToString();
    }
}
