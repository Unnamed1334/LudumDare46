using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour {
    public AirProduction production;
    public Room room;

    public bool open;
    public bool broken;

    public int transferRate = 200;
    public float airTimer = 1;

    public GameObject working;
    public GameObject notWorking;

    public GameObject sparks;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (room == null) {
            room = GameObject.Find("_Ship").GetComponent<Ship>().GetRoom(transform.position);
        }
        if (production == null) {
            production = GameObject.Find("_AirSystem").GetComponent<AirProduction>();
        }
        // Updating air
        if(open) {
            airTimer -= Time.deltaTime;
            if (airTimer < 0) {
                airTimer += 1;
                if (room.air < room.airMax) {
                    int transfer = Mathf.Min(transferRate, production.air);
                    production.air -= transfer;
                    room.air += transfer;
                }
            }
        }
        sparks.SetActive(broken);
    }

    public void ToggleVent() {
        if(!broken) {
            open = !open;
            working.SetActive(open);
            notWorking.SetActive(!open);
        }
    }
}
