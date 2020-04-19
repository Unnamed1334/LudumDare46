using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour {
    public TextMeshPro info;

    public List<RoomConnection> connections = new List<RoomConnection>();

    public int airMax = 1000;
    public int air = 1000;
    public int toxin = 0;

    public int transferRate = 200;
    public float airTimer = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(info != null) {
            info.text = "Air: " + (100 * air / airMax) + "%\nToxin: toxin";
        }

        airTimer -= Time.deltaTime;
        if (airTimer < 0) {
            airTimer += 1;
            //if (room.air < room.airMax) {
            //    int transfer = Mathf.Min(transferRate, production.air);
            //    production.air -= transfer;
            //    room.air += transfer;
            //}
        }
    }

    [System.Serializable]
    public class RoomConnection {
        Room room;
        Door door;

        public RoomConnection(Room otherRoom, Door otherDoor) {
            room = otherRoom;
            door = otherDoor;
        }
    }
}
