using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class RoadBlock : MonoBehaviour
    {
        [HideInInspector] public int Difficult
        {
            get {
                return difficult;
            }
        }
        [SerializeField, Range(0, 100)] private int difficult;

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
