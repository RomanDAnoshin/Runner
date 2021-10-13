using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBlockData : MonoBehaviour
    {
        [HideInInspector] public int BarricadeProbability
        {
            get {
                return barricadeProbability;
            }
        }
        [SerializeField, Range(0, 100)] private int barricadeProbability;

        [HideInInspector] public int CoinProbability
        {
            get {
                return coinProbability;
            }
        }
        [SerializeField, Range(0, 100)] private int coinProbability;

        [HideInInspector] public GameObject[] Coins
        {
            get {
                return coins;
            }
        }
        [SerializeField] private GameObject[] coins;

        [HideInInspector] public GameObject[] Barricades
        {
            get {
                return barricades;
            }
        }
        [SerializeField] private GameObject[] barricades;

        void OnDestroy()
        {
            foreach(var coin in coins) {
                Destroy(coin);
            }
            coins = null;
            foreach (var barricade in barricades) {
                Destroy(barricade);
            }
            barricades = null;
        }
    }
}
