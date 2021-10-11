using UnityEngine;

namespace Road
{
    public class RoadBlockGenerator : MonoBehaviour
    {
        [SerializeField] private RoadBlockData RoadBlockData;

        void Start()
        {
            GenerateCoins();
            GenerateBarricades();
        }

        private void GenerateCoins()
        {
            if (Random.Range(0, 101) > RoadBlockData.CoinProbability) {
                for (var i = 0; i < RoadBlockData.Coins.Length; i++) {
                    Destroy(RoadBlockData.Coins[i]);
                }
            }
        }

        private void GenerateBarricades()
        {
            if (Random.Range(0, 101) > RoadBlockData.BarricadeProbability) {
                for (var i = 0; i < RoadBlockData.Barricades.Length; i++) {
                    Destroy(RoadBlockData.Barricades[i]);
                }
            }
        }
    }
}
