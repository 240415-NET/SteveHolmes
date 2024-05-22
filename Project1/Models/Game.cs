namespace NoughtsAndCrosses.Models;

public class Game
{
    public Guid gameId { get; set; }
    public Guid userId { get; set; }
    public char[] board { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public char winLossDraw { get; set; }

    public Game() {} 

    public Game(User user)
    {
        gameId = Guid.NewGuid();
        userId = user.userId;
        board = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
        startTime = MinimumSqlDateTime();
        endTime = MinimumSqlDateTime();
        winLossDraw = ' ';
    }

    public static DateTime MinimumSqlDateTime()
    {
        return DateTime.Parse("1/1/1753 12:00:00 AM");  // the earliest date supported by SQL Server.
    }

    public bool isInProgress()
    {
        return endTime == MinimumSqlDateTime();
    }

    public override string ToString()
    {
        string boardString = new string(board);
        return $"{gameId} {userId} {boardString} {startTime} {endTime} {winLossDraw}";
    }

    public bool WasGameWonBy(char playerLetter)
    {
        return  // horizonal
               board[0] == playerLetter && board[1] == playerLetter && board[2] == playerLetter
            || board[3] == playerLetter && board[4] == playerLetter && board[5] == playerLetter
            || board[6] == playerLetter && board[7] == playerLetter && board[8] == playerLetter

            // vertical
            || board[0] == playerLetter && board[3] == playerLetter && board[6] == playerLetter
            || board[1] == playerLetter && board[4] == playerLetter && board[7] == playerLetter
            || board[2] == playerLetter && board[5] == playerLetter && board[8] == playerLetter

            // diagonal
            || board[0] == playerLetter && board[4] == playerLetter && board[8] == playerLetter
            || board[6] == playerLetter && board[4] == playerLetter && board[2] == playerLetter;
    }

    public bool DidUserWin()
    {
        return WasGameWonBy('X');
    }

    public bool DidUserLose()
    {
        return WasGameWonBy('O');
    }

    public bool DidUserDraw()
    {
        return FirstAvailablePosition() < 0 && !DidUserWin() && !DidUserLose();
    }

    public int FirstAvailablePosition()
    {
        for (int i=0; i<=8; i++)
            if (IsPositionAvailable(i))
                return i;
        return -1;
    }

    public bool IsPositionAvailable(int n)
    {
        return Char.IsDigit(board[n]);
    }

    public void UserSelectsPosition(int n)
    {
        board[n] = 'X';
    }

    public void SystemSelectsPosition(int n)
    {
        board[n] = 'O';
    }

    public void RecordUserWin()
    {
        winLossDraw = 'W';
    }

    public void RecordUserLoss()
    {
        winLossDraw = 'L';
    }

    public void RecordUserDraw()
    {
        winLossDraw = 'D';
    }
}