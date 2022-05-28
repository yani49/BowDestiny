using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera m_mainCamera = null;

    void Update()
    {
        if(m_mainCamera == null)
        {
            m_mainCamera = FindObjectOfType<Camera>();
        }
        transform.LookAt(transform.position + m_mainCamera.transform.rotation * Vector3.forward, m_mainCamera.transform.rotation * Vector3.up);
    }
}