using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCursor : MonoBehaviour
{
	void Update()
	{
		//Vector3 mousePos = Input.mousePosition;
		Vector3 mousePos = Mouse.current.position.ReadValue();
		mousePos.z = 5.23f;
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos -= objectPos;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg));
	}
}
