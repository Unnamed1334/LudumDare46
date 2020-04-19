using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : MonoBehaviour {

    public string shipString;

    public List<GameObject> mapPrefabs;


    // Start is called before the first frame update
    void Start() {
        int x = 0;
        int y = 15;
        GameObject root = new GameObject();
        for(int i = 0; i < shipString.Length; i++) {
            char tile = shipString[i];
            if (tile == 'r') {
                x = 0;
                y--;
            }
            else if (tile == '|') {
                Instantiate(mapPrefabs[0], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '#') {
                Instantiate(mapPrefabs[1], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '1') {
                Instantiate(mapPrefabs[3], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '2') {
                Instantiate(mapPrefabs[4], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '3') {
                Instantiate(mapPrefabs[5], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '4') {
                Instantiate(mapPrefabs[6], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'A') {
                Instantiate(mapPrefabs[2], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'B') {
                Instantiate(mapPrefabs[7], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'C') {
                Instantiate(mapPrefabs[8], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'E') {
                Instantiate(mapPrefabs[9], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'M') {
                Instantiate(mapPrefabs[10], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == 'V') {
                Instantiate(mapPrefabs[11], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '_') {
                Instantiate(mapPrefabs[12], new Vector3(x, 0, y), Quaternion.identity).gameObject.transform.parent = root.transform;
                x++;
            }
            else if (tile == '.') {
                x++;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
