using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Room[] rooms;
    public int[] roomLookup;
    public int shipWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Room GetRoom(Vector3 position) {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.z);
        int idx = x + shipWidth * y;
        if (x < 0 || x >= shipWidth || y < 0 || idx > roomLookup.Length) {
            return null;
        }
        int roomidx = roomLookup[idx];
        return rooms[roomidx];
    }
}
    