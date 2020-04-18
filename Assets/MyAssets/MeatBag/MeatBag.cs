using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatBag : MonoBehaviour
{
    public Room currentRoom;

    public GameObject[] targets;

    public int health;
    public int healthMax;

    public int air;
    public int airMax;
    public int airDrain;
    public int airRefill;

    public float airTimer = 1;

    public Ship ship;
    public NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRoom = ship.GetRoom(transform.position);

        // Updating air
        airTimer -= Time.deltaTime;
        if(airTimer < 0) {
            airTimer += 1;
            if(currentRoom != null && air < airMax) {
                currentRoom.air -= airRefill;
                air += airRefill;
            }
            air -= airDrain;
            if(air < 0) {
                health += air;
                air = 0;
            }
        }

        // If the destination is reached or we can no longer reach it
        if (nav.remainingDistance < .25f || nav.path.status != NavMeshPathStatus.PathComplete) {
            if(targets.Length != 0) {
                List<GameObject> posibleTargets = new List<GameObject>(targets);

                int idx = Mathf.FloorToInt(posibleTargets.Count * Random.value * .999f);
                GameObject target = posibleTargets[idx];
                NavMeshPath navMeshPath = new NavMeshPath();
                nav.CalculatePath(target.transform.position, navMeshPath);
                // No valid Path
                if (navMeshPath.status == NavMeshPathStatus.PathComplete) {
                    nav.SetPath(navMeshPath);
                }
            }
        }
        
    }
}
