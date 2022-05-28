using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private GameObject EndGameObject = null;
    [SerializeField] private GameObject DeadWindow = null;
    [SerializeField] private GameObject WindWindow = null;
    [SerializeField] private GameObject m_player = null;
    [SerializeField] private UIManager m_uiManager = null;

    [SerializeField] private UpgradeManager m_upgradesManager = null;

    [SerializeField] private int m_maxEnemyCounter = 1;
    private int m_SlainEnemyCounter = 0;
    int m_multiplier = 1;

    private void Start()
    {
        m_uiManager.UpdateGoalBar(m_maxEnemyCounter,m_SlainEnemyCounter);        
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            HidePanels();
        }
    }

    public void ShowPanels(int chooseScreen)
    {
        Cursor.lockState = CursorLockMode.None;
        switch(chooseScreen)
        {
            //when player die
            case 0:
                m_player.GetComponent<playerMovement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                m_player.GetComponentInChildren<MouseLook>().enabled = false;
                DeadWindow.SetActive(true);
            break;

            //when you reach a goal
            case 1:
                m_player.GetComponent<PlayerStats>().m_level = m_player.GetComponent<PlayerStats>().m_level + 1;
                m_player.GetComponent<playerMovement>().enabled = false;
                m_player.GetComponentInChildren<MouseLook>().enabled = false;
                WindWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "You upgraded to level " + m_player.GetComponent<PlayerStats>().m_level;
                Cursor.lockState = CursorLockMode.None;
                WindWindow.SetActive(true);
                break;
        }
    }

    private void HidePanels()
    {
        m_player.GetComponent<playerMovement>().enabled = true;
        m_player.GetComponentInChildren<MouseLook>().enabled = true;
        EndGameObject.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        WindWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckEngGoal()
    {
        m_SlainEnemyCounter = m_SlainEnemyCounter + 1;
        m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);

        if (m_SlainEnemyCounter == m_maxEnemyCounter)
        {

            ShowPanels(1);
        }
    }

    public void ContinueGame()
    {
        m_maxEnemyCounter = m_multiplier * m_player.GetComponent<PlayerStats>().m_level;
        m_SlainEnemyCounter = 0;

        HidePanels();
        m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);
    }
}