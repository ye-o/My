﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Enemies;
    public float SpawnTime = 5f;
    public float SpawnDelay = 3f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", SpawnDelay, SpawnTime);
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, Enemies.Length);
        Instantiate(Enemies[index], transform.position, transform.localRotation);
    }
}
