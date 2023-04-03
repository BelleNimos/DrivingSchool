using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
    [SerializeField] private Transform _parentObject;

    private List<LeaderboardElement> _spawnedElements = new();

    public void ConstructLeaderboard(List<PlayerInfoLeaderboard> playersInfo)
    {
        ClearLeaderboard();

        foreach (var playerInfo in playersInfo)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _parentObject);

            LeaderboardElement leaderboardElement = leaderboardElementInstance;
            leaderboardElement.Initialize(playerInfo.Name, playerInfo.Score);

            _spawnedElements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
            Destroy(element.gameObject);

        _spawnedElements.Clear();
    }
}
