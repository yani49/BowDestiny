using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float m_hpPlayer = 2;
    [SerializeField] private GoalManager m_scenemanager = null;

    public void WoundChar(float wound)
    {
        m_hpPlayer = m_hpPlayer - wound;
        if(m_hpPlayer <= 0)
        {
            m_scenemanager.ShowPanels(0);
        }
    }
}