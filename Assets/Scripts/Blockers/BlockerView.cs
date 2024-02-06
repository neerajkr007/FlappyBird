using System;
using FlappyBird.Managers;
using UnityEngine;

namespace FlappyBird.Blocker
{
    public class BlockerView : MonoBehaviour
    {
        [SerializeField] private Collider collider;

        private Bounds colliderBounds;

        private Action<BlockerView> onBlockerOutOfCameraFrustum;
        private Action onBlockerCleared;

        private bool ifEnteredScreen = false;
        private bool ifPointCredited = false;

        private void OnEnable()
        {
            // reset flag in case of gameobject being fetched again from pool
            ifPointCredited = false;
        }

        private void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                // update bounds
                colliderBounds = collider.bounds;

                CheckIfOutOfBounds();

                // check if the blocker is cleared without the game being over
                if (!ifPointCredited &&
                    transform.position.x < GameManager.Instance.MainCamera.transform.position.x - 0.5f)
                {
                    ifPointCredited = true;
                    onBlockerCleared?.Invoke();
                }
            }
        }

        private void CheckIfOutOfBounds()
        {
            if (!GeometryUtility.TestPlanesAABB(LevelGenerator.Instance.CameraFrustumPlanes, colliderBounds))
            {
                if (ifEnteredScreen)
                    onBlockerOutOfCameraFrustum?.Invoke(this);
                ifEnteredScreen = false;
            }
            else
                ifEnteredScreen = true;
        }

        #region Getters

        public Vector3 GetColliderBoundsSize()
        {
            return collider.bounds.size;
        }

        #endregion

        #region Setters

        public void SetOnBlockerOutOfCameraFrustumAction(Action<BlockerView> onBlockerOutOfCameraFrustum)
        {
            this.onBlockerOutOfCameraFrustum = onBlockerOutOfCameraFrustum;
        }

        public void SetOnBlockerClearedAction(Action onBlockerCleared)
        {
            this.onBlockerCleared = onBlockerCleared;
        }

        #endregion
    }
}