using System;
using System.Collections.Generic;
using FlappyBird.Blocker;
using FlappyBird.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBird.Managers
{
    public class LevelGenerator : MonoSingleton<LevelGenerator>
    {
        [Header("Attributes")] 
        [SerializeField] private float minDistanceBetweenBlockers;

        [SerializeField] private float maxDistanceBetweenBlockers;
        [SerializeField] private float minBlockerHeight;
        [SerializeField] private float maxBlockerHeight;


        private Camera mainCamera;
        private float pointOutsideCameraFrustum;
        private Vector3 previousBlockerPosition;
        private Vector3 previousPlatformPosition;

        public Plane[] CameraFrustumPlanes;

        private void Start()
        {
            mainCamera = GameManager.Instance.MainCamera;
            CameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

            float aspect = (float)Screen.width / Screen.height;
            pointOutsideCameraFrustum = mainCamera.orthographicSize * aspect;
            previousBlockerPosition = Vector3.right * (pointOutsideCameraFrustum - 3);

            GenerateInitialBlockers();
            GenerateInitialPlatforms();
        }

        private void Update()
        {
            // update frustum planes as the camera is moving every frame
            CameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        }

        private void GenerateInitialBlockers()
        {
            // generate a predefined number of blockers to populate the level
            for (int i = 0; i < ConstantsHelper.NUMBER_OF_INITIAL_BLOCKERS; i++)
            {
                GenerateNextBlocker();
            }
        }

        private void GenerateInitialPlatforms()
        {
            // generate a predefined number of Platforms
            for (int i = 0; i < ConstantsHelper.NUMBER_OF_INITIAL_PLATFORMS; i++)
            {
                GenerateNextPlatform();
            }
        }

        private void GenerateNextBlocker(BlockerView previousBlockerView = null)
        {
            // return previous blocker to the pool
            if (previousBlockerView)
                PoolManager.Instance.ReturnBlockerViewToPool(previousBlockerView);

            // get new blocker from the pool
            BlockerView blockerView = PoolManager.Instance.BlockerPool.Dequeue();

            // assign deligate to init a new blocker from the pool once this blocker is out of screen frustum
            blockerView.SetOnBlockerOutOfCameraFrustumAction(GenerateNextBlocker);

            // assign deligate to add points when the blocked is cleared
            blockerView.SetOnBlockerClearedAction(() => GameManager.Instance.AddPoint());

            InitBlockerPlacement(blockerView.gameObject);
        }

        private void GenerateNextPlatform(BlockerView previousPlatformView = null)
        {
            // return previous blocker to the pool
            if (previousPlatformView)
                PoolManager.Instance.ReturnPlatformViewToPool(previousPlatformView);

            // get new platform from the pool
            BlockerView blockerView = PoolManager.Instance.PlatformPool.Dequeue();

            // assign deligate to init a new blocker from the pool once this blocker is out of screen frustum
            blockerView.SetOnBlockerOutOfCameraFrustumAction(GenerateNextPlatform);

            InitPlatformPlacement(blockerView);
        }

        private void InitBlockerPlacement(GameObject blockerView)
        {
            // update both horizontal and vertical placement with some rng
            blockerView.transform.position =
                previousBlockerPosition + Vector3.up * Random.Range(minBlockerHeight, maxBlockerHeight);

            // update the cached position
            previousBlockerPosition +=
                Vector3.right * Random.Range(minDistanceBetweenBlockers, maxDistanceBetweenBlockers);
            blockerView.SetActive(true);
        }

        private void InitPlatformPlacement(BlockerView blockerView)
        {
            if (previousPlatformPosition != default)
                blockerView.transform.position = previousPlatformPosition;
            else
                previousPlatformPosition = blockerView.transform.position;

            blockerView.gameObject.SetActive(true);

            Vector3 boundsSize = blockerView.GetColliderBoundsSize();
            // update the cached position
            previousPlatformPosition += new Vector3(boundsSize.x, 0, 0);
        }
    }
}