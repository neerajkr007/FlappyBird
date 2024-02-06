using System;
using System.Collections.Generic;
using FlappyBird.Blocker;
using UnityEngine;

namespace FlappyBird.Utils
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [Header("Attributes")] 
        [SerializeField] private int blockerPoolSize;
        [SerializeField] private int platformPoolSize;

        [Header("Asset Refs")] 
        [SerializeField] private GameObject blockerPrefab;
        [SerializeField] private Transform blockerParent;
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private Transform platformParent;

        private Queue<BlockerView> blockerPool;
        private Queue<BlockerView> platformPool;
        
        public Queue<BlockerView> BlockerPool
        {
            get { return blockerPool; }
            private set { blockerPool = value; }
        }
        public Queue<BlockerView> PlatformPool
        {
            get { return platformPool; }
            private set { platformPool = value; }
        }

        protected override void Awake()
        {
            base.Awake();

            blockerPool = new Queue<BlockerView>();
            platformPool = new Queue<BlockerView>();
            
            GameObject pooledGO;
            
            // populate the pool and queue for ref
            for (int i = 0; i < blockerPoolSize; i++)
            {
                pooledGO = Instantiate(blockerPrefab, blockerParent);
                pooledGO.SetActive(false);
                blockerPool.Enqueue(pooledGO.GetComponent<BlockerView>());
            }
            
            for (int i = 0; i < platformPoolSize; i++)
            {
                pooledGO = Instantiate(platformPrefab, platformParent);
                pooledGO.SetActive(false);
                platformPool.Enqueue(pooledGO.GetComponent<BlockerView>());
            }
        }

        public void ReturnBlockerViewToPool(BlockerView previousBlockerView)
        {
            blockerPool.Enqueue(previousBlockerView);
            previousBlockerView.gameObject.SetActive(false);
        }

        public void ReturnPlatformViewToPool(BlockerView previousPlatformView)
        {
            platformPool.Enqueue(previousPlatformView);
            previousPlatformView.gameObject.SetActive(false);
        }
    }
}