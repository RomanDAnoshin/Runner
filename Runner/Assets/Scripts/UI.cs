using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Transform HeroTransform;
    public Text Text;

    void Update()
    {
        Text.text = "Distance: " + ((int)HeroTransform.position.z).ToString();
    }
}
