using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class GameHelper
{
    public static Player[] GetPlayers(List<PlayerData> datas, PlayerSetup[] _allPlayerSetups)
    {
        List<Player> _players = new List<Player>();
        int number = datas.Count;

        if (number == 2)
        {
            _players.Add(CreatePlayer(datas[0], _allPlayerSetups[0]));
            _players.Add(CreatePlayer(datas[1], _allPlayerSetups[3]));
        }
        else if (number == 3) 
        {
            _players.Add(CreatePlayer(datas[0], _allPlayerSetups[0]));
            _players.Add(CreatePlayer(datas[1], _allPlayerSetups[2]));
            _players.Add(CreatePlayer(datas[2], _allPlayerSetups[4]));
        }
        else if ( number == 4)
        {
            _players.Add(CreatePlayer(datas[0], _allPlayerSetups[0]));
            _players.Add(CreatePlayer(datas[1], _allPlayerSetups[1]));
            _players.Add(CreatePlayer(datas[2], _allPlayerSetups[3]));
            _players.Add(CreatePlayer(datas[3], _allPlayerSetups[4]));
        }
        else 
        {
            for (int i = 0; i < number; i++)
            {
                _players.Add(CreatePlayer(datas[i], _allPlayerSetups[i]));
            }
        }

        foreach (PlayerSetup setup in _allPlayerSetups)
        {
            if (!_players.Contains(setup.GetComponent<Player>())) 
            {
                setup.gameObject.SetActive(false);
            }
        }
        return _players.ToArray();
    }

    private static Player CreatePlayer(PlayerData playerData, PlayerSetup playerSetup)
    {
        PlayerType playerType = playerData._playerType;

        Player player = playerType switch
        {
            PlayerType.Human            => playerSetup.AddComponent<HumanPlayer>(),
            PlayerType.ComputerEasy     => playerSetup.AddComponent<ComputerEasy>(),
            PlayerType.ComputerNormal   => playerSetup.AddComponent<ComputerNormal>(),
            PlayerType.ComputerHard     => playerSetup.AddComponent<ComputerHard>(),
			_                           => playerSetup.AddComponent<HumanPlayer>()
		};

        player.SetPlayerName(playerData._name);
        return player;
    }
}
