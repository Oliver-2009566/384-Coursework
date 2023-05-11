using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public float verticalBoundary =  11.3f;
    public float horizontalBoundary = 18.7f;
    public GameObject playerPrefab;
    public GameObject asteroidPrefab;
    private int score;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.GetComponent<Player>().verticalBoundary = verticalBoundary - 0.8f;
        player.GetComponent<Player>().horizontalBoundary = horizontalBoundary - 0.8f;

        spawnAsteroid();

        score = 0;
    }

    // Update is called once per frame
    private void spawnAsteroid()
    {
        int spawnLocation = Random.Range(0, 3);
        Vector2 asteroidPos;
        float scale = Random.Range(0.5f, 5f);
        if (spawnLocation == 0)
        {
            // Left side
            asteroidPos = new Vector2(-(horizontalBoundary), Random.Range(-(verticalBoundary), verticalBoundary));
        }
        else if (spawnLocation == 1)
        {   
            // Right side
            asteroidPos = new Vector2(horizontalBoundary, Random.Range(-(verticalBoundary), verticalBoundary));
        }
        else if (spawnLocation == 2)
        {
            // Top side
            asteroidPos = new Vector2(Random.Range(-(horizontalBoundary), horizontalBoundary), verticalBoundary);
        }
        else
        {
            // Bottom side
        asteroidPos = new Vector2(Random.Range(-(horizontalBoundary), horizontalBoundary), -(verticalBoundary));
        }
        GameObject asteroid = Instantiate(asteroidPrefab);
        asteroid.transform.localPosition = asteroidPos;
        asteroid.transform.localScale = new Vector3(scale, scale, 1);
        asteroid.GetComponent<Asteroid>().verticalBoundary = verticalBoundary;
        asteroid.GetComponent<Asteroid>().horizontalBoundary = horizontalBoundary;
    }
}
