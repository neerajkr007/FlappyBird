using UnityEngine;

namespace FlappyBird.Utils
{
    // class that can be used to easily create monobehaviour singletons 
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        [SerializeField] private bool dontDestroyOnLoad;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
                Instance = this as T;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(this);
        }
    }
}