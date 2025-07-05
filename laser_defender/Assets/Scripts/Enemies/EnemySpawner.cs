using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> _waveConfigs;
    public WaveConfigSO CurrentWave { get; private set; }

    [SerializeField] float _timeBetweenWaves = 0f;

    bool _isSpawningEnemies = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    private IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO waveConfig in _waveConfigs)
            {
                CurrentWave = waveConfig;

                for (int i = 0; i < CurrentWave.GetEnemyCount(); i++)
                {
                    Instantiate(CurrentWave.GetEnemyPrefabAt(i), CurrentWave.GetStartingWaypoint().position, Quaternion.identity, transform);
                    yield return new WaitForSeconds(CurrentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }
        while (_isSpawningEnemies);
    }
}
