using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

{
    private RawImage _button;
    [SerializeField] Texture _textureDefault;
    [SerializeField] Texture _textureEnter;
    [SerializeField] GameObject _controlMenu;
    [SerializeField] GameObject _pastControlMenu;

    private void Awake()
    {
        _button = GetComponent<RawImage>();
        if (_controlMenu)
        {
            _controlMenu.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.texture = _textureEnter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _button.texture = _textureDefault;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Event();
    }
    private void Update()
    {
        if (name == "Back" && Input.GetKeyDown(KeyCode.Escape))
        {
            Event();
        }
    }

    private void Event()
    {
        if (_controlMenu)
        {
            _controlMenu.SetActive(!_controlMenu.activeSelf);
        }
        if (_pastControlMenu)
        {
            _pastControlMenu.SetActive(!_pastControlMenu.activeSelf); 
        }
    }
}