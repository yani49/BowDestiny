 using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    [Range (0,4)]
    public int RotateAtY;

    public bool Wall_N_bool;
    public bool Wall_S_bool;
    public bool Wall_E_bool;
    public bool Wall_W_bool;

    public GameObject Wall_N;
    public GameObject Wall_S;
    public GameObject Wall_E;
    public GameObject Wall_W;

    public void OnGUI()
    {
        transform.rotation = Quaternion.Euler(0, RotateAtY * 90, 0);

        if (Wall_N_bool)
        {
            Wall_N.gameObject.SetActive(true);
            //transform.rotation = Random.rotation;
        }

        if (!Wall_N_bool)
        {
            Wall_N.gameObject.SetActive(false);
        }

        if (Wall_S_bool)
        {
            Wall_S.gameObject.SetActive(true);
        }
        if (!Wall_S_bool)
        {
            Wall_S.gameObject.SetActive(false);
        }

        if (Wall_E_bool)
        {
            Wall_E.gameObject.SetActive(true);
        }
        if (!Wall_E_bool)
        {
            Wall_E.gameObject.SetActive(false);
        }

        if (Wall_W_bool)
        {
            Wall_W.gameObject.SetActive(true);
        }
        if (!Wall_W_bool)
        {
            Wall_W.gameObject.SetActive(false);
        }
    }
}