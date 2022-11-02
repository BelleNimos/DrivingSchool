using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Scale _sliderCone;
    [SerializeField] private GameObject _cone;
    [SerializeField] private float _delay;
    [SerializeField] private UnityEvent _allConesInWaveSpawned;

    private Stack<Cone> _cones;
    private Wave _currentWave;
    private float _timeAfterLastSpawn;
    private int _counter;
    private int _currentWaveNumber = 0;

    private const int MaxCountCones = 20;
    private const int CountCones = 4;

    public int CurrentConesCount => _cones.Count;

    private void Start()
    {
        _cones = new Stack<Cone>();
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_sliderCone.IsEmpty == true)
        {
            _sliderCone.gameObject.SetActive(false);

            ResetWave();
        }

        Generate();
    }

    private void Generate()
    {
        _timeAfterLastSpawn += Time.deltaTime;

        if (CurrentConesCount < MaxCountCones)
        {
            if (_timeAfterLastSpawn >= _delay)
            {
                InstantiateCone();
                _counter++;
                _timeAfterLastSpawn = 0;
            }
        }

        if (_waves.Count > _currentWaveNumber + 1)
            if (_counter == CountCones)
                _allConesInWaveSpawned?.Invoke();
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void InstantiateCone()
    {
        if (_counter == 0)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint2.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 1)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint3.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 2)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint4.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
        if (_counter == 3)
        {
            Cone cone = Instantiate(_cone, _waves[_currentWaveNumber].SpawnPoint1.transform).GetComponent<Cone>();
            _cones.Push(cone);
        }
    }

    public void NextWave()
    {
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
        _counter = 0;
    }

    public void ResetWave()
    {
        _currentWaveNumber = 0;
        _counter = 0;
    }

    public Cone GetCone()
    {
        return _cones.Pop();
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
