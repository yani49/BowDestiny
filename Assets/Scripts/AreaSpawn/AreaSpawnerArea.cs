using UnityEngine;

public class AreaSpawnerArea : MonoBehaviour
{
    //[SerializeField] GameObject g_ObjectToSpawn = null;
    [SerializeField] public Enemy_Master g_EnemyToSpawn;
    [SerializeField] GameObject g_areaOfSpawn = null;
    [SerializeField] int m_numberofMonsters;

    [SerializeField] bool isAutotrigger = false;

    private float minx;
    private float maxx;
    bool isAlreadySpawned = false;

    public float m_modifierEnemyHP = 0f;
    public float m_modifierEnemyDMG = 0f;

    private void Start()
    {
        minx = 0 - g_areaOfSpawn.transform.localScale.x;
        maxx = minx * (-1);
        print(minx);
        print(maxx);
        /*
        if (isAutotrigger)
        {
            Destroy(g_areaOfSpawn);
            bla_Spawner(m_numberofMonsters);            
        }*/
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            //bla_Spawner(m_numberofMonsters);
            isAlreadySpawned = true;
            Destroy(g_areaOfSpawn);
            GetComponent<Collider>().enabled = false;
        }
    }

    public void bla_Spawner(int m_maxEnemyCounter , float modEnemyHP, float modEnemyDmg)
    {
        m_modifierEnemyHP += modEnemyHP;
        m_modifierEnemyDMG += modEnemyDmg;
        print("sm dobil sporoèilo");
        for(int spwancounter = 0; spwancounter<m_maxEnemyCounter; spwancounter++)
        {
            print("spawnde");
            float Randomx = Random.Range(minx, maxx);
            float Randomz = Random.Range(minx, maxx);
            
            float spwaner_positionx = g_areaOfSpawn.transform.position.x;
            float spwaner_positionz = g_areaOfSpawn.transform.position.z;

            //Instantiate(g_ObjectToSpawn, new Vector3(spwaner_positionx+(Randomx), 2f, spwaner_positionz+(Randomz)), Quaternion.identity);            
            GameObject SpawnedEnemy = Instantiate(g_EnemyToSpawn.gameObject, new Vector3(spwaner_positionx + (Randomx), 2f, spwaner_positionz + (Randomz)), Quaternion.identity);
            
            SpawnedEnemy.GetComponent<Enemy_Master>().ChangeEnemyStats(m_modifierEnemyHP, m_modifierEnemyDMG);
        }
    }
}