using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Chose : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject _chapterSelectionMenu;
    [SerializeField] GameObject _selectedChapter;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (name == "Preview")
        {
            _chapterSelectionMenu.SetActive(false);
            _selectedChapter.SetActive(true);
        }
        else if (name == "LevelButton")
        {
            string _sceneName = transform.parent.name;
            SceneManager.LoadScene(_sceneName);
        }
    }
}

