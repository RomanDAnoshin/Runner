using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Transform HeroTransform;
    public Text Text;

    void Start()
    {
        // TODO: get data in PlayerData
    }

    void Update()
    {
        Text.text = 
            "Coins: " + "\n" +
            "Distance: " + ((int)HeroTransform.position.z).ToString();
    }
}
