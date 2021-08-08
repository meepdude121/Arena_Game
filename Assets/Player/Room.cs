using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] DarknessObjects;
    public bool Explored;
    public GameObject[] Enemies;

    public GameObject player;
    private Player playerComponent;
    public bool Locked = true;
    public bool ObjectiveRoom = false;
    private bool roomRunning = false;
    public int EnemyCount;
    RoomManager roomManager;
    public Collider[] doorwayColliders;
    private Loot lootManager;
    public Loot.LootTable LootTable;
    public Vector2 Bounds;
    private void Awake()
    {
        EnemyCount = Enemies.Length;
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player>();
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        lootManager = GameObject.Find("LootManager").GetComponent<Loot>();
    }
    public void Update()
    {
        if (roomRunning)
        {
            if (EnemyCount <= 0)
            {
                roomManager.UpdateRooms(false);
                roomRunning = false;
                foreach (Collider collider in doorwayColliders)
                {
                    collider.isTrigger = true;
                    collider.gameObject.GetComponent<Shield>().FadeOut(1f);
                }
                GameObject loot = Instantiate(lootManager.DecideDrop(LootTable));
                loot.transform.position = transform.position;
                loot.GetComponent<Item>().InitItem();
            }
        }
    }
    public void RoomEnter()
    {
        if (!Locked)
        {
            if (!Explored)
            {
                /*
                roomManager.UpdateRooms(true);
                roomRunning = true;
                playerComponent.transitionPosition = new Vector3(transform.position.x, transform.position.y, -1f);
                playerComponent.InTransition = true;
                foreach (GameObject Darkness in DarknessObjects) Darkness.GetComponent<Darkness>().FadeOut(1f, this);
                foreach (Collider collider in doorwayColliders)
                {
                    collider.isTrigger = false;
                    collider.gameObject.GetComponent<Shield>().FadeIn(1f);
                }*/
            }
        }
    }
    public void FinishedFading()
    {
        /*
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
        }*/
    }
}
