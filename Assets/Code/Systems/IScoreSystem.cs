public interface IScoreSystem
{
    void Init();
    void SubtractLevelScore();
    void ResetLevelScore();
    UserData GetUserData();
    int GetMinimumScoreTopTen();
    int GetHighScore();
    void SaveUserData(string[] playerNames, int[] bestScores);
    int GlobalScore { get; }
    int LevelScore { get; }
}
