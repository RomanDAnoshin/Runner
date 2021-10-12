using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class CreateAccountWindow : MonoBehaviour
    {
        private PlayerData playerData;
        [SerializeField] private InputField InputField;
        [SerializeField] private Button ButtonOk;

        void Start()
        {
            playerData = FindObjectOfType<PlayerData>();
            InputField.onEndEdit.AddListener(OnEndEditInputField);
            ButtonOk.onClick.AddListener(OnClickButtonOk);
        }

        private void OnEndEditInputField(string value)
        {
            if (!string.IsNullOrWhiteSpace(InputField.text)) {
                ButtonOk.interactable = true;
                playerData.PlayerName = InputField.text;
                playerData.Save();
            }
        }

        private void OnClickButtonOk()
        {
            Destroy(gameObject);
        }
    }
}
