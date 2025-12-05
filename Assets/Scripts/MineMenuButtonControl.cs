using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MineMenuButtonControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject _controlMenu;
    [SerializeField] GameObject _pastControlMenu;

    private void Awake()
    {
        if (_controlMenu)
        {
            _controlMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (name == "Back" && Input.GetKeyDown(KeyCode.Escape))
        {
            Event();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Event();
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