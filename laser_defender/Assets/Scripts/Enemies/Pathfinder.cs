using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpawner _enemySpawner;
    WaveConfigSO _waveConfig;

    List<Transform> _waypoints;
    int _waypointIndex = 0;

    void Awake()
    {
        _enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }
    void Start()
    {
        _waveConfig = _enemySpawner.CurrentWave;
        _waypoints = _waveConfig.GetWaypoints();
        transform.position = _waypoints[_waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            Vector3 targetPosition = _waypoints[_waypointIndex].position;
            float delta = _waveConfig.MoveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

            if (transform.position == targetPosition)
                _waypointIndex++;
        }
        else
            Destroy(gameObject);
    }
}
