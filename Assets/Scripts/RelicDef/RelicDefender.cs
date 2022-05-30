using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RelicDefender : MonoBehaviour
{
    [SerializeField] private GameObject EndGameObject = null;
    [SerializeField] private GameObject DeadWindow = null;
    [SerializeField] private GameObject PlayerDiedWindow = null;
    [SerializeField] private GameObject SmallWinWindow = null;
    [SerializeField] private GameObject PauseWindows = null;
    [SerializeField] private TextMeshProUGUI m_waveCounter = null;
    [SerializeField] private TextMeshProUGUI m_enemyHpCounter = null;
    [SerializeField] private TextMeshProUGUI m_enemyDMGCounter= null;
    [SerializeField] private TextMeshProUGUI m_enemySpawnCounter = null; 
    [SerializeField] private TextMeshProUGUI m_attackTimer = null;

    private int totalSlainEnemies = 0;
    [SerializeField] private TextMeshProUGUI m_scoreCategory = null;

    private int m_scoreValue = 0;
    [SerializeField] private TextMeshProUGUI m_scoreIngame = null;
    [SerializeField] private TextMeshProUGUI m_scoreFinal = null;

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

    public bool testgameplay;
    private bool IsLToContinue= false;
    private bool isPaused;

    private float timer = 0f;
    private int m_waveCounternumber =1;

    private void Update()
    {
        if(timer >=1)
        {
            timer = timer - Time.deltaTime;
            m_attackTimer.text = "Game continues after: " + Mathf.FloorToInt(timer) + " s.";
        }

        if (IsLToContinue)
        {
            IsLToContinue = false;
            Invoke("ContinueGame", 5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAGame();
        }
    }

    public void PauseAGame()
    {
        isPaused = !isPaused;
        if(!isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            PauseWindows.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            PauseWindows.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void Awake()
    {
        m_waveCounter.text = "Wave: " + m_waveCounternumber.ToString();

        m_enemySpawnCounter.text = "Spawn: " + m_SlainEnemyCounter + " / " + m_maxEnemyCounter;
        m_CurrentRelicHP = m_Maxrelic_HP;



        m_scoreValue = m_scoreValue + (m_SlainEnemyCounter * m_CurrentRelicHP);
        m_scoreIngame.text = "Score: " + m_scoreValue.ToString();

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
                m_scoreCategory.text = "You killed total: " + totalSlainEnemies+" of enemies.\n And you survived until " + m_waveCounternumber.ToString() + " wave. GJ";
                m_scoreFinal.text = m_scoreValue.ToString();
                break;

            //when you defend the relic
            case 1:

                Cursor.lockState = CursorLockMode.Locked;
                m_playermovement.enabled = true;
                m_playerMouseMove.enabled = true;

                IsLToContinue = true;
                timer = 5f;
                
                //m_playerStats.m_level = m_playerStats.m_level + 1;

                SmallWinWindow.SetActive(true);

                GObla = GameObject.FindGameObjectsWithTag("Enemy");
                if(m_Maxrelic_HP > 0)
                {
                    foreach (GameObject i in GObla)
                    {
                        Destroy(i, 3.0f);
                        //Destroy(i);
                    }
                }
                m_waveCounternumber = m_waveCounternumber + 1;

                m_waveCounter.text = "Wave: " + m_waveCounternumber.ToString();
                m_enemySpawnCounter.text = "Spawn: " + m_SlainEnemyCounter + " / " + m_maxEnemyCounter;
                float dividingbyPlayerLevel = m_waveCounternumber % 5;
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
                    //m_maxEnemyCounter = m_maxEnemyCounter + m_playerStats.m_level;
                }

                //m_scoreValue = m_scoreValue+(m_SlainEnemyCounter * m_CurrentRelicHP);
                m_SlainEnemyCounter = 0;

                m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);
                break;
            
            //when player die
            case 2:
                GObla = GameObject.FindGameObjectsWithTag("Enemy");
                int m_counter = 0;
                foreach (GameObject i in GObla)
                {
                    if(!i.GetComponent<Enemy_Master>().isDead)
                    {
                        //Invoke(Destroy(i), 3f);
                        Destroy(i,3.0f);
                        
                        m_counter =+ Mathf.FloorToInt(i.GetComponent<Enemy_Master>().m_basicDamage);
                    }
                    AttackingRelic(m_counter);
                }

                if (m_CurrentRelicHP <= 0)
                {
                    DeadWindow.SetActive(true);
                }

                else if (m_CurrentRelicHP > 0)
                {
                    //m_playerStats.ResetPlayerHP();
                    //m_playerStats.LevelUpHPPlayer(2f, true, true);
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

                if (testgameplay)
                {
                    int modfierforHP = 2;
                    m_playerStats.LevelUpHPPlayer(modfierforHP, true, true);
                }
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
        SceneManager.LoadScene(1);
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
        totalSlainEnemies += 1;

        m_scoreValue = m_scoreValue + (m_SlainEnemyCounter * m_CurrentRelicHP);
        m_scoreIngame.text = "Score: " + m_scoreValue.ToString();
        m_uiManager.UpdateGoalBar(m_maxEnemyCounter, m_SlainEnemyCounter);

        //m_enemySpawnCounter.text = "We spawned: " + m_maxEnemyCounter + ". You killed: " + m_SlainEnemyCounter + ".";

        //m_enemySpawnCounter.text = m_SlainEnemyCounter + " / " + m_maxEnemyCounter;
        m_enemySpawnCounter.text = "Spawn: " + m_SlainEnemyCounter + " / " + m_maxEnemyCounter;

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
        
        m_enemyHpCounter.text = "HP: " + FindObjectOfType<Enemy_Master>().m_EnemyHP;
        m_enemyDMGCounter.text = "DMG: " + FindObjectOfType<Enemy_Master>().m_basicDamage;
        m_enemySpawnCounter.text = "Spawn: " + m_SlainEnemyCounter + " / " + m_maxEnemyCounter;
    }    
}