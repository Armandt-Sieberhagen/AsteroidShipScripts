using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float Radius;
    [SerializeField] int MaxRocks;
    [SerializeField] float TimeBeforeRocksIncrease;
    [SerializeField] float TimeBetweenRocks;
    [SerializeField] float RockStrength;
    [SerializeField] GameObject Rock;
    [SerializeField] GameObject Player;
    [SerializeField] public int CurrentAmountOfRocks;
    [SerializeField] public int CurrentRocksBroken;
    float CurrentTimer;
    float RockCurrentTimer;

    void Start()
    {
        CurrentAmountOfRocks = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentTimer<=Time.deltaTime)
        {
            MaxRocks++;
            SpawnRockAndAddForceToRock();
            CurrentTimer = TimeBeforeRocksIncrease;
        }
        else CurrentTimer -= Time.deltaTime;
        SpawnRockAndAddForceToRock();
    }

    void SpawnRockAndAddForceToRock()
    {
        if (CurrentAmountOfRocks < MaxRocks && RockCurrentTimer < Time.deltaTime)
        {
            GameObject go = Instantiate(Rock, CreateCirclePosition(), Quaternion.identity);
            go.transform.LookAt(Player.transform.position);
            ShootOffInDirection(go.GetComponent<Rigidbody>());
            go.GetComponent<Rock>().Spawner = this;
            CurrentAmountOfRocks++;
            RockCurrentTimer = TimeBetweenRocks;
            TimeBetweenRocks = Mathf.Lerp(TimeBetweenRocks,0.1f,Time.deltaTime*0.5f);
        }
        else RockCurrentTimer -= Time.deltaTime;
    }

    Vector3 CreateCirclePosition()
    {
        float angle = Random.Range(0, 360) * Mathf.PI * 2f / MaxRocks;
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * Radius, 0, Mathf.Sin(angle) * Radius);
        return newPos;
    }

    void ShootOffInDirection(Rigidbody body)
    {
        Vector3 direction = Player.transform.position - body.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Random.Range(-20,20);
        Vector3 finalDir = new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.z);
        Quaternion rot = Quaternion.Euler(finalDir);
        transform.rotation = rot;
        body.AddForce(transform.forward * RockStrength);
    }
}
