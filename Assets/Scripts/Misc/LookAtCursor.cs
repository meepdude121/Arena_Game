using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCursor : MonoBehaviour
{
    GameObject child;
    SpriteRenderer sr;
    private void Awake()
    {
        OnChangeWeapon();
    }
    private void OnChangeWeapon()
    {
        child = transform.GetChild(0).gameObject;
        sr = child.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = 5.23f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos -= objectPos;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg));

        // flip sprite if z rotation of parent is > 90 or < -90
        float zAngle = transform.rotation.eulerAngles.z;
        // force angle to be positive
        if (zAngle < 0) zAngle += 180;
        // if zAngle is within 90 and 270
        if (zAngle > 90 && zAngle < 270)
        {
            sr.flipY = true;
        } else
        {
            sr.flipY = false;
        }
    }
}
