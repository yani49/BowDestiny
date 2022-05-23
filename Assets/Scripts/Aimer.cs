using UnityEngine;
public class Aimer : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;

    public bool isShooting = false;

    private void Update()
    {
        if(isShooting)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = (m_mainCamera.ScreenPointToRay(screenCenterPoint));

            print("helllo 01");
            //phyiscs raycast ne�e delat...
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f))
            {
                print("helllo 01.5");
                if (raycastHit.collider.gameObject.CompareTag("Enemy"))
                {
                    raycastHit.collider.gameObject.GetComponent<Enemy_Master>().WoundChar(1f);
                    print("helllo 02");
                    raycastHit.collider.gameObject.GetComponent<Enemy_Master>().m_player = this.gameObject;
                    raycastHit.collider.gameObject.GetComponent<Enemy_Master>().isAngered = true;
                }
            }
            isShooting = false;
        }
    }
}