using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] PlayerSetup[] _allPlayerSetups;
    [SerializeField] GameOverScreen _gameOverScreen;
    [SerializeField] PlayerSelectionScreen _playerSelectionScreen;

    Round _round;
    int _pointsToWin;
    Player[] _players;

    static Game instance;
    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Game>();
            }
            return instance;
        }
    }

    private void Start()
    {
        _playerSelectionScreen.OnPlayersNumberSelected += OnPlayersNumberSelected;
        _pointsToWin = 45;
    }

    public void Restart()
    {
        foreach (var setup in _allPlayerSetups)
        {
            setup.gameObject.SetActive(true);
            setup.RestoreSetup();
        }
        _playerSelectionScreen.gameObject.SetActive(true);

        DiceManager.Instance.Reset();
    }

    private void OnPlayersNumberSelected(int number)
    {
        _players = GameHelper.GetPlayers(number, _allPlayerSetups);
        _round = new Round(_players);
        _round.OnRoundFinished += Round_OnRoundFinished;
    }

    private void Round_OnRoundFinished(Player[] players)
    {
        _round.OnRoundFinished -= Round_OnRoundFinished;
        _round = new Round(players);
        _round.OnRoundFinished += Round_OnRoundFinished;
        DiceManager.Instance.Reset();
    }

    public void CheckWinners()
    {
        if (IsGameOver(out int id))
        {
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.UpdateWinnersText(_players[id].Name);
        }
    }

    private bool IsGameOver(out int winnerId)
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i].Score >= _pointsToWin || !_players[i].Setup.HasAnyChips())
            {
                winnerId = i;
                return true;
            }
        }
        winnerId = -1;
        return false;
    }
}
