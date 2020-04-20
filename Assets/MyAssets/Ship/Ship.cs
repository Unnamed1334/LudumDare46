using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public Room[] rooms;
    public int[] roomLookup;
    public int shipWidth;
    public int shipHeight;

    public string shipString;

    public GameObject roomPrefab;

    private void Awake() {
        gameObject.name = "_Ship";

        for(int i = 0; i < rooms.Length; i++) {
            rooms[i] = Instantiate(roomPrefab).GetComponent<Room>();
            rooms[i].name = "Room " + i;
        }

        shipWidth = 0;
        shipHeight = 0;

        List<int> layout = new List<int>();
        int width = 0;
        for (int i = 0; i < shipString.Length; i++) {
            char tile = shipString[i];

            if (tile == 'r') {
                shipHeight++;
                shipWidth = width;
                width = 0;
            }
            else if (tile == '.') {
                width++;
                layout.Add(-1);
            }
            else {
                try {
                    int idx = int.Parse("" + tile);
                    layout.Add(idx);
                    rooms[idx].AddTile(new Vector3(width, 0, -shipHeight));
                    width++;
                }
                catch (FormatException) {
                }
            }
        }
        roomLookup = layout.ToArray();


        for (int i = 0; i < rooms.Length; i++) {
            rooms[i].transform.position += shipHeight * Vector3.forward;
        }
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public Room GetRoom(Vector3 position) {
        int x = Mathf.FloorToInt(position.x);
        int y = shipHeight - Mathf.CeilToInt(position.z);
        int idx = x + shipWidth * y;
        //Debug.Log(x + ", " + y);
        if (x < 0 || x >= shipWidth || y < 0 || idx >= roomLookup.Length) {
            return null;
        }

        int roomidx = roomLookup[idx];
        if (roomidx < 0 || roomidx >= rooms.Length) {
            return null;
        }

        return rooms[roomidx];
    }
}
    