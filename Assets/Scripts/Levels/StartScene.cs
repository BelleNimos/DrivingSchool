using Agava.YandexGames;
using System.Collections;
using UnityEngine;
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

        while (true)
        {
            if (YandexGamesSdk.IsInitialized == true)
                SceneManager.LoadScene(1);

            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
}
