using FlappyBird.Managers;
using FlappyBird.Utils;
using UnityEngine;

namespace FlappyBird.Player
{
    public class PlayerView : MonoBehaviour
    {
        #region Collision Detection

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(ConstantsHelper.BLOCKER_TAG))
                GameManager.Instance.GameOver();
        }

        #endregion
    }
}