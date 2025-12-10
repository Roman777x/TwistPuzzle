using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{

    SnakeController _snakeController;
    [SerializeField] GameObject _background;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _victory;
    [SerializeField] GameObject _fail;
    public bool isFinish = false;

    private void Awake()
    {
        _snakeController = FindAnyObjectByType<SnakeController>();
        _background.SetActive(false);
        _menu.SetActive(false);
        _settings.SetActive(false);
    }

    private void Update()
    {
        if (!isFinish)
        {
            switch (true)
            {
                case true when Input.GetKeyDown(KeyCode.R):
                    ResetLevel();
                    break;
                case true when Input.GetKeyDown(KeyCode.Escape):
                    Menu();
                    break;
            }
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        _background.SetActive(!_background.activeSelf);
        _menu.SetActive(!_menu.activeSelf);
        _settings.SetActive(false);
        _snakeController.canMove = !_snakeController.canMove;
    }

    public void Settings()
    {
        _settings.SetActive(!_settings.activeSelf);
    }

    public void ComplitLevel(bool isWin)
    {
        if (isWin)
        {
            GameData.CompliteForestLevels[SceneManager.GetActiveScene().name] = true;
            _victory.SetActive(true);
        }
        else _fail.SetActive(true);
    }
    public void ExitToMainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
