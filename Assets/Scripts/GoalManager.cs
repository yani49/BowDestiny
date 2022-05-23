using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    [SerializeField]private GameObject EndGameObject = null;
    [SerializeField]private GameObject DeadWindow = null;
    [SerializeField]private GameObject WindWindow = null;
    [SerializeField] private GameObject m_player = null;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            HidePanels();
        }
    }

    public void ShowPanels(int chooseScreen)
    {
        switch(chooseScreen)
        {
            case 0:
                m_player.GetComponent<playerMovement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                m_player.GetComponentInChildren<MouseLook>().enabled = false;
                DeadWindow.SetActive(true);
            break;

            case 1:
                m_player.GetComponent<playerMovement>().enabled = false;
                m_player.GetComponentInChildren<MouseLook>().enabled = false;
                WindWindow.SetActive(true);
                break;
        }
    }

    private void HidePanels()
    {
        m_player.GetComponent<playerMovement>().enabled = true;
        m_player.GetComponentInChildren<MouseLook>().enabled = true;
        EndGameObject.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}