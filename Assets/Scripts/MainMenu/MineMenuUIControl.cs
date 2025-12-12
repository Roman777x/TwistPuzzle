using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MineMenuUIControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] MainMenuControl MainMenuControl;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (true)
        {
            case true when gameObject.CompareTag("Back"):
                MainMenuControl.CloseLastWindows();
                break;
            
            case true when gameObject.CompareTag("Start"):
                MainMenuControl.OpenChapterSelection();
                break;
            
            case true when gameObject.CompareTag("Settings"):
                MainMenuControl.OpenSettings();
                break;
            case true when gameObject.CompareTag("Shop"):
                MainMenuControl.OpenShop();
                break;
            case true when gameObject.CompareTag("Exit"):
                MainMenuControl.QuitGame();
                break;
            case true when gameObject.CompareTag("Chapter"):
                MainMenuControl.OpenChapter(gameObject.name);
                break;
            case true when gameObject.CompareTag("LevelButton"):
                MainMenuControl.LoadLevel(gameObject.transform.parent.name);
                break;
        }
    }
}