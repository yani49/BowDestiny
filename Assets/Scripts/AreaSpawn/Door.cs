using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Dictionary<string,bool> WhichKey = new Dictionary<string,bool>();
    [SerializeField] Animator m_animatorController;

    [SerializeField] bool isEnemyRoomDoor = false;

    /*
    private void Start()
    {
      
     WhichKey.Add("This is the name of the first boolean", false);
     WhichKey.Add("This is the name of the second boolean", true);
    }
    */
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" && !isEnemyRoomDoor)  
        {
            print("heelo");
            m_animatorController.SetBool("IsOpen",true);
        }

        if(other.tag== "Enemy" && isEnemyRoomDoor)
        {
            print("They open the room");
            m_animatorController.SetBool("IsOpen", true);
        }
    }
}