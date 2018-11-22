using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {
    public Vector2[] spawnPositions;
    public int carCount;

    private List<GameObject> cars;
    private CarPool carPoolLol;
    private GameState gameState;
    private float spawnCooldown;
    private float spawnTimer;

    // Use this for initialization
    void Start () {
        gameState = FindObjectOfType<GameState>();
        spawnCooldown = 2.5f;
        carPoolLol = GetComponent<CarPool>();
        cars = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState.gameState == States.RUSH)
        {
            // Spawn twice(?) as many cars
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0.0f)
        {
            spawnTimer = spawnCooldown;
            if (cars.Count < carCount)
            {
                int posIndex = Random.Range(0, 3);
                SpawnCar(spawnPositions[posIndex]);
            }
        }
	}

    public void RemoveCar(GameObject car)
    {
        carPoolLol.Recycle(car);
    }

    private void SpawnCar(Vector2 spawnPos)
    {
        Direction carDirection = Direction.Right;
        if (spawnPos.x > 0.0f)
        {
            carDirection = Direction.Left;
        }
        GameObject newCar = carPoolLol.GetNext();
        if (newCar != null)
        {
            newCar.transform.position = spawnPos;
            Car carScript = newCar.GetComponent<Car>();
            carScript.direction = carDirection;
            carScript.spawner = this;
        }
    }

}
