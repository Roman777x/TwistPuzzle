using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneControl : MonoBehaviour
{

    [SerializeField] SnakeController _snakeController;
    [SerializeField] GameObject _menu;

    private void Awake()
    {
        _menu.SetActive(false);
    }

    private void Update()
    {
        switch (true)
        {
            case true when Input.GetKeyDown(KeyCode.R):
                ResetLevel();
                break;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenLevelMenu()
    {
        _menu.SetActive(!_menu.activeSelf);
        _snakeController._canMove = !_snakeController._canMove;
    }
}
