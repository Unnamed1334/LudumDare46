using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCamera : MonoBehaviour {
    public Ship ship;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 newPos = transform.position;
        if (Input.GetKey(KeyCode.W)) {
            newPos.z +=  7 * Time.deltaTime;
            if(newPos.z > ship.shipHeight) {
                newPos.z = ship.shipHeight;
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            newPos.z -= 7 * Time.deltaTime;
            if (newPos.z < 0) {
                newPos.z = 0;
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            newPos.x += 7 * Time.deltaTime;
            if (newPos.x > ship.shipWidth) {
                newPos.x = ship.shipWidth;
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            newPos.x -= 7 * Time.deltaTime;
            if (newPos.x < 0) {
                newPos.x = 0;
            }
        }
        transform.position = newPos;
    }
}
