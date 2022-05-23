using TMPro;
using UnityEngine;

public class RandomWordGen : MonoBehaviour
{
    [SerializeField] private string m_vseCrke;
    [SerializeField] private int m_dolzinaBesede;
    [SerializeField] private TextMeshProUGUI textTM;

    private string m_Beseda = "kurvamat";

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void RandomBesedaMetoda(int dolzinabesede) 
    {
        m_dolzinaBesede = dolzinabesede;
        m_Beseda = "";
        for(int i = 0; i<m_dolzinaBesede; i++)
        {
            int randomnumber = Random.Range(0,m_vseCrke.Length-1);
            m_Beseda = m_Beseda.ToString() + m_vseCrke[randomnumber];
        }
        textTM.text = m_Beseda;
    }

    public void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return m_Beseda.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = m_Beseda.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private void SetRemainingWord(string newString)
    {
        m_Beseda = newString;
        textTM.text = m_Beseda;
    }

    public bool IsWordComplete()
    {
        return m_Beseda.Length == 0;
    }
}