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
    private int _countWave;

    private const int ConesInWaveCount = 4;

    public bool IsReady { get; private set; }
    public int CurrentConesCount => _cones.Count;
    public int CountWave => _countWave;

    private void Start()
    {
        _cones = new Stack<Cone>();
        _waitForSeconds = new WaitForSeconds(0.1f);
        _conePrefab = _startConePrefab;
        IsReady = false;

        if (SceneData.CountWaveSpawner > 0)
            _countWave = SceneData.CountWaveSpawner;
        else
            _countWave = 5;
    }

    private void Update()
    {
        if (CurrentConesCount == ConesInWaveCount * _countWave)
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

        for (int i = 0; i < _countWave; i++)
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
                cone = Instantiate(_conePrefab, position, Quaternion.identity);
                cone.SetMoneyPoint(_moneyPoint);
                _cones.Push(cone);

                yield return _waitForSeconds;
            }
        }
    }

    public void GiveAwayCone(Bag bag)
    {
        if (CurrentConesCount > 0)
            bag.AddCone(_cones.Pop());
    }

    public void IncreaseCountWaves()
    {
        _countWave++;
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
