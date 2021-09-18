using UnityEngine;
using SimpleJSON;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public string PlayerName;
    public int Coins;
    public int Distance;

    public int CurrentDistance;

    public void Save()
    {
        var jsonObject = ToJSONObject();
        PlayerPrefs.SetString("PlayerData", jsonObject.ToString());
    }

    public bool HasSaved()
    {
        return PlayerPrefs.HasKey("PlayerData");
    }

    public void Load()
    {
        var data = PlayerPrefs.GetString("PlayerData");
        var jsonObject = JSON.Parse(data);
        PlayerName = jsonObject["PlayerName"].Value;
        Coins = int.Parse(jsonObject["Coins"].Value);
        Distance = int.Parse(jsonObject["Distance"].Value);
        CurrentDistance = 0;
    }

    public void Reset()
    {
        PlayerName = "";
        Coins = 0;
        Distance = 0;
        CurrentDistance = 0;

        Save();
    }

    private JSONNode ToJSONObject()
    {
        var result = new JSONObject();
        result.Add("PlayerName", PlayerName);
        result.Add("Coins", Coins);
        Distance += CurrentDistance;
        result.Add("Distance", Distance);
        return result;
    }
}
