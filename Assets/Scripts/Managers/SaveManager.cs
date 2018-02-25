using UnityEngine;

public class SaveManager : SingletonMB<SaveManager>
{
    #region Best score

    public void ReplaceBestIfNeeded(int _Score)
    {
        if (_Score > GetBestScore())
            PlayerPrefs.SetInt(Constants.c_BestScoreSave, _Score);
    }

    public int GetBestScore()
    {
        if (PlayerPrefs.HasKey(Constants.c_BestScoreSave))
            return PlayerPrefs.GetInt(Constants.c_BestScoreSave);
        else
            return 0;
    }

    #endregion
}
