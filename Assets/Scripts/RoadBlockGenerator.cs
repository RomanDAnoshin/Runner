using UnityEngine;

public class RoadBlockGenerator : MonoBehaviour
{
    [Range(0, 100)]
    public int CoinProbability;
    public GameObject[] Coins;

    [Range(0, 100)]
    public int BarricadeProbability;
    public GameObject[] Barricades;

    void Start()
    {
        GenerateCoins();
        GenerateBarricades();
    }

    private void GenerateCoins()
    {
        if (Random.Range(0, 101) > CoinProbability) {
            for (var i = 0; i < Coins.Length; i++) {
                Destroy(Coins[i]);
            }
        }
    }

    private void GenerateBarricades()
    {
        if (Random.Range(0, 101) > BarricadeProbability) {
            for (var i = 0; i < Barricades.Length; i++) {
                Destroy(Barricades[i]);
            }
        }
    }
}
