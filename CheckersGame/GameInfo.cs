using static CheckersGameLogic.GameLogic;
using CheckersGameLogic;

namespace CheckersGameForms
{
    class GameInfo
    {
        public GameInfo(string i_Player1Name, string i_Player2Name, int gameSize, eGameType i_GameType)
        {
            Player1Name = i_Player1Name;
            Player2Name = i_Player2Name;
            BoardSize = gameSize;
            GameType = i_GameType;
        }

        public eGameType GameType { get; }
        public eGameTurn GameTurn { get; private set; }
        public eGameStatus GameStatus { get; private set; }
        public int BoardSize { get; }
        public string Player1Name { get; }
        public string Player2Name { get; }
        public int Player1Score { get; private set; }
        public int Player2Score { get; private set; }
        public int Player1TotalScore { get; private set; }
        public int Player2TotalScore { get; private set; }


        public void ResetGame(GameLogic i_GameLogic)
        {
            Player1Score = Player1TotalScore + i_GameLogic.GetPlayerScore(1);
            Player2Score = Player2TotalScore + i_GameLogic.GetPlayerScore(2);
            GameStatus = eGameStatus.Playing;
            GameTurn = eGameTurn.Player1;
        }

        public void SwitchPlayerTurn(GameLogic i_GameLogic, bool i_HaveEatMove)
        {
            if (!i_HaveEatMove)
            {
                if (GameTurn == eGameTurn.Player1)
                {
                    GameTurn = eGameTurn.Player2;
                }
                else
                {
                    GameTurn = eGameTurn.Player1;
                }
            }
            Player1Score = Player1TotalScore + i_GameLogic.GetPlayerScore(1);
            Player2Score = Player2TotalScore + i_GameLogic.GetPlayerScore(2);
        }

        public void CheckGameStatus(GameLogic i_GameLogic)
        {
            GameStatus = i_GameLogic.GetGameStatus();
            if (GameStatus != eGameStatus.Playing)
            {
                Player1TotalScore += i_GameLogic.GetPlayerScore(1);
                Player2TotalScore += i_GameLogic.GetPlayerScore(2);
            }
        }

    }
}