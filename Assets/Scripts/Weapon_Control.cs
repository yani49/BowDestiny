using UnityEngine;

public class Weapon_Control : MonoBehaviour
{
    [SerializeField] private RandomWordGen m_wordGen;
    [SerializeField] private Aimer m_aimer;
    [SerializeField] private Color[] m_arrowsMAT;
    int chosenColor = 0;

    //tole bojo kasneje particle system in ne samo menjava render colorja
    [SerializeField] private GameObject m_bowHead;

    bool vpisujemGeslo = false;
    bool isShooting = false;

    private void Start()
    {
        m_arrowsMAT[0] = m_bowHead.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if(Input.GetButtonDown("Weapon 01"))
        {
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(2);
            chosenColor = 1;
        }

        if (Input.GetButtonDown("Weapon 02"))
        {
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(3);
            chosenColor = 2;
        }

        if (Input.GetButtonDown("Weapon 03"))
        {
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(4);
            chosenColor = 3;
        }

        if (Input.GetButtonDown("Weapon 04"))
        {
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(5);
            chosenColor = 4;
        }

        if (Input.GetButtonDown("Weapon 05"))
        {
            vpisujemGeslo = true;
            m_wordGen.gameObject.SetActive(true);
            m_wordGen.RandomBesedaMetoda(6);
            chosenColor = 5;
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
                        m_bowHead.GetComponent<Renderer>().material.color = m_arrowsMAT[chosenColor];
                        Debug.Log("pravilno si vtipkal!");
                        vpisujemGeslo = false;
                        isShooting = true;
                        
                    }
                }
            }
        }

        if(Input.GetMouseButtonDown(0) && isShooting)
        {
            m_wordGen.gameObject.SetActive(false);
            m_bowHead.GetComponent<Renderer>().material.color = m_arrowsMAT[0];
            m_aimer.isShooting = true;
            isShooting = false;
        }
    }
}