using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject Darkness;
    public bool Explored;
    public GameObject[] Enemies;

    

    public void RoomEnter()
    {
        if (!Explored)
        {
            Darkness.SetActive(false);
            Explored = true;
            if (Enemies != null)
            {
                foreach (GameObject Enemy in Enemies)
                {
                    Enemy.GetComponent<Entity>().AIActive = true;
                }
            }
        }
    }
}
