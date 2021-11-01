using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class CreateAccountWindow : MonoBehaviour
    {
        [SerializeField] private InputField InputField;
        [SerializeField] private Button ButtonOk;

        void Start()
        {
            InputField.onEndEdit.AddListener(OnEndEditInputField);
            ButtonOk.onClick.AddListener(OnClickButtonOk);
        }

        private void OnEndEditInputField(string value)
        {
            if (!string.IsNullOrWhiteSpace(InputField.text)) {
                ButtonOk.interactable = true;
                PlayerData.Instance.PlayerName = InputField.text;
                PlayerData.Instance.Save();
            }
        }

        private void OnClickButtonOk()
        {
            Destroy(gameObject);
        }

        void OnDestroy()
        {
            InputField.onEndEdit.RemoveListener(OnEndEditInputField);
            InputField = null;
            ButtonOk.onClick.RemoveListener(OnClickButtonOk);
            ButtonOk = null;
        }
    }
}
