using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Ship ship;
    public Room currentRoom;

    public int airDrain = 30;
    public float airTimer = 1;

    // Start is called before the first frame update
    void Start() {
        ship = GameObject.Find("_Ship").GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update() {
        currentRoom = ship.GetRoom(transform.position);

        airTimer -= Time.deltaTime;
        if (airTimer < 0) {
            airTimer += .5f;
            // Room Refill
            if (currentRoom != null) {
                if (currentRoom.air > airDrain) {
                    currentRoom.air -= airDrain;
                }
                else {
                    currentRoom.air = 0;
                    Destroy(gameObject);
                }
            }
        }
    }
}
