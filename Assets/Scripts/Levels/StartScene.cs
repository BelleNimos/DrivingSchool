using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();
    }

    private void Update()
    {
        if (YandexGamesSdk.IsInitialized == true)
            SceneManager.LoadScene(1);
    }
}
