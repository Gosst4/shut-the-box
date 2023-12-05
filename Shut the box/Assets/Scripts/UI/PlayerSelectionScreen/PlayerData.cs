
public class PlayerData 
{
    public string _name {  get; }
    public PlayerType _playerType { get; }

    public PlayerData(string name, PlayerType playerType)
    {
        _name = name;   
        _playerType = playerType;
    }
}
