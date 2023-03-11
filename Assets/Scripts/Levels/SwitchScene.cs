using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey(KeysData.IndexScene) == true)
            SceneManager.LoadScene(PlayerPrefs.GetInt(KeysData.IndexScene));
        else
            SceneManager.LoadScene(2);
    }
}
