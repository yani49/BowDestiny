using UnityEngine;
public class Aimer : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private Weapon_Control m_WeaponControl;
    [SerializeField] private FaceCamera[] m_allEnemies = null;
    [SerializeField] private AudioSource m_audioSource;

    public AudioClip m_AudioClips;

    public bool isShooting = false;
    public bool isMultishot = false;

    private void Update()
    {        
        /*
        if(Input.GetMouseButtonDown(1))
        {
            UpdateEnemyList();   
            foreach(FaceCamera i in m_allEnemies)
            {
                i.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }         
        }

        if(Input.GetMouseButtonUp(1))
        { 
            foreach(FaceCamera i in m_allEnemies)
            {
                i.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }         
        }*/
    }

    public void ShootTarget()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = (m_mainCamera.ScreenPointToRay(screenCenterPoint));

        print("helllo 01");

        RaycastHit[] shootRays = Physics.RaycastAll(ray, 1000f);

        foreach (RaycastHit shootRay in shootRays)
        {
            if (shootRay.collider.gameObject.CompareTag("Enemy"))
            {
                int callulatedDMG = m_WeaponControl.weaponDmgMultipler[m_WeaponControl.chosenDmgType] * m_WeaponControl.weaponlvl[m_WeaponControl.chosenDmgType];
                shootRay.collider.gameObject.GetComponent<Enemy_Master>().WoundChar(m_WeaponControl.weaponDmgMultipler[m_WeaponControl.chosenDmgType], m_WeaponControl.chosenDmgType);

                shootRay.collider.gameObject.GetComponent<Enemy_Master>().m_player = this.gameObject;
                shootRay.collider.gameObject.GetComponent<Enemy_Master>().isAngered = true;

                Vector3 force = 250f * transform.forward;
                
                //shootRay.collider.gameObject.GetComponent<Enemy_Master>().m_rigidbody.AddForce(force);
            }
            else if(!shootRay.collider.gameObject.CompareTag("Env"))
            {
                m_audioSource.PlayOneShot(m_AudioClips);
            }
        }
        isShooting = false;
    }

    private void UpdateEnemyList()
    {
        m_allEnemies = FindObjectsOfType<FaceCamera>();
    }
}