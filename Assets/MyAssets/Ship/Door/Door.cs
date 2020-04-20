using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool open;

    public GameObject door;
    
    public int idx = 0;
    public Sprite[] sprites;
    public float frameTime = 0.1f;
    public float currentTime = 0;

    public SpriteRenderer rend;

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
        currentTime -= Time.deltaTime;
        if (currentTime < 0) {
            currentTime += frameTime;

            if(open) {
                idx--;
            }
            else {
                idx++;
            }
            idx = Mathf.Clamp(idx, 0, sprites.Length - 1);
            rend.sprite = sprites[idx];
        }
    }

    public void ToggleDoor() {
        open = !open;
        door.SetActive(open);

        //idx = 0;
        currentTime = 0;
    }
}
