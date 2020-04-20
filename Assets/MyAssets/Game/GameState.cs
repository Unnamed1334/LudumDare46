using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    public GameState instance;

    public TextMeshProUGUI timeOutput;

    public float timeRemaining;
    public int productivity;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0) {

        }

        timeOutput.text = "Time Remaining: "+
            Mathf.FloorToInt(timeRemaining / 60) + ":" +
            Mathf.FloorToInt(timeRemaining % 60) +
            "\nProduction Goal:" + 100 +
            "\nProduction:" + 0;

    }
}
