using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;

    public Transform GetStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }
    public List<Transform> GetWayPoints()
    {
        List<Transform> wayPoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            wayPoints.Add(child);

        }
        return wayPoints;

    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }
    public GameObject GetEnemyPrefab(int index)
    {


        return enemyPrefabs[index];
    }
}
