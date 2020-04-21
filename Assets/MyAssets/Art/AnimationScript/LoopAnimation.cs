using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    public int idx = 0;
    public Sprite[] sprites;
    public float frameTime = 0.1f;
    public float currentTime = 0;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        idx = Mathf.FloorToInt(.99f * Random.value * sprites.Length);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime < 0) {
            currentTime += frameTime;
            idx = (idx + 1) % sprites.Length;
            rend.sprite = sprites[idx];
        }
    }
}
