using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<RoomConnection> connections;

    public int airMax = 1000;
    public int air = 1000;
    public int toxin = 0;

    public int transferRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    public class RoomConnection {
        Room room;
        Door door;
    }
}
