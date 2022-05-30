using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public int w_maxTypeReward = 3;
    private int w_maxTypeRewardBuffer = 0; //doloci kateri tip rewarda je

    public float w_maxHpPlusAmount = 10f;
    private float w_maxHpPlusBuffer = 0f; //doloci katera je naslednja stopnja

    public int numberofUpgrade=1;

    [SerializeField] PlayerStats m_playerStats;
    [SerializeField] Weapon_Control m_weaponControlStats;

    
    [SerializeField] GameObject m_UpgradeToggleTemplate;
    [SerializeField] ToggleGroup m_upgradeUIToogleGroup;
    [SerializeField] Upgrade_Toggle[] AllUpgradeToggles;

    public void RandomReward()
    {
        if(m_upgradeUIToogleGroup.transform.childCount >1)
        {
            for (int opa3 = 1; opa3 < m_upgradeUIToogleGroup.transform.childCount; opa3++)
            {
                Destroy(m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).gameObject);
            }
        }
        for (int opa2 = 0; opa2 < numberofUpgrade; opa2++)
        {
            GameObject objecttemplate = Instantiate(m_UpgradeToggleTemplate);
            objecttemplate.gameObject.transform.parent = m_upgradeUIToogleGroup.gameObject.transform;
            objecttemplate.SetActive(true);

            w_maxTypeRewardBuffer = Random.Range(0, w_maxTypeReward);
            int w_ChoosedWeapon = Random.Range(1, m_weaponControlStats.weaponlvl.Length);

            objecttemplate.transform.GetComponent<Upgrade_Toggle>().WeaponToggleOrganizer(w_ChoosedWeapon);
        }
    }
    public void GetReward()
    {
        AllUpgradeToggles = FindObjectsOfType<Upgrade_Toggle>();
        for(int indexWeaponUpgrade = 0; indexWeaponUpgrade<AllUpgradeToggles.Length;indexWeaponUpgrade++)
        {
            if (AllUpgradeToggles[indexWeaponUpgrade].GetComponent<Toggle>().isOn)
            {
                AllUpgradeToggles[indexWeaponUpgrade].UpdateWeaponStats();
            }
        }
    }
    //for now useless 
    /*
    public void OnLevelUp()
    {
        for (int opa3 = 0; opa3 < m_upgradeUIToogleGroup.gameObject.transform.childCount; opa3++)
        {
            //m_allUpgradeToggles[opal].ToggleOrganizer(1, 1);
            
            //for weapon/damage  upgrade
            if (m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Toggle>().isOn && m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Upgrade_Toggle>().m_UpgradeType == 0)
            {
                m_weaponControlStats.AttackDmg += m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Upgrade_Toggle>().m_UpgradeAmount;
                print("<color=blue>NoviDamage: </color>" + m_weaponControlStats.AttackDmg);
                m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Toggle>().isOn = false;
            }
            
            //for hp
            if (m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Toggle>().isOn && m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Upgrade_Toggle>().m_UpgradeType == 1)
            {
                print(m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Upgrade_Toggle>().m_UpgradeAmount);
                //m_playerStats.LevelUpHPPlayer(m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Upgrade_Toggle>().m_UpgradeAmount, true);
                m_upgradeUIToogleGroup.gameObject.transform.GetChild(opa3).GetComponent<Toggle>().isOn = false;
            }
        }

        GetComponent<GoalManager>().ContinueGame();
    }*/
}