using System.Collections.Generic;

public static class GameHelper
{
    public static Player[] GetPlayers(int number, Player[] _allPlayers)
    {
        List<Player> _players = new List<Player>();

        if (number == 2)
        {
            _players.Add(_allPlayers[0]);
            _players.Add(_allPlayers[3]);
        }
        else if (number == 3) 
        {
            _players.Add(_allPlayers[0]);
            _players.Add(_allPlayers[2]);
            _players.Add(_allPlayers[4]);
        }
        else if ( number == 4)
        {
            _players.Add(_allPlayers[0]);
            _players.Add(_allPlayers[1]);
            _players.Add(_allPlayers[3]);
            _players.Add(_allPlayers[4]);
        }
        else 
        {
            for (int i = 0; i < number; i++)
            {
                _players.Add(_allPlayers[i]);
            }
        }

        int index = 1;
        foreach (Player player in _allPlayers)
        {
            if (_players.Contains(player)) 
            {
                player.SetPlayerName($"Player {index}");
                index++;
            }
            else player.gameObject.SetActive(false);
        }

        return _players.ToArray();
    }
}
