using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicDefender : MonoBehaviour
{
    [SerializeField] private GameObject EndGameObject = null;
    [SerializeField] private GameObject DeadWindow = null;
    [SerializeField] private GameObject PlayerDiedWindow = null;
    [SerializeField] private GameObject SmallWinWindow = null;

    //[SerializeField] private GameObject m_player = null;
    [SerializeField] private playerMovement m_playermovement;
    [SerializeField] private MouseLook m_playerMouseMove;
    [SerializeField] private PlayerStats m_playerStats;

    [SerializeField] private UIManager m_uiManager = null;
    [SerializeField] private RandomWordGen m_WordGen;

    [SerializeField] private UpgradeManager m_upgradesManager = null;

    [SerializeField] private int m_maxEnemyCounter = 1;
    private int m_SlainEnemyCounter = 0;
    int m_multiplier = 1;

    [SerializeField] private int m_Maxrelic_HP = 50;
    
    private int m_CurrentRelicHP = 0;

    [SerializeField] private AreaSpawnerArea[] m_spwaners;

    GameObject[] GObla;

    private void Awake()
    {
        m_CurrentRelicHP = m_Maxrelic_HP;
        m_uiManager.RelicHealthBar(m_Maxrelic_HP, m_CurrentRelicHP);

        SpawnEnemies();
    }

    public void ShowPanels(int chooseScreen)
    {
        Cursor.lockState = CursorLockMode.None;

        m_WordGen.enabled = false;
        m_playermovement.enabled = false;
        m_playerMouseMove.enabled = false;
        switch (chooseScreen)
        {
            //when enemies destroy relic
            case 0:
                DeadWindow.SetActive(true);
                break;

            //when you defend the relic
            case 1:
                m_playerStats.m_level = m_playerStats.m_level + 1;

                SmallWinWindow.SetActive(true);

                GObla = GameObject.FindGameObjectsWithTag("Enemy");
                if(m_Maxrelic_HP > 0)
                {
                    foreach (GameObject i in GObla)
                    {
                        Destroy(i);
                    }
                }

                float dividingbyPlayerLevel = m_playerStats.m_level % 5;
                print("<color=red>rezultat je " + dividingbyPlayerLevel + "</color>");

                if (dividingbyPlayerLevel == 0)
                {   
                    
                    m_maxEnemyCounter = 1;
                    foreach(AreaSpawnerArea spawner in m_spwaners)
                    {
                        spawner.m_modifierEnemyHP += +2.0f;
                        spawner.m_modifierEnemyDMG += +3.0f;
                    }
                }
                else if(dividingbyPlayerLevel != 0)
                {
                    m_maxEnemyCounter = m_maxEnemyCounter + 1;
                }
                m_SlainEnemyCounter = 0;
                m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);

                int modfierforHP = 2;
                m_playerStats.LevelUpHPPlayer(modfierforHP, false, true);
                break;
            
            //when player die
            case 2:
                GObla = GameObject.FindGameObjectsWithTag("Enemy");
                int m_counter = 0;
                foreach (GameObject i in GObla)
                {
                    if(!i.GetComponent<Enemy_Master>().isDead)
                    {
                        Destroy(i);
                        m_counter =+1;
                    }
                    AttackingRelic(m_counter);
                }

                if (m_CurrentRelicHP <= 0)
                {
                    DeadWindow.SetActive(true);
                }

                else if (m_CurrentRelicHP > 0)
                {
                    m_playerStats.ResetPlayerHP();
                    PlayerDiedWindow.SetActive(true);
                    m_upgradesManager.RandomReward();
                    foreach (GameObject i in GObla)
                    {
                        Destroy(i);
                    }
                    m_SlainEnemyCounter = 0;
                    m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);
                }
                // nagradi igralca

                break;
        }
    }

    private void HidePanels()
    {
        m_playermovement.enabled = true;
        m_playerMouseMove.enabled = true;
        m_WordGen.enabled = true;
        EndGameObject.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        PlayerDiedWindow.SetActive(false);
        SmallWinWindow.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void AttackingRelic(float wound)
    {
        if(m_CurrentRelicHP > 0)
        {
            m_CurrentRelicHP = m_CurrentRelicHP - Mathf.FloorToInt(wound);
            m_uiManager.RelicHealthBar(m_Maxrelic_HP, m_CurrentRelicHP);
        }
        else if(m_CurrentRelicHP <= 0)
        {
            ShowPanels(0);
        }        
    }

    public void CheckEnemiesCount()
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
        m_upgradesManager.GetReward();
        m_playerStats.GetComponent<Weapon_Control>().UpgradeWeapon();
        SpawnEnemies();
        HidePanels();
    }

    private void SpawnEnemies()
    {
        int temp_saver = m_maxEnemyCounter;
        while (m_maxEnemyCounter > 0)
        {
            for (int j = 0; j < m_spwaners.Length; j++)
            {
                var numLeftToTry = (m_spwaners.Length) - j;
                var chance = m_maxEnemyCounter / numLeftToTry;

                if (Random.value < chance)
                {
                    m_spwaners[Random.Range(0,m_spwaners.Length)].bla_Spawner(1,0,0);
                    m_maxEnemyCounter--;                    
                }
            }
        }
        m_maxEnemyCounter = temp_saver;
    }    
}