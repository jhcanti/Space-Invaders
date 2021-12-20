public interface IScoreSystem
{
    void Init();
    void Reset();
    UserData GetUserData();
    void SaveUserData(string[] playerNames, int[] bestScores);
    int CurrentScore { get; }
}
