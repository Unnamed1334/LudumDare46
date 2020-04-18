using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public AirProduction production;
    public Room room;

    public bool open;

    public int transferRate = 200;
    public float airTimer = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Updating air
        airTimer -= Time.deltaTime;
        if (airTimer < 0) {
            airTimer += 1;
            if(room.air < room.airMax) {
                int transfer = Mathf.Min(transferRate, production.air);
                production.air -= transfer;
                room.air += transfer;
            }
        }
    }

    public void ToggleVent() {
        open = !open;
    }
}
