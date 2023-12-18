using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    DatabaseReference reference;

    public void OnStart()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            //FirebaseApp app = FirebaseApp.DefaultInstance;
            reference = FirebaseDatabase.DefaultInstance.RootReference;

            //reference.Child("Player").OrderByChild("score").ValueChanged += HandleScoresChanged;
        });
    }
    public void SavePlayerScore(string playerName, int score)
    {
        PlayerScoreData playerScore = new PlayerScoreData
        {
            playerName = playerName,
            score = score
        };

        string json = JsonUtility.ToJson(playerScore);
        reference.Child("Player").Push().SetRawJsonValueAsync(json);
    }
    void HandleScoresChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        // Parse and use the retrieved scores
        foreach (var childSnapshot in args.Snapshot.Children)
        {
            string json = childSnapshot.GetRawJsonValue();
            PlayerScoreData playerScore = JsonUtility.FromJson<PlayerScoreData>(json);

            Debug.Log($"Player: {playerScore.playerName}, Score: {playerScore.score}");
        }
    }
    public void SetScoreData(string userid, int _score)
    {
        Debug.Log("UserId === " + userid + "Score = " + _score);
        Task DBTask = reference.Child("Score").Child(userid).SetValueAsync(_score);
    }
}
