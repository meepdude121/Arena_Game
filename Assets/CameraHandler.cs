using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    GameObject PlayerObject;
    public bool FollowPlayer = true;
    void Start()
    {
        PlayerObject = Player.instance.gameObject;
    }

    void LateUpdate()
    {
        if (FollowPlayer)
        {
            // Get current camera position
            Vector3 oldPos = transform.position;
            // Get target camera position
            Vector3 newPos = PlayerObject.transform.position;
            // set target camera position's z (depth) value = old depth value
            newPos.z = oldPos.z;
            // apply position change
            transform.position = newPos;
        }
    }
}
