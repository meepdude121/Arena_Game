using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room[] rooms;
	private void Awake()
	{
		GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Room");
		rooms = new Room[GameObjects.Length];
		for (int i = 0; i < GameObjects.Length; i++)
		{
			rooms[i] = GameObjects[i].GetComponent<Room>();
		}
	}

	public void UpdateRooms(bool Locked)
	{
		foreach (Room room in rooms)
		{
			room.Locked = Locked;
		}
	}
}
