using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour {

    public MeatBag claim;
    public float priority = 1;
    public float priorityGrowth = 0;
    public TaskType type;
    
    public enum TaskType {
        Nothing,
        Interact,
        PickUp,
        Deviver,
        Produce,
        Door,
        Vent,
        Fire,
        Person,
        Jail,
        Exit
    }

    // Start is called before the first frame update
    void Start() {
        TaskManager.instance.tasks.Add(this);
    }

    // Update is called once per frame
    void Update() {
        priority += priorityGrowth * Time.deltaTime;
    }
}
