using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider m_healthBarSlider = null;

    [SerializeField] Slider m_relicBarSlider = null;
    [SerializeField] Slider m_goalCounterSlider = null;

    public void UpdateGoalBar(int maxEnemies, int slainEnemies)
    {
        
        m_goalCounterSlider.GetComponentInChildren<TextMeshProUGUI>().text = (slainEnemies + " / " +maxEnemies);
        float mediana = (1f/maxEnemies);
        if(slainEnemies != maxEnemies && slainEnemies!=0)
        {
            m_goalCounterSlider.value =+ mediana; 
        }
    }

    public void UpdateHeathBar(float maxHP, float currentHP)
    {
        m_healthBarSlider.GetComponentInChildren<TextMeshProUGUI>().text = (currentHP + " / " + maxHP);
        if(currentHP != maxHP)
        {
            float m_mediana = (1f/maxHP);  
            m_healthBarSlider.value = m_healthBarSlider.value - m_mediana;
        }

        if(currentHP == maxHP)
        {
            m_healthBarSlider.value = 1;
        }
    }

    public void UpdateKillBar(int maxEnemies, int slainEnemies)
    {
        m_goalCounterSlider.GetComponentInChildren<TextMeshProUGUI>().text = (slainEnemies + " / " + maxEnemies);
        float mediana = (1f / maxEnemies);
        if (slainEnemies != maxEnemies && slainEnemies != 0)
        {
            m_goalCounterSlider.value = +mediana;
        }
    }

    public void RelicHealthBar(float maxHPrelic, float currentHPrelic)
    {
        m_relicBarSlider.minValue = 0;
        m_relicBarSlider.maxValue = maxHPrelic;
        m_relicBarSlider.value = currentHPrelic;
        m_relicBarSlider.GetComponentInChildren<TextMeshProUGUI>().text = (currentHPrelic + " / " + maxHPrelic);
        if (currentHPrelic != maxHPrelic)
        {
            m_relicBarSlider.value = currentHPrelic;
        }

        if (currentHPrelic == maxHPrelic)
        {
            m_relicBarSlider.value = maxHPrelic;
        }
    }
}