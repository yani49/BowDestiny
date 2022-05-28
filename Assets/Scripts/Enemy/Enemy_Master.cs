using UnityEngine;
using UnityEngine.AI;

public class Enemy_Master : MonoBehaviour
{
    [SerializeField] private GoalManager m_goalManager = null;
    [SerializeField] private RelicDefender m_relicDef = null;

    public Rigidbody m_rigidbody = null;
    public GameObject m_player;
    public GameObject m_relic;
    
    public float m_EnemyHP = 2.0f;
    public float m_DistanceToPlayer;
    public float m_DistanceToRelic;
    public float m_GuardArea = 5f;
    public bool isAngered;
    public bool isHit;
    [SerializeField] float HittedCooldown;

    private GameObject attackedObject = null;
    
    public NavMeshAgent m_agent;
    public Animator m_animator;
    private float m_basicDamage = 1;

    [SerializeField] public bool isDead = false;
    [SerializeField] AudioSource m_audioEffects = null;

    [SerializeField] private int vunrableType = 3;
    [SerializeField] bool isRelicGame = false;

    private void Start() 
    {
        m_goalManager = FindObjectOfType<GoalManager>();
        m_relicDef = FindObjectOfType<RelicDefender>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    //preveri zakaj ne delajo niè skode sedaj 

    void Update()
    {
        if(!isDead)
        {
            m_DistanceToPlayer = Vector3.Distance(transform.position, m_player.transform.position);

            m_DistanceToRelic = Vector3.Distance(transform.position, m_relic.transform.position);

            if (m_DistanceToPlayer<= m_GuardArea)
            {
                isAngered = true;
                //m_DistanceToPlayer = Vector3.Distance(transform.position, m_player.transform.position);

                m_agent.isStopped = false;
                m_agent.SetDestination(m_player.transform.position);
                attackedObject = m_player;
            }

            if(m_DistanceToPlayer >= m_GuardArea)
            {
                if(isRelicGame)
                {
                    isAngered = true;
                    m_agent.SetDestination(m_relic.transform.position);
                    attackedObject = m_relicDef.gameObject;
                }
                if(!isRelicGame)
                {
                    isAngered = false;
                    m_agent.isStopped = true;
                    //attackedObject = m_player;
                }
                
            }
            /*
            if(!isAngered)
            {
                m_agent.isStopped = true;
            }*/
            if(m_DistanceToPlayer<3.0f || m_DistanceToRelic<3.0f)
            {
                m_agent.isStopped = true;
                m_animator.SetTrigger("Attack");
            }

            /*
            if(isAngered && m_DistanceToRelic<5.0f)
            {
                m_agent.isStopped = true;
                m_animator.SetTrigger("Attack");
            }
            */

            /*
            if(isHit)
            {   
                //float timer = c
                m_agent.isStopped = false;
                m_agent.SetDestination(m_player.transform.position);
                isAngered = true;
            }*/
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, m_GuardArea);
    }

    private void Animation()    
    {        
        //m_agent.isStopped = true;
        if(isRelicGame)
        {            
            if(m_DistanceToPlayer <= 3.0f)
            {
                m_player.GetComponent<PlayerStats>().WoundPlayer(m_basicDamage);
            }
            else if(m_DistanceToRelic <= 3.0f)
            {
                print("hello");
                m_relicDef.GetComponent<RelicDefender>().AttackingRelic(m_basicDamage);
            }
        }
        else if(!isRelicGame)
        {
            m_player.GetComponent<PlayerStats>().WoundPlayer(m_basicDamage);
            //attackedObject.GetComponent<PlayerStats>().WoundPlayer(m_basicDamage);
        }
        
    }

    public void WoundChar(float damage ,int DmgType)
    {
        /* //class damage type
        if(DmgType == vunrableType)
        {
            m_EnemyHP = m_EnemyHP - damage;
        }
        else
        {
            m_EnemyHP = m_EnemyHP - (damage / 2);
        }*/
        m_EnemyHP = m_EnemyHP - damage;
        m_audioEffects.PlayOneShot(m_audioEffects.clip);
        print("You hit me with: " + damage);
        
        if (!isDead && m_EnemyHP <= 0 )
        {
            isDead = true;
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            m_animator.Play("Death");
            m_DistanceToPlayer = 0;
            m_agent.isStopped = true;
            if(isRelicGame)
            {
                m_relicDef.CheckEnemiesCount();
            }
            else if(!isRelicGame)
            {
                m_goalManager.CheckEngGoal();
            }

            
            Destroy(m_rigidbody);
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    public void DeleteObject()
    {
        Destroy(this.gameObject);
    }

    public void ChangeEnemyStats(float f_enemyHPMod, float f_enemyDmgMod)
    {
        m_EnemyHP += f_enemyHPMod;
        m_basicDamage += f_enemyDmgMod;
    }
}