using System;
using System.Collections;
using System.Collections.Generic;
using FlappyBird.Managers;
using FlappyBird.Utils;
using UnityEngine;

namespace FlappyBird.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Camera mainCamera;

        [Header("View Components")] 
        [SerializeField] private Rigidbody playerRigidbody;

        private void Start()
        {
            mainCamera = GameManager.Instance.MainCamera;
        }

        void Update()
        {
            UpdatePlayerAndCameraPosition();
            HandlePlayerInput();
        }

        private void UpdatePlayerAndCameraPosition()
        {
            Vector3 movementVector = Vector3.right * ConstantsHelper.BASE_MOVEMENT_SPEED * Time.deltaTime;
            transform.position += movementVector;
            mainCamera.transform.position += movementVector;
        }

        private void HandlePlayerInput()
        {
            // works for both mobile and PC devices
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.CanAddInputFromPlayer)
            {
                playerRigidbody.AddForce(Vector3.up * ConstantsHelper.BASE_IMPULSE_MAGNITUDE, ForceMode.Impulse);
            }
        }
    }
}