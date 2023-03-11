using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Scale _sliderCone;
    [SerializeField] private Cone _startConePrefab;
    [SerializeField] private MoneyPoint _moneyPoint;

    private Stack<Cone> _cones;
    private WaitForSeconds _waitForSeconds;
    private Cone _conePrefab;
    private int _countWaves;
    private float _timer;

    private const int ConesInWaveCount = 4;
    private const float MinTime = 0.05f;

    public bool IsReady { get; private set; }
    public int IndexCone => _conePrefab.GetIndex();
    public int CurrentConesCount => _cones.Count;
    public int CountWaves => _countWaves;

    private void Start()
    {
        _cones = new Stack<Cone>();
        _waitForSeconds = new WaitForSeconds(0.1f);
        
        _timer = 0f;
        IsReady = false;

        if (PlayerPrefs.HasKey(KeysData.IndexCone) == true)
            SpawnerPrefabs.SpawnerPrefabsInstance.GiveAwayPrefab(this);
        else
            _conePrefab = _startConePrefab;

        if (PlayerPrefs.HasKey(KeysData.SpawnerCountWaves) == true)
            _countWaves = PlayerPrefs.GetInt(KeysData.SpawnerCountWaves);
        else
            _countWaves = 5;
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
                cone.SetMoneyPoint(_moneyPoint);
                _cones.Push(cone);

                yield return _waitForSeconds;
            }
        }
    }

    public void GiveAwayCone(Bag bag)
    {
        if (CurrentConesCount > 0 && _timer >= MinTime)
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
