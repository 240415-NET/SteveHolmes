namespace NoughtsAndCrosses.Models;

public class Game
{

    public Guid gameId {get; set;}
    public Guid userId {get; set;}
    public char[] board {get; set;} 
    public DateTime startTime {get; set;}
    public DateTime endTime {get; set;}
    public char winLossDraw {get; set;}

    public Game () {}

    public Game(User user)
    {
        gameId = Guid.NewGuid(); 
        userId = user.userId;
        board = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        startTime = DateTime.MinValue;   
        endTime = DateTime.MinValue;
        winLossDraw = '?';
    }

    // public Game(char[] _board, bool _isInProgress, DateTime _startTime)
    // {
    //     gameId = Guid.NewGuid(); 
    //     board = _board;
    //     startTime = _startTime;      
    //     endTime = DateTime.MinValue;
    //     winLossDraw = 'X';
    // }   

    public bool isInProgress()
    {
        return endTime == DateTime.MinValue;
    }

    public override string ToString()
    {
        return $"{startTime} {endTime} {winLossDraw}";
    }

}