using System.Collections.Generic;
using UnityEngine;

public class SpawnerPrefabs : MonoBehaviour
{
    [SerializeField] private List<Cone> _conePrefabs;

    public static SpawnerPrefabs SpawnerPrefabsInstance = null;

    private void Awake()
    {
        if (SpawnerPrefabsInstance == null)
        {
            SpawnerPrefabsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GiveAwayPrefab(Spawner spawner, int indexCone)
    {
        foreach (var cone in _conePrefabs)
            if (cone.Index == indexCone)
                spawner.ChangeConePrefab(cone);
    }
}
