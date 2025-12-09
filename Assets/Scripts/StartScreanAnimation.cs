using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreanAnimation : MonoBehaviour
{
    [SerializeField] Image _blackScrean;
    [SerializeField] CanvasGroup _blackScreanAlpha;
    [SerializeField] CanvasGroup _logoAlpha;
    private float _speed = 1f;


    private void Awake()
    {
        SaveManager.LoadOrCreate();
        _logoAlpha.alpha = 0;
        _blackScreanAlpha.alpha = 1;
    }
    private void Start()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(LogoView(0f, 1f));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(LogoView(1f, 0f));
        yield return new WaitForSeconds(0.5f);

        _blackScrean.raycastTarget = false;
        float progres = 0f;
        while (progres < 1)
        {
            progres += Time.deltaTime;
            _blackScreanAlpha.alpha = Mathf.Lerp(1, 0, progres / _speed);
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator LogoView(float from, float to)
    {
        float progres = 0f;
        while (progres < 1)
        {
            progres += Time.deltaTime;
            _logoAlpha.alpha = Mathf.Lerp(from, to, progres / _speed);
            yield return null;
        }
    }
}

