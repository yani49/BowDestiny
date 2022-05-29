using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_Toggle : MonoBehaviour
{
    [SerializeField] Weapon_Control m_WeaponControl;
    public int m_UpgradeType = 0;

    public int m_weaponType = 1;
    public int m_currentLevel = 0;
    //public int m_CurrentLevelDMG = 0;
    public int m_UpgradeLevel = 1;
    //public int m_UpgradeLevelDMG = 0;

    public TextMeshProUGUI m_Categorytext = null;
    public TextMeshProUGUI m_Amounttext = null;
    public TextMeshProUGUI m_Upgradedtext = null;
    private void Start()
    {
        m_WeaponControl = FindObjectOfType<Weapon_Control>();
    }

    //weapon type
    public void WeaponToggleOrganizer(int TypeOfWeapon)
    {
        m_weaponType = TypeOfWeapon;
        m_currentLevel = m_WeaponControl.weaponlvl[m_weaponType];
        m_UpgradeLevel = m_currentLevel+1;

        gameObject.GetComponent<Toggle>().targetGraphic.color = m_WeaponControl.m_arrowsMAT[m_weaponType];

        if(m_currentLevel == 0)
        {
            m_Upgradedtext.text = "Damage increase:  " + 0 + "->" + (m_WeaponControl.weaponDmgMultipler[m_weaponType] + 0);
        }
        else
        {
            m_Upgradedtext.text = "Damage increase:  " + m_WeaponControl.weaponDmgMultipler[m_weaponType] + "->" + (m_WeaponControl.weaponDmgMultipler[m_weaponType] * m_UpgradeLevel);
        }
        m_Categorytext.text = "Button To press: " + m_weaponType;
        m_Amounttext.text = "level increase: " + m_currentLevel +"->" +m_UpgradeLevel;
        
    }

    public void UpdateWeaponStats()
    {
        m_WeaponControl.weaponlvl[m_weaponType] = m_UpgradeLevel;
    }
}