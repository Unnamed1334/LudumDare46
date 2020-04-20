using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    public GameState instance;

    public TextMeshProUGUI timeOutput;

    public float timeRemaining;
    public int goal = 100;
    public int productivity = 0;

    public int lives = 3;

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
            "\nProduction Goal:" + goal +
            "\nProduction:" + productivity +
            "\nLives:" + lives;

    }

    public void MeatbagDeath() {
        lives--;
        if(lives <= 0) {
            GameOver();
        }
    }

    public void GameOver() {

    }
}
