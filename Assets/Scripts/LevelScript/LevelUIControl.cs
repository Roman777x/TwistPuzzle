using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUIControl : MonoBehaviour, IPointerClickHandler
{
    LevelControl _levelControl;

    private void Start()
    {
        _levelControl = FindAnyObjectByType<LevelControl>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (true)
        {
            case true when gameObject.CompareTag("MenuButton"):
                _levelControl.Menu();
                break;
            
            case true when gameObject.CompareTag("RestartButton"):
                _levelControl.ResetLevel();
                break;

            case true when gameObject.CompareTag("SettingsButton"):
                _levelControl.Settings();
                break;
            
            case true when gameObject.CompareTag("ExitButton"):
                _levelControl.ExitToMainmenu();
                break;

        }
    }
}
