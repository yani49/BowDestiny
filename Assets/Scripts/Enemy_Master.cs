using UnityEngine;

public class Enemy_Master : MonoBehaviour
{
    public GameObject m_player;

    public float m_EnemyHP = 2.0f;

    public float m_Distance;
    public float m_GuardArea = 5f;
    public bool isAngered;
    public UnityEngine.AI.NavMeshAgent m_agent;

    public Animator m_animator;

    private float m_basicDamage = 1;

    bool isDead = false;

    void Update()
    {
        if(!isDead)
        {
            m_Distance = Vector3.Distance(transform.position, m_player.transform.position);
            /*
            if(m_Distance<= m_GuardArea)
            {
                isAngered = true;
            }

            if(m_Distance >= m_GuardArea)
            {
                isAngered = false;
            }*/

            if(isAngered)
            {
                m_agent.isStopped = false;
                m_agent.SetDestination(m_player.transform.position);
            }
            if(!isAngered)
            {
                m_agent.isStopped = true;
            }
            if(isAngered && m_Distance<2.0f)
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
        m_agent.isStopped = true;
        m_player.GetComponent<PlayerStats>().WoundChar(m_basicDamage);
    }

    public void WoundChar(float damage)
    {
        m_EnemyHP = m_EnemyHP - damage;
        if(m_EnemyHP <= 0 )
        {
            isDead = true;
            m_animator.Play("Death");
            m_Distance = 0;
            m_agent.isStopped = true;
        }
    }
}