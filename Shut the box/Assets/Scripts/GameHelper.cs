using System.Collections.Generic;
using Unity.VisualScripting;

public static class GameHelper
{
    public static Player[] GetPlayers(int number, PlayerSetup[] _allPlayerSetups)
    {
        List<Player> _players = new List<Player>();

        if (number == 2)
        {
            Player player = _allPlayerSetups[0].AddComponent<HumanPlayer>();
            _players.Add(player);
            Player player2 = _allPlayerSetups[3].AddComponent<HumanPlayer>();
            _players.Add(player2);
        }
        else if (number == 3) 
        {
            Player player = _allPlayerSetups[0].AddComponent<HumanPlayer>();
            _players.Add(player);
            Player player2 = _allPlayerSetups[2].AddComponent<ComputerPlayer>();
            _players.Add(player2);
            Player player3 = _allPlayerSetups[4].AddComponent<ComputerPlayer>();
            _players.Add(player3);
        }
        else if ( number == 4)
        {
            //_players.Add(_allPlayerSetups[0]);
            //_players.Add(_allPlayerSetups[1]);
            //_players.Add(_allPlayerSetups[3]);
            //_players.Add(_allPlayerSetups[4]);
        }
        else 
        {
            for (int i = 0; i < number; i++)
            {
                //_players.Add(_allPlayerSetups[i]);
            }
        }

        int index = 1;
        foreach (PlayerSetup setup in _allPlayerSetups)
        {
            if (_players.Contains(setup.GetComponent<Player>())) 
            {
                setup.GetComponent<Player>().SetPlayerName($"Player {index}");
                index++;
            }
            else setup.gameObject.SetActive(false);
        }

        return _players.ToArray();
    }
}
