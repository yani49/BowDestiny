using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_Toggle : MonoBehaviour
{
    [SerializeField] Weapon_Control m_WeaponControl;
    public int m_UpgradeType = 0;

    public int m_weaponType = 1;
    public int m_currentLevel = 0;
    public int m_CurrentLevelDMG = 0;
    public int m_UpgradeLevel = 1;
    public int m_UpgradeLevelDMG = 0;

    public TextMeshProUGUI m_Categorytext = null;
    public TextMeshProUGUI m_Amounttext = null;
    public TextMeshProUGUI m_Upgradedtext = null;

    //weapon type
    public void WeaponToggleOrganizer(int TypeOfWeapon)
    {
        m_weaponType = TypeOfWeapon;
        m_currentLevel = m_WeaponControl.weaponlvl[m_weaponType];
        m_UpgradeLevel = m_currentLevel+1;

        /*
        m_CurrentLevelDMG = m_WeaponControl.weaponDmgMultipler[m_weaponType];
        m_UpgradeLevelDMG = m_WeaponControl.weaponDmgMultipler[m_weaponType];
        */

        gameObject.GetComponent<Toggle>().targetGraphic.color = Color.red;
        m_Categorytext.text = "Category: Basic Damage";
        m_Amounttext.text = "Old Amount: " + m_currentLevel;
        m_Upgradedtext.text = "New Amount: " + m_UpgradeLevel;
    }

    public void UpdateWeaponStats()
    {
        m_WeaponControl.weaponlvl[m_weaponType] = m_UpgradeLevel;
    }

    // personal health type


}