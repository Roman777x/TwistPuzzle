using UnityEngine;
using UnityEngine.EventSystems;

public class LevelButtonControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] LevelSceneControl _leveledControl;
        public void OnPointerClick(PointerEventData eventData)
    {
        switch (true)
        {
            case true when gameObject.name == "Pause":
                _leveledControl.OpenLevelMenu();
                break;
            
            case true when gameObject.name == "Restart":
                _leveledControl.ResetLevel();
                break;
        }
    }
}
