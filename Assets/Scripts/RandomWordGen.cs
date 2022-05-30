using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomWordGen : MonoBehaviour
{
    [SerializeField] private GameObject m_CharTemplate;
    [SerializeField] private PlayerStats m_pStats;
    //[SerializeField] private string m_vseCrke;
    [SerializeField] private string[] m_vseCrkes;
    [SerializeField] private int m_dolzinaBesede;

    List<GameObject>  m_currentCharTemplates = new List<GameObject>();

    string m_Beseda = "loremIpsum";

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void RandomBesedaMetoda(int dolzinabesede , int weaponLevel) 
    {
        print("i got" + weaponLevel + "reading table "+ m_vseCrkes[weaponLevel]);
        //Time.timeScale = 0.3f;

        if (m_currentCharTemplates.Count>1)
        {
            foreach(GameObject i in m_currentCharTemplates)
            {
                Destroy(i);
            }
            m_currentCharTemplates.Clear();
        }

        m_dolzinaBesede = dolzinabesede;
        m_Beseda = "";
        for(int i = 0; i<m_dolzinaBesede; i++)
        {
            int randomnumbers = Random.Range(0, m_vseCrkes[weaponLevel].Length);
            m_Beseda = m_Beseda.ToString() + m_vseCrkes[weaponLevel][randomnumbers];
            
            GameObject instanceM_charTemplate = Instantiate(m_CharTemplate);
            instanceM_charTemplate.GetComponentInChildren<TextMeshProUGUI>().text = m_vseCrkes[weaponLevel][randomnumbers].ToString();          
            instanceM_charTemplate.transform.SetParent(transform.GetChild(0).transform,false);
            m_currentCharTemplates.Add(instanceM_charTemplate);

        }
    }

    public void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            //m_animator.SetTrigger("Correct");
            RemoveLetter();
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return m_Beseda.IndexOf(letter) == 0;

    }

    private void RemoveLetter()
    {
        Destroy(m_currentCharTemplates[0], 0.1f);
        
        string newString = m_Beseda.Remove(0, 1);
        m_currentCharTemplates.RemoveAt(0);
        SetRemainingWord(newString);
    }

    private void SetRemainingWord(string newString)
    {
        m_Beseda = newString;
    }

    public bool IsWordComplete()
    {
        //Time.timeScale = 1.0f;
        return m_Beseda.Length == 0;
    }
}