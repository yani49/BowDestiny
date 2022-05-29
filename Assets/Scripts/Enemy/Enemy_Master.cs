using UnityEngine;
using UnityEngine.AI;

public class Enemy_Master : MonoBehaviour
{
    //managers
    [SerializeField] private GoalManager m_goalManager = null;
    [SerializeField] private RelicDefender m_relicDef = null;
    [SerializeField] AudioSource m_audioEffects = null;

    public NavMeshAgent m_agent;
    public Animator m_animator;

    public Rigidbody m_rigidbody = null;
    public GameObject m_player;
    public GameObject m_relic;

    [SerializeField] bool isPlayerMainTarget;
    
    //Stasts
    public float m_EnemyHP = 2.0f;
    public float m_DistanceToPlayer;
    public float m_DistanceToRelic;
    public float m_GuardArea = 5f;
    [SerializeField] public float m_EnemySpeed = 3.5f;
    public bool isAngered;
    public bool isHit;
    [SerializeField] float HittedCooldown;

    private GameObject attackedObject = null;
    
    public float m_basicDamage = 1;
    [SerializeField] private int vunrableType = 3;

    //bools
    [SerializeField] public bool isDead = false;
    [SerializeField] bool isRelicGame = false;

    //AudioC
    public AudioClip[] m_EnemyAudioClips;

    //beautyShots
    [SerializeField] private ParticleSystem m_DeathParticleSystem;
    [SerializeField] private Material[] m_material;


    private void Start() 
    {
        m_material = FindObjectsOfType<Material>();
        m_goalManager = FindObjectOfType<GoalManager>();
        m_relicDef = FindObjectOfType<RelicDefender>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_agent.speed = m_EnemySpeed;
        //m_agent.speed = 3f;
    }

    void Update()
    {
        if(!isDead)
        {
            m_DistanceToPlayer = Vector3.Distance(transform.position, m_player.transform.position);

            m_DistanceToRelic = Vector3.Distance(transform.position, m_relic.transform.position);

            if (m_DistanceToPlayer<= m_GuardArea ||isPlayerMainTarget)
            {
                isAngered = true;
                //m_DistanceToPlayer = Vector3.Distance(transform.position, m_player.transform.position);

                m_agent.isStopped = false;
                m_agent.SetDestination(m_player.transform.position);
                attackedObject = m_player;
            }

            if(m_DistanceToPlayer >= m_GuardArea && !isPlayerMainTarget)
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
            if(m_DistanceToPlayer<3.0f || m_DistanceToRelic<3.0f)
            {
                m_agent.isStopped = true;
                m_animator.SetTrigger("Attack");
            }
            */
            if (m_DistanceToPlayer <= 4.0f)
            {
                m_agent.isStopped = true;
                m_animator.SetTrigger("Attack");
            }
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
            else if(m_DistanceToRelic <= 3.0f || !isPlayerMainTarget)
            {
                print("hello");
                //m_relicDef.GetComponent<RelicDefender>().AttackingRelic(m_basicDamage);
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
        m_EnemyHP = m_EnemyHP - damage;

        int m_AudioRandomClip = Random.RandomRange(0, m_EnemyAudioClips.Length);
        m_audioEffects.PlayOneShot(m_EnemyAudioClips[m_AudioRandomClip]);
        print("You hit me with: " + damage);
        
        if (!isDead && m_EnemyHP <= 0 )
        {
            m_DeathParticleSystem.Play();
            isDead = true;
            gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
            //m_animator.Play("Death");
            m_animator.SetTrigger("Death");
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