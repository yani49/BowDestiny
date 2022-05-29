using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float m_MaxhpPlayer = 2;
    [SerializeField] private float m_CurrenthpPlayer = 0;
    [SerializeField] public int m_level = 1;
    [SerializeField] private GoalManager m_scenemanager = null;
    [SerializeField] private RelicDefender m_RelicDef = null;

    [SerializeField] private UIManager m_uiManager = null;
    

    //Audio
    [SerializeField] private AudioSource m_playerAS = null;
    public AudioClip[] m_playerAC;

    private void Awake() 
    {        
        m_CurrenthpPlayer = m_MaxhpPlayer;
        m_uiManager.UpdateHeathBar(m_MaxhpPlayer,m_CurrenthpPlayer);
        //m_uiManager.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "lvl: " + m_level;
    }

    public void WoundPlayer(float wound)
    {
        if(m_CurrenthpPlayer != 0)
        {
            m_CurrenthpPlayer = m_CurrenthpPlayer - wound;
            m_uiManager.UpdateHeathBar(m_MaxhpPlayer, m_CurrenthpPlayer);
            m_playerAS.PlayOneShot(m_playerAC[0]);
            if (m_CurrenthpPlayer <= 0)
            {
                m_RelicDef.ShowPanels(2);

                /*
                if (m_scenemanager != null)
                {
                    m_scenemanager.ShowPanels(0);
                }
                else if (m_scenemanager == null)
                {
                    m_RelicDef.ShowPanels(1);
                }*/
            }
        }
    }

    public void LevelUpHPPlayer(float amount, bool allHPrestore , bool levelup)
    {
        if(levelup)
        {
            m_level += 1;
            m_MaxhpPlayer = m_MaxhpPlayer + amount;

            m_CurrenthpPlayer = m_MaxhpPlayer;
            m_uiManager.UpdateHeathBar(m_MaxhpPlayer, m_CurrenthpPlayer);
            
            //m_uiManager.transform.GetChild(2).GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Wave: " + m_level;
            //m_uiManager.transform.GetChild(2).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "lvl: " + m_level;
        }
        else if(!levelup)
        {
            if (allHPrestore == true)
            {
                m_MaxhpPlayer = m_MaxhpPlayer + amount;

                m_CurrenthpPlayer = m_MaxhpPlayer;
                m_uiManager.UpdateHeathBar(m_MaxhpPlayer, m_CurrenthpPlayer);
            }
            if (m_CurrenthpPlayer < m_MaxhpPlayer)
            {
                m_CurrenthpPlayer = m_MaxhpPlayer;
                m_uiManager.UpdateHeathBar(m_MaxhpPlayer, m_CurrenthpPlayer);

            }
        }
    }

    public void ResetPlayerHP()
    {
        m_CurrenthpPlayer = m_MaxhpPlayer;
        m_uiManager.UpdateHeathBar(m_MaxhpPlayer, m_CurrenthpPlayer);
    }
}