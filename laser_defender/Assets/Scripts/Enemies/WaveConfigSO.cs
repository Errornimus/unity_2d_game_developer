using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform _pathPrefab;

    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;

    [SerializeField] List<GameObject> _enemyPrefabs;
    [SerializeField] float _timeBetweenEnemySpawns = 1f;
    [SerializeField] float _spawnTimeVariance = 0f;
    [SerializeField] float _minimumSpawnTime = 0.2f;

    public Transform GetStartingWaypoint()
    {
        return _pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in _pathPrefab)
            waypoints.Add(child);

        return waypoints;
    }

    public int GetEnemyCount()
    {
        return _enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefabAt(int index)
    {
        return _enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(_timeBetweenEnemySpawns - _spawnTimeVariance, _timeBetweenEnemySpawns + _spawnTimeVariance);
        return Mathf.Clamp(spawnTime, _minimumSpawnTime, float.MaxValue);
    }
}
