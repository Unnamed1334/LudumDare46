using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPurifier : MonoBehaviour {
   
    public int maxCapacity;
    public int air = 0;
    public int toxin = 0;

    public float airTimer = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Updating air
        airTimer -= Time.deltaTime;
        if (airTimer < 0) {
            airTimer += 1;
            toxin = Mathf.Max(toxin - 100, 0);
        }
    }
}
