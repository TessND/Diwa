using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float _range;
    [SerializeField] float _decreaseSpawnTime;
    [SerializeField] float _spawnRate;
    [SerializeField] float _spawnMinus;
    [SerializeField] GameObject[] _enemyPrefabs;

    Vector2 _player;
    Vector2 _enemySpawn;

    float _spawn;

    private void Update()
    {
        if (Time.time > _spawn + _spawnRate)
            Spawn();

        DecreaseSpawnRate();
    }

    private void Spawn()
    {
        _player = PlayerBehaviour.Instance.transform.position + new Vector3(10f,10f,0);

        _enemySpawn.x = Random.Range(_player.x - _range, _player.x + _range);
        _enemySpawn.y = Random.Range(_player.y - _range, _player.y + _range);

        int randomIndex = Random.Range(0, _enemyPrefabs.Length);

        Instantiate(_enemyPrefabs[randomIndex], new Vector2(_enemySpawn.x, _enemySpawn.y), Quaternion.identity);
        _spawn = Time.time;
    }

    private void DecreaseSpawnRate()
    {
        if (Time.time > _decreaseSpawnTime)
            _spawnRate -= _spawnMinus;
    }    
}
