using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    GameObject PlayerObject;
    public bool FollowPlayer = true;
    private Camera CameraComponent;
    void Start()
    {
        PlayerObject = Player.instance.gameObject;
        CameraComponent = Camera.main;
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
    public void StartPlayerDeathAnimation() {
        StartCoroutine(PlayerDeathAnimation());
    }
    private IEnumerator PlayerDeathAnimation() {

        Debug.Log("Start death animation");
        // variable declaration
        float CameraLerpTime = 0f;

        // run every frame
        while (true) {
            while (CameraLerpTime < 3f) {
                Debug.Log("Camera lerp time: " + CameraLerpTime);
                CameraLerpTime += Time.deltaTime;
                // change camera render size
                CameraComponent.orthographicSize = Mathf.Lerp(CameraComponent.orthographicSize, 3f, CameraLerpTime / 3f);

                // slow down time
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0.3f, CameraLerpTime / 3f);
                yield return null;
            }

            yield return null;
        }
    }
}
