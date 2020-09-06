using UnityEngine;
using SimpleJSON;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public string PlayerName;
    public int Coins;
    public float Distance;

    public float CurrentDistance;

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
        Distance = float.Parse(jsonObject["Distance"].Value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        CurrentDistance = 0f;
    }

    public void Reset()
    {
        PlayerName = "";
        Coins = 0;
        Distance = 0f;
        CurrentDistance = 0f;

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
