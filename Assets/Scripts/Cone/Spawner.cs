using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private GameObject _cone;
    [SerializeField] private float _delay;

    private Stack<Cone> _cones;
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private int _counter;
    private float _timeAfterLastSpawn;

    private const int MaxCountCones = 20;
    private const int CountCones = 4;

    [SerializeField] private UnityEvent AllConeSpawned;

    private void Start()
    {
        _cones = new Stack<Cone>();
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _delay && _cones.Count < MaxCountCones)
        {
            InstantiateCone();
            _counter++;
            _timeAfterLastSpawn = 0;
        }

        if (CountCones <= _counter)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllConeSpawned?.Invoke();

            _currentWave = null;
        }
    }

    public void NextWave()
    {
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
        _counter = 0;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void InstantiateCone()
    {
        if (_counter == 0)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint1.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 1)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint2.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 2)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint3.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 3)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint4.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public Transform SpawnPoint1;
        public Transform SpawnPoint2;
        public Transform SpawnPoint3;
        public Transform SpawnPoint4;
    }
}
