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
            info.text = "Air: \n" + (100 * air / airMax) + "%\nToxin:\n toxin";
        }

        airTimer -= Time.deltaTime;
        if (airTimer < 0) {
            airTimer += 1;
            for(int i = 0; i < connections.Count; i++) {
                if(connections[i].door.open) {
                    int diff = (air - connections[i].room.air) / 2;
                    diff = Mathf.Clamp(diff, -transferRate/2, transferRate/2);
                    air -= diff;
                    connections[i].room.air += diff;
                }
            }
        }
    }

    public void AddTile(Vector3 tilePosition) {
        Vector3 infoPos = info.transform.parent.position;
        infoPos.x = Mathf.Min(info.transform.parent.position.x, tilePosition.x);
        infoPos.z = Mathf.Max(info.transform.parent.position.z, tilePosition.z);
        info.transform.parent.position = infoPos;
    }

    [System.Serializable]
    public class RoomConnection {
        public Room room;
        public Door door;

        public RoomConnection(Room otherRoom, Door otherDoor) {
            room = otherRoom;
            door = otherDoor;
        }
    }
}
