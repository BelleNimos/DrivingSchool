using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Scale _sliderCone;
    [SerializeField] private Cone _startConePrefab;
    [SerializeField] private CashCounter _cashCounter;

    private Stack<Cone> _cones;
    private Cone _conePrefab;
    private int _countWaves = 5;
    private float _timer = 0f;

    private const int ConesInWaveCount = 4;
    private const float DelayGiveAway = 0.05f;

    public bool IsReady { get; private set; } = false;
    public int IndexCone => _conePrefab.Index;
    public int CurrentConesCount => _cones.Count;
    public int CountWaves => _countWaves;

    private void Awake()
    {
        _cones = new Stack<Cone>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (CurrentConesCount == ConesInWaveCount * _countWaves)
            IsReady = true;
        else if (IsReady == true && CurrentConesCount > 0)
            IsReady = true;
        else
            IsReady = false;

        if (_sliderCone.IsEmpty == true)
        {
            _sliderCone.gameObject.SetActive(false);
            StartCoroutine(InstantiateCones());
        }
    }

    private IEnumerator InstantiateCones()
    {
        float distanceCoefficient = 0.25f;

        for (int i = 0; i < _countWaves; i++)
        {
            Cone cone;

            float positionX;
            float positionY;
            float positionZ;

            for (int j = 0; j < _points.Count; j++)
            {
                positionX = _points[j].position.x;
                positionY = _points[j].position.y;
                positionZ = _points[j].position.z;

                positionY = (positionY + i) * distanceCoefficient;
                Vector3 position = new Vector3(positionX, positionY, positionZ);
                cone = Instantiate(_conePrefab, position, Quaternion.Euler(0f, 20f, 0f));
                cone.SetCashCounter(_cashCounter);
                _cones.Push(cone);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void SetDefaultValues()
    {
        _conePrefab = _startConePrefab;
    }

    public void SetStartValues(int countWaves, int indexCone)
    {
        _countWaves = countWaves;
        SpawnerPrefabs.SpawnerPrefabsInstance.GiveAwayPrefab(this, indexCone);
    }

    public void GiveAwayCone(Bag bag)
    {
        if (CurrentConesCount > 0 && _timer >= DelayGiveAway)
        {
            bag.AddCone(_cones.Pop());
            _timer = 0f;
        }
    }

    public void IncreaseCountWaves()
    {
        _countWaves++;
    }

    public void ChangeConePrefab(Cone cone)
    {
        _conePrefab = cone;
    }

    public void EnableSilder()
    {
        _sliderCone.gameObject.SetActive(true);
    }
}
