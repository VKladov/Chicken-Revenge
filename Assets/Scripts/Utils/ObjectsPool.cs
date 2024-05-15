using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Scripts
{
    public class ObjectsPool<T> where T : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<T>> _pool = new();
        private readonly Dictionary<int, int> _instaceToPrefabId = new();
        private readonly DiContainer _diContainer;

        public ObjectsPool(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Get(T prefab)
        {
            var prefabId = prefab.GetInstanceID();
            if (_pool.TryGetValue(prefabId, out var list) && list.Count > 0)
            {
                var instanceFromPool = list.Dequeue();
                instanceFromPool.gameObject.SetActive(true);
                return instanceFromPool;
            }

            var instance = _diContainer.InstantiatePrefab(prefab).GetComponent<T>();
            _instaceToPrefabId.Add(instance.GetInstanceID(), prefabId);
            return instance;
        }

        public void Return(T instance)
        {
            var instanceId = instance.GetInstanceID();
            if (!_instaceToPrefabId.TryGetValue(instanceId, out var prefabId))
            {
                throw new Exception("Trying to return unknown object");
            }
            
            instance.gameObject.SetActive(false);
            if (_pool.TryGetValue(prefabId, out var list))
            {
                list.Enqueue(instance);
            }
            else
            {
                var newList = new Queue<T>();
                newList.Enqueue(instance);
                _pool.Add(prefabId, newList);
            }
        }
    }
}