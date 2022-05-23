using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController m_controller;

    public float m_speed = 12f;
    public float m_gravity = -9.81f;
    public float m_jumpHeight = 2f;

    public Transform m_groundCheck;
    public float m_groundDistance = 0.4f;
    public LayerMask m_groundMask;

    Vector3 m_velocity;
    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

        if(isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        m_controller.Move(move * m_speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight* -2f * m_gravity);
        }
        
        m_velocity.y += m_gravity * Time.deltaTime;

        m_controller.Move(m_velocity * Time.deltaTime);
    }
}