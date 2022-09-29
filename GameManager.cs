using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> prefabs;
    Vector2 position;
    public TMP_Text scoreText;
    public int scoreCount;
    PlayerController pc;

    void Start()
    {
        scoreCount = 0;
        position = new Vector2(-90,55);
        Instantiate(prefabs[2], position, prefabs[2].transform.rotation);
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnHurdles());
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreCount;
        if (pc.gameOver == true)
        {
            StopCoroutine(SpawnHurdles());
        }
    }
    IEnumerator SpawnHurdles()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            int index = Random.Range(0, prefabs.Count);
            Instantiate(prefabs[index], position, prefabs[index].transform.rotation);

        }
    }
}
