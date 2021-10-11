using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUI.MainMenu
{
    public class CreateAccountWindow : MonoBehaviour
    {
        [SerializeField] private PlayerData PlayerData;
        [SerializeField] private InputField InputField;

        public void OnEndEditInputField(string value)
        {
            PlayerData.PlayerName = InputField.text;
            PlayerData.Save();
        }

        public void OnClickButtonOk()
        {
            gameObject.GetComponentInParent<PlayerInfo>()?.Refresh();
            Destroy(gameObject);
        }
    }
}
