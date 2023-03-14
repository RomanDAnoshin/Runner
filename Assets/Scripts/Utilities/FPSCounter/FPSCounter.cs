using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.FPSCounter
{
    public class FPSCounter : MonoBehaviour
    {
        public int FPS { get; protected set; }

        void Update()
        {
            FPS = (int)(1f / Time.unscaledDeltaTime);
        }
    }
}
