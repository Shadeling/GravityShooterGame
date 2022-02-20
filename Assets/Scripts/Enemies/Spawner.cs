using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float TimeBetweenSpawns = 5;
    [SerializeField] int maxCount = 10;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] spawnPoints;

    private float _time = 0;
    private int _count = 0;
    private float _randomTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_count < maxCount && _time > TimeBetweenSpawns+_randomTime)
        {
            if (spawnPoints.Length > 0)
            {
                var _mob = Instantiate(prefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position, Quaternion.identity);
                if(_mob.TryGetComponent<EnemyHealth>(out var enemyHP)) enemyHP.onEnemyDie += OnPrefabDie;

                if (_mob.TryGetComponent<Zombie>(out var zombie)) zombie.SetPatrolPoints(spawnPoints);

                _count++;
                _time = 0;
                _randomTime = Random.Range(0, TimeBetweenSpawns);
            }
        }
    }

    void OnPrefabDie()
    {
        if (_count != 0)
        {
            _count--;
        }
    }
}
