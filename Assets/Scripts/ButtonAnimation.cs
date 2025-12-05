using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RawImage _button;
    [SerializeField] Texture _textureDefault;
    [SerializeField] Texture _textureEnter;

    private void Awake()
    {
        _button = GetComponent<RawImage>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.texture = _textureEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button.texture = _textureDefault;
    }
}
