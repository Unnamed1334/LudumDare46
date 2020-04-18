using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Room[] rooms;
    public int[] roomLookup;
    public int shipWidth;

    public string shipString;

    // Start is called before the first frame update
    void Start()
    {
        List<int> layout = new List<int>();
        int width = 0;
        for(int i = 0; i < shipString.Length; i++) {
            char tile = shipString[i];

            if (tile == '\n') {
                shipWidth = width;
                width = 0;
            }
            else if (tile == '_') {
                width++;
                layout.Add(-1);
            }
            else {
                try {
                    int idx = int.Parse("" + tile);
                    width++;
                    layout.Add(idx);
                }
                catch (FormatException) {
                }
            }
        }
        roomLookup = layout.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Room GetRoom(Vector3 position) {
        int x = Mathf.FloorToInt(position.x);
        int y = roomLookup.Length / shipWidth - Mathf.CeilToInt(position.z);
        int idx = x + shipWidth * y;
        Debug.Log(x + ", " + y);
        if (x < 0 || x >= shipWidth || y < 0 || idx >= roomLookup.Length) {
            return null;
        }

        int roomidx = roomLookup[idx];
        if(roomidx < 0 || roomidx >= rooms.Length) {
            return null;
        }
            
        return rooms[roomidx];
    }
}
    