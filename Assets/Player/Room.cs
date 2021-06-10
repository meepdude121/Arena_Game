using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] DarknessObjects;
    public bool Explored;
    public GameObject[] Enemies;

    public GameObject player;
    private Player playerComponent;

	private void Awake()
	{
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player>();
	}

	public void RoomEnter()
    {
        if (!Explored)
        {
            playerComponent.transitionPosition = new Vector3(transform.position.x, transform.position.y, -1f);
            playerComponent.InTransition = true;
            foreach (GameObject Darkness in DarknessObjects) Darkness.GetComponent<Darkness>().FadeOut(1f, this);
        }
    }

    public void FinishedFading()
	{
        Explored = true;
        playerComponent.InTransition = false;
        if (Enemies != null)
        {
            foreach (GameObject Enemy in Enemies)
            {
                Entity e = Enemy.GetComponent<Entity>();

                e.InternalBulletDelay = Random.Range(-1.5f, -0.5f);
                e.AIActive = true;
            }
        }
    }
}
