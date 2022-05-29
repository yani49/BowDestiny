using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float speedMod = 10.0f;//a speed modifier
    [SerializeField] private MeshRenderer[] m_SpawnAreaRender;

    public bool isRotating;
    public bool isshowSpawnAreas;

    private void Awake()
    {
        speedMod = 0.3f;
    }

    public void WhenRotating()
    {
        isRotating = !isRotating;
    }

    void Update()
    {
        transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        if (isRotating)
        {
            //speedMod = 0.8f;
            foreach (MeshRenderer fe_spawnArea in m_SpawnAreaRender)
            {
                fe_spawnArea.enabled= true;
                //fe_spawnArea.gameObject.active = true;
            }
        }
        if(!isRotating)
        {

            //speedMod = 0.3f;
            foreach (MeshRenderer fe_spawnArea in m_SpawnAreaRender)
            {
                fe_spawnArea.enabled = false;
                //fe_spawnArea.gameObject.active = false;
            }
        }        
    }
}
