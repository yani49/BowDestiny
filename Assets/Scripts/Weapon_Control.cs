using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon_Control : MonoBehaviour
{
    [SerializeField] private RandomWordGen m_wordGen;
    [SerializeField] private Aimer m_aimer;
    [SerializeField] public Color[] m_arrowsMAT;
    [SerializeField] private GameObject m_weaponPanel;
    [SerializeField] private GameObject m_weaponPanel_toggle;
    

    public int chosenDmgType = 0;

    [SerializeField] private GameObject m_bowHead;
    [SerializeField] private ParticleSystem m_areaParticleSystem;
    [SerializeField] private ParticleSystem m_arrowParticleSystem;

    bool vpisujemGeslo = false;
    bool isShooting = false;

    public bool[] isType;
    public int[] weaponlvl;
    public int[] weaponDmgMultipler;
    public int[] weaponWordTypeLength;
    public float m_slowMotionTimeScale = 0.8f;

    //sound
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] AudioClip[] m_audioClips;

    private void Start()
    {
        m_arrowsMAT[0] = m_bowHead.GetComponent<Renderer>().material.color;
        m_bowHead.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(m_arrowsMAT[0].r, m_arrowsMAT[0].g, m_arrowsMAT[0].b, m_arrowsMAT[0].a));

        var main = m_areaParticleSystem.main;
        main.startColor = new Color(m_arrowsMAT[0].r, m_arrowsMAT[0].g, m_arrowsMAT[0].b);

        for (int i = 1; i < m_arrowsMAT.Length; i++)
        {
            GameObject newWeaponToggle = (GameObject)Instantiate(m_weaponPanel_toggle);
            newWeaponToggle.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = i.ToString();
            newWeaponToggle.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (weaponDmgMultipler[i]*weaponlvl[i]).ToString() + " dmg";

            newWeaponToggle.transform.GetChild(0).GetComponent<Image>().color = new Color(m_arrowsMAT[i].r, m_arrowsMAT[i].g, m_arrowsMAT[i].b);

            newWeaponToggle.SetActive(false);
            newWeaponToggle.transform.SetParent(m_weaponPanel.transform, false);
        }
        UpgradeWeapon();
    }
    void Update()
    {        
        if(Input.GetButtonDown("Weapon 01") && isType[1] && !m_weaponPanel.transform.GetChild(1).GetComponent<Toggle>().isOn)
        {
            chosenDmgType = 1;
            //m_AudioSource.PlayOneShot(m_audioClips[0]);
            //Time.timeScale = m_slowMotionTimeScale;
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(weaponWordTypeLength[chosenDmgType], weaponlvl[chosenDmgType]);
            m_weaponPanel.transform.GetChild(chosenDmgType).GetComponent<Toggle>().isOn = true;
        }

        if (Input.GetButtonDown("Weapon 02") && isType[2] && !m_weaponPanel.transform.GetChild(2).GetComponent<Toggle>().isOn)
        {
            chosenDmgType = 2;
            //m_AudioSource.PlayOneShot(m_audioClips[0]);
            //Time.timeScale = m_slowMotionTimeScale;
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(weaponWordTypeLength[chosenDmgType], weaponlvl[chosenDmgType]);
            m_weaponPanel.transform.GetChild(chosenDmgType).GetComponent<Toggle>().isOn = true;
        }

        if (Input.GetButtonDown("Weapon 03") && isType[3] && !m_weaponPanel.transform.GetChild(3).GetComponent<Toggle>().isOn)
        {
            chosenDmgType = 3;
            m_AudioSource.PlayOneShot(m_audioClips[0]);
            Time.timeScale = m_slowMotionTimeScale - 0.4f;
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(weaponWordTypeLength[chosenDmgType], weaponlvl[chosenDmgType]);
            m_weaponPanel.transform.GetChild(chosenDmgType).GetComponent<Toggle>().isOn = true;
        }

        if (Input.GetButtonDown("Weapon 04") && isType[4] && !m_weaponPanel.transform.GetChild(4).GetComponent<Toggle>().isOn)
        {
            chosenDmgType = 4;
            m_AudioSource.PlayOneShot(m_audioClips[0]);
            Time.timeScale = m_slowMotionTimeScale - 0.5f;
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(weaponWordTypeLength[chosenDmgType], weaponlvl[chosenDmgType]);
            m_weaponPanel.transform.GetChild(chosenDmgType).GetComponent<Toggle>().isOn = true;
        }

        if (Input.anyKey && vpisujemGeslo)
        {
            if(Input.inputString == "a" || Input.inputString == "d" || Input.inputString == "w" || Input.inputString == "s")
            {
                print("Tola je narobe");
            }
            else
            {
                string keyPressed = Input.inputString;
                if (keyPressed.Length == 1)
                {
                    m_wordGen.EnterLetter(keyPressed);
                    if (m_wordGen.IsWordComplete())
                    {
                        //m_bowHead.GetComponent<Renderer>().material.color = m_arrowsMAT[chosenDmgType];


                        //Color32 convertBlabla = new Color(m_arrowsMAT[chosenDmgType].r, m_arrowsMAT[chosenDmgType].g, m_arrowsMAT[chosenDmgType].b, m_arrowsMAT[chosenDmgType].a);
                        m_bowHead.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(m_arrowsMAT[chosenDmgType].r, m_arrowsMAT[chosenDmgType].g, m_arrowsMAT[chosenDmgType].b, m_arrowsMAT[chosenDmgType].a));

                        var main = m_areaParticleSystem.main;
                        main.startColor = new Color(m_arrowsMAT[chosenDmgType].r, m_arrowsMAT[chosenDmgType].g, m_arrowsMAT[chosenDmgType].b);

                        Debug.Log("pravilno si vtipkal!");
                        vpisujemGeslo = false;
                        isShooting = true;
                        m_weaponPanel.transform.GetChild(chosenDmgType).GetComponent<Toggle>().isOn = false;

                        m_slowMotionTimeScale = 1f;
                        Time.timeScale = m_slowMotionTimeScale;
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(0) && isShooting)
        {
            m_wordGen.gameObject.SetActive(false);
            //m_bowHead.GetComponent<Renderer>().material.color = m_arrowsMAT[0];

            //Color32 convertBlabla = new Color(m_arrowsMAT[0].r, m_arrowsMAT[0].g, m_arrowsMAT[0].b, m_arrowsMAT[0].a);
            m_bowHead.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(m_arrowsMAT[0].r, m_arrowsMAT[0].g, m_arrowsMAT[0].b, m_arrowsMAT[0].a));


            var main = m_areaParticleSystem.main;
            main.startColor = new Color(m_arrowsMAT[0].r, m_arrowsMAT[0].g, m_arrowsMAT[0].b);
            //m_aimer.isShooting = true;
            m_aimer.ShootTarget();
            isShooting = false;

            if (chosenDmgType == 3 || chosenDmgType == 4 || chosenDmgType == 5)
            {
                m_AudioSource.PlayOneShot(m_audioClips[1]);
            }
            
        }
    }
    public void UpgradeWeapon()
    {
        for(int i = 0;i<weaponlvl.Length;i++)
        {
            if(weaponlvl[i] > 0)
            {
                isType[i] = true;
                m_weaponPanel.gameObject.transform.GetChild(i).gameObject.SetActive(true);
                m_weaponPanel.gameObject.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = (weaponDmgMultipler[i] * weaponlvl[i]).ToString() +" dmg";

                //transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = i.ToString();
            }
        }
    }
}