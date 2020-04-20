using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatBag : MonoBehaviour {
    public Ship ship;
    public Room currentRoom;

    public int health;
    public int healthMax;

    public int air;
    public int airMax;
    public int airDrain;
    public int airRefill;

    public float airTimer = 1;

    public int suspicion = 0;
    public float lazy = 0;

    // Task
    public Task task;

    // Unit types
    public bool engineerUnit = false;
    public bool secUnit = false;
    public bool cargoUnit = false;
    public bool manufacturerUnit = false;
    public bool traitorUnit = false;


    //Hacky debug state
    public int cargo = 0;
    public int material = 0;
    public int product = 0;
    public int export = 0;
    public MeatBag criminal;


    public float idleTimer;
    public NavMeshAgent nav;
    public GameObject icon;

    public GameObject normalGraphic;
    public GameObject lowGraphic;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        ship = GameObject.Find("_Ship").GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRoom = ship.GetRoom(transform.position);

        // Updating air
        airTimer -= Time.deltaTime;
        if(airTimer < 0) {
            airTimer += 1;
            // Room Refill
            if(currentRoom != null && air < airMax && currentRoom.air >= airRefill) {
                currentRoom.air -= airRefill;
                air += airRefill;
            }
            // Air Drain
            air -= airDrain;
            if(air < 0) {
                health += air;
                air = 0;
                if(health < 0) {
                    Die();
                }
            }
        }

        //If the path fails
        if(nav.path.status != NavMeshPathStatus.PathComplete) {
            InteruptTask();
            NewTask();
        }

        // If the destination is reached or we can no longer reach it
        if (nav.remainingDistance < .25f) {
            if (idleTimer < 0) {
                EndTask();
                NewTask();
            }
            else {
                idleTimer -= Time.deltaTime;
            }
        }

        // Icon
        if (health > 50) {
            normalGraphic.SetActive(true);
            lowGraphic.SetActive(false);
        }
        else {
            normalGraphic.SetActive(false);
            lowGraphic.SetActive(true);
        }
        
    }

    public void NewTask() {
        List<Task> tasks = TaskManager.instance.tasks;
        Task bestTask = null;
        float bestValue = lazy;
        NavMeshPath bestPath = new NavMeshPath();
        
        for (int i = 0; i < tasks.Count; i++) {
            float newValue = 0;
            Task nextTask = tasks[i];

            if (nextTask.claim == null) {
                // Make sure the target is reachable
                NavMeshPath newPath = new NavMeshPath();
                nav.CalculatePath(nextTask.transform.position, newPath);
                if (newPath.status == NavMeshPathStatus.PathComplete) {
                    float distance = Vector3.Distance(nextTask.transform.position, transform.position);

                    if (nextTask.type == Task.TaskType.Nothing) {
                        // Do Nothing
                    }
                    if (nextTask.type == Task.TaskType.Interact) {
                        float baseCost = Random.value * (2 * lazy - 0.1f * distance);
                        newValue = baseCost;
                    }
                    if (nextTask.type == Task.TaskType.PickUp) {
                        newValue = 0;
                        if (cargoUnit && cargo == 0) {
                            float baseCost = 40 - distance;
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Deviver) {
                        newValue = 0;
                        if (cargoUnit && cargo == 1) {
                            float baseCost = 40 - distance;
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Produce) {
                        newValue = 0;
                        if (manufacturerUnit) {
                            float baseCost = 40 - distance;
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Door) {
                        // Engineers repair
                        if (engineerUnit) {
                            float baseCost = nextTask.priority * (40 - distance);
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Vent) {
                        // Engineers repair
                        if (engineerUnit) {
                            float baseCost = nextTask.priority * (40 - distance);
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Fire) {
                        newValue = 0;
                        // Engineers repair
                        if (traitorUnit) {
                            float baseCost = 40 - distance;
                            newValue = baseCost;
                        }
                    }
                    if (nextTask.type == Task.TaskType.Person) {
                        if (secUnit) {
                            if (traitorUnit) {
                                // Arrest Anyone
                                float baseCost = 3 * suspicion - 10;
                                newValue = baseCost;
                            }
                            else {
                                // Only arrest high suspicion
                                float baseCost = 5 * suspicion - 40;
                                newValue = baseCost;
                            }
                        }
                    }
                    if (nextTask.type == Task.TaskType.Jail) {
                        // Do Nothing, Not a valid target
                    }
                    if (nextTask.type == Task.TaskType.Exit) {
                        // Do Nothing, Not a valid target
                    }

                    Debug.Log("Unit: " + gameObject.name + ", Type: " + nextTask.type + ", Value: " + newValue);

                    // Update if action is better
                    if (newValue > bestValue) {
                        bestTask = nextTask;
                        bestValue = newValue;
                        bestPath = newPath;
                    }
                }
            }
        }
        // Default task, Move a bit
        if(bestTask == null) {
            // 3 attempts
            for(int i = 0; i < 3; i++) {
                Vector2 circle = Random.insideUnitCircle;
                nav.CalculatePath(transform.position + new Vector3(3 * circle.x, 0, 3 * circle.y), bestPath);
                if (bestPath.status == NavMeshPathStatus.PathComplete) {
                    i = 100;
                    nav.SetPath(bestPath);
                    idleTimer = 3 + 4 * Random.value;
                }
            }
        }
        else {
            bestTask.claim = this;
            if (bestTask.type == Task.TaskType.Interact) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 8 + 4 * Random.value;
            }
            if (bestTask.type == Task.TaskType.PickUp) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 8 + 4 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Deviver) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 8 + 4 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Produce) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 8 + 4 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Door) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 2 + 2 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Vent) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 2 + 2 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Fire) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 8 + 4 * Random.value;
            }
            if (bestTask.type == Task.TaskType.Person) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 1;
            }
            if (bestTask.type == Task.TaskType.Jail) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 40;
            }
            if (bestTask.type == Task.TaskType.Exit) {
                task = bestTask;
                nav.SetPath(bestPath);
                idleTimer = 5;
            }
        }
    }

    public void EndTask() {
        if (task == null) {
            lazy -= 5;
        }
        else {
            task.claim = null;
            if (task.type == Task.TaskType.Interact) {
                lazy -= 10;
            }
            if (task.type == Task.TaskType.Vent) {
                task.priority -= 1;
                lazy += 10;
            }
            if (task.type == Task.TaskType.Door) {
                task.priority -= 1;
                lazy += 10;
            }
        }
    }

    public void InteruptTask() {
        if (task != null) {
            task.claim = null;
        }
    }

    public void Die() {

    }

    void LateUpdate() {
        if(icon != null) {
            icon.transform.rotation = Quaternion.Euler(90, 0, 0);
            normalGraphic.transform.rotation = Quaternion.Euler(90, 0, 0);
            lowGraphic.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
