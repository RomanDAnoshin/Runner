using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class TopRightAutoAlign : MonoBehaviour
    {
        [SerializeField] private RectTransform CanvasTransform;
        [SerializeField] private float Offset = 10f;

        private RectTransform rectTransform;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            if (rectTransform != null && CanvasTransform != null) {

                rectTransform.localPosition = new Vector3(
                    CanvasTransform.rect.width / 2f - rectTransform.rect.width / 2f - Offset,
                    CanvasTransform.rect.height / 2f - rectTransform.rect.height / 2f - Offset,
                    0f
                );
            }
        }
    }
}
