﻿using Character;
using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI.Road
{
    public class GameGUI : MonoBehaviour
    {
        [SerializeField] private GameObject StartMessage;
        [SerializeField] private GameObject DeathWindow;
        private CharacterBodyCollision characterBodyCollision;
        private PlayerInput playerInput;

        void Start()
        {
            playerInput = FindObjectOfType<PlayerInput>();
            playerInput.PlayerActed += OnPlayerActed;
            characterBodyCollision = FindObjectOfType<CharacterBodyCollision>();
            characterBodyCollision.CollisionBarricade += OnCharacterCollisionBarricade;
        }

        private void OnPlayerActed()
        {
            if (playerInput.Value == PlayerInput.PlayerActions.Run) {
                Destroy(StartMessage);
            }
        }

        private void OnCharacterCollisionBarricade()
        {
            StartCoroutine("OpenDeathWindow");
        }

        private IEnumerator OpenDeathWindow()
        {
            yield return new WaitForSeconds(4);
            Instantiate(DeathWindow, transform);
        }

        void OnDestroy()
        {
            playerInput.PlayerActed -= OnPlayerActed;
            playerInput = null;
            characterBodyCollision.CollisionBarricade -= OnCharacterCollisionBarricade;
            characterBodyCollision = null;
            StartMessage = null;
            DeathWindow = null;
        }
    }
}
