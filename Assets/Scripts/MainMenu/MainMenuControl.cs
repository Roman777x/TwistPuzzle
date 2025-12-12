using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject ChaptelSelection;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject ForestLevels;

    List<GameObject> OpenWindow = new List<GameObject>();

    private void Start()
    {
        ChaptelSelection.SetActive(false);
        Settings.SetActive(false);
        Shop.SetActive(false);
        ForestLevels.SetActive(false);
    }

    public void CloseLastWindows()
    {
        int _last = OpenWindow.Count - 1;
        OpenWindow[_last].SetActive(false);
        OpenWindow.RemoveAt(_last);
    }
    public void OpenChapterSelection()
    {
        ChaptelSelection.SetActive(true);
        OpenWindow.Add(ChaptelSelection);
    }

    public void OpenSettings()
    {
        Settings.SetActive(true);
        OpenWindow.Add(Settings);
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
        OpenWindow.Add(Shop);
    }
    public void QuitGame()
    {
        SaveManager.Save();
        Application.Quit();
    }

    public void OpenChapter(string Name)
    {
        
        if (Name == "Forest")
        {
            ForestLevels.SetActive(true);
            ForestLevels.GetComponentInChildren<CheckLevels>().Check();
            OpenWindow.Add(ForestLevels);
        }
    }

    public void LoadLevel(string levelNumper)
    {
        string chapter = OpenWindow[OpenWindow.Count - 1].name;
        SceneManager.LoadScene(chapter + levelNumper);
    }
}
