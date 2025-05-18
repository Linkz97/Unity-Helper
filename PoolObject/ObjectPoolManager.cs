using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LinkzJ.Games.Ultilities.PoolObject
{
    public class ObjectPoolManager : MonoBehaviour
    {
        // Depends on the situation to Use
        [SerializeField] private bool _addToDontDestoryOnLoad = false;

        private GameObject _emptyHolder;

        private static GameObject _particleSystemsEmpty;
        private static GameObject _gameObjectEmpty;
        private static GameObject _audioSourcesEmpty;

        private static Dictionary<GameObject, ObjectPool<GameObject>> _objectPools;
        private static Dictionary<GameObject, GameObject> _cloneToPrefabMap;

        public enum PoolType
        {
            ParticleSystem,
            GameObject,
            SoundFX
        }

        public static PoolType PoolingType;

        private void Awake()
        {
            _objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
            _cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

            SetupEmpties();
        }

        private void SetupEmpties()
        {
            _emptyHolder = new GameObject("Object Pools");
            
            _particleSystemsEmpty = new GameObject("Particle Effects");
            _particleSystemsEmpty.transform.SetParent(_emptyHolder.transform);
            
            _gameObjectEmpty = new GameObject("Game Objects");
            _gameObjectEmpty.transform.SetParent(_emptyHolder.transform);
            
            _audioSourcesEmpty = new GameObject("Sound Effect");
            _audioSourcesEmpty.transform.SetParent(_emptyHolder.transform);

            if (_addToDontDestoryOnLoad)
                DontDestroyOnLoad(_particleSystemsEmpty.transform.root);
        }

        private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(prefab, pos, rot, poolType),
                actionOnGet: OnGetObject,
                actionOnRelease: OnReleaseObject,
                actionOnDestroy: OnDestroyObject
            );
            
            _objectPools.Add(prefab, pool);;
        }

        private static void CreatePool(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () => CreateObject(prefab, parent, rot, poolType),
                actionOnGet: OnGetObject,
                actionOnRelease: OnReleaseObject,
                actionOnDestroy: OnDestroyObject
            );
            
            _objectPools.Add(prefab, pool);;
        }
        
        private static GameObject SetParentObject(PoolType poolType)
        {
            switch (poolType)
            {
                case PoolType.ParticleSystem:
                    return _particleSystemsEmpty;
                case PoolType.GameObject:
                    return _gameObjectEmpty;
                case PoolType.SoundFX:
                    return _audioSourcesEmpty;
                default: return null;
            }
        }
        
        private static GameObject CreateObject(GameObject prefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            // Set Object to Deactive First before Initialize it
            prefab.SetActive(false);
            
            GameObject obj = Instantiate(prefab, pos, rot);
            obj.SetActive(true);

            GameObject parentObj = SetParentObject(poolType);
            obj.transform.SetParent(parentObj.transform);
            
            return obj;
        }
        
        private static GameObject CreateObject(GameObject prefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            // Set Object to Deactive First before Initialize it
            prefab.SetActive(false);
            
            GameObject obj = Instantiate(prefab, parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = rot;
            obj.transform.localScale = Vector3.one;
            obj.SetActive(true);
            return obj;
        }
        
        private static void OnGetObject(GameObject obj)
        {
            // Optional Logic Here
        }

        private static void OnReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        private static void OnDestroyObject(GameObject obj)
        {
            if (_cloneToPrefabMap.ContainsKey(obj))
            {
                _cloneToPrefabMap.Remove(obj);
            }
        }

        private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject) where T : Object
        {
            if (!_objectPools.ContainsKey(objectToSpawn))
            {
                CreatePool(objectToSpawn, pos, rot, poolType);
            }

            GameObject obj = _objectPools[objectToSpawn].Get();

            if (obj != null)
            {
                if (!_cloneToPrefabMap.ContainsKey(obj))
                {
                    _cloneToPrefabMap.Add(obj, objectToSpawn);
                }

                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                {
                    return obj as T;
                }
                
                T component = obj.GetComponent<T>();
                if (component == null)
                {
                    UnityEngine.Debug.LogError($"Object {objectToSpawn.name} does not have {typeof(T).Name} component");
                    return null;
                }
                
                return component;
            }
            
            return null;
        }

        public static T SpawnObject<T>(T typePrefab, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject) where T : Component
        {
            return SpawnObject<T>(typePrefab.gameObject, pos, rot, poolType);
        }
        
        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            return SpawnObject<GameObject>(objectToSpawn, pos, rot, poolType);
        }

        
        private static T SpawnObject<T>(GameObject objectToSpawn, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObject) where T : Object
        {
            if (!_objectPools.ContainsKey(objectToSpawn))
            {
                CreatePool(objectToSpawn, parent, rot, poolType);
            }

            GameObject obj = _objectPools[objectToSpawn].Get();

            if (obj != null)
            {
                if (!_cloneToPrefabMap.ContainsKey(obj))
                {
                    _cloneToPrefabMap.Add(obj, objectToSpawn);
                }

                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.rotation = rot;
                obj.SetActive(true);

                if (typeof(T) == typeof(GameObject))
                {
                    return obj as T;
                }
                
                T component = obj.GetComponent<T>();
                if (component == null)
                {
                    UnityEngine.Debug.LogError($"Object {objectToSpawn.name} does not have {typeof(T).Name} component");
                    return null;
                }
                
                return component;
            }
            
            return null;
        }
        
        public static T SpawnObject<T>(T typePrefab, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObject) where T : Component
        {
            return SpawnObject<T>(typePrefab.gameObject, parent, rot, poolType);
        }
        
        public static GameObject SpawnObject(GameObject objectToSpawn, Transform parent, Quaternion rot, PoolType poolType = PoolType.GameObject)
        {
            return SpawnObject<GameObject>(objectToSpawn, parent, rot, poolType);
        }
        
        public static void ReturnObject(GameObject obj, PoolType poolType = PoolType.GameObject)
        {
            if (_cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
            {
                GameObject parentObject = SetParentObject(poolType);

                if (obj.transform.parent != parentObject.transform)
                {
                    obj.transform.SetParent(parentObject.transform);
                }
            }

            if (_objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
    }
}