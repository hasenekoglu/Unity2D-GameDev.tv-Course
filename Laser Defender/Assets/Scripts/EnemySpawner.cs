using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] WaveConfigSO currentWave;
    void Start()
    {
        SpawnEnemies();
    }
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate
                    (
                    currentWave.GetEnemyPrefab(0),
                    currentWave.GetStartingWayPoint().position,
                    Quaternion.identity,
                    transform
                    );
        }

    }
}
