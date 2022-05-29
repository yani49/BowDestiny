using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(int m_sceneindex)
    {
        SceneManager.LoadScene(m_sceneindex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}