using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool open;

    public GameObject door;

    // Start is called before the first frame update
    void Start() {
        Ship ship = GameObject.Find("_Ship").GetComponent<Ship>();
        Room nRoom = ship.GetRoom(transform.position + Vector3.forward);
        Room sRoom = ship.GetRoom(transform.position - Vector3.forward);
        Room eRoom = ship.GetRoom(transform.position + Vector3.right);
        Room wRoom = ship.GetRoom(transform.position - Vector3.right);
        if (nRoom != null) {
            nRoom.connections.Add(new Room.RoomConnection(sRoom, this));
        }
        if (sRoom != null) {
            sRoom.connections.Add(new Room.RoomConnection(nRoom, this));
        }
        if (eRoom != null) {
            eRoom.connections.Add(new Room.RoomConnection(wRoom, this));
        }
        if (wRoom != null) {
            wRoom.connections.Add(new Room.RoomConnection(eRoom, this));
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void ToggleDoor() {
        open = !open;
        door.SetActive(open);
    }
}
