public interface IScoreSystem
{
    void Init();
    void SubtractLevelScore();
    void ResetLevelScore();
    UserData GetUserData();
    int GetMinimumScoreTopTen();
    int GetHighScore();
    void UpdateBestScores(string playerName, int newScore);
    int GlobalScore { get; }
    int LevelScore { get; }
}
