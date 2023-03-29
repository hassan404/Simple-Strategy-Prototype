using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleRTS.Helpers {

    public class ObjectPooler : MonoBehaviour
    {
        /// <summary>
        /// Static reference to the instance, not accessible to other classes directly.
        /// </summary>
        private static ObjectPooler instance;

        internal class ObjectPoolList
        {
            public GameObject preset;
            public int initialCount;
            public List<GameObject> pooledObjects = new List<GameObject>();

        }

        [SerializeField]
        private List<ObjectPoolList> objectsToPool = new List<ObjectPoolList>();

        private void Awake()
        {
            if (!instance)
                instance = this;
        }
        /// <summary>
        /// returns an instance from the object pool if available, otherwise uses GameObject.Instantiate to return an instance.
        /// </summary>
        /// <param name="preset"> GameObject to clone the instance from</param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject preset, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            GameObject outObject;
            if (instance)
            {
                ObjectPoolList objectPoolList = instance.FindPoolListByPreset(preset);
                outObject = instance.GetInstanceFromPool(objectPoolList);
                outObject.transform.SetPositionAndRotation(position, rotation);
                outObject.transform.parent = parent;
                outObject.SetActive(true);
                return outObject;
            }

            outObject = Instantiate(preset, position, rotation, parent);
            return outObject;
        }
        public static bool Deinstantiate(GameObject instantiation)
        {
            ObjectPoolList objectPoolList = instance.FindPoolListByPreset(instantiation);
            if (objectPoolList is not null)
            {
                objectPoolList.pooledObjects.Add(instantiation);
            }
            return false;
        }

        private GameObject GetInstanceFromPool(ObjectPoolList poolList)
        {
            if (poolList is not null)
            {
                foreach (GameObject g in poolList.pooledObjects)
                {
                    if (g)
                        return g;
                    else
                        poolList.pooledObjects.Remove(g);
                }
                return CreateNewInstance(poolList);
            }
            return null;
        }

        private void ReturnInstanceToPool(ObjectPoolList poolList)
        {

        }

        private GameObject CreateNewInstance(ObjectPoolList poolList)
        {
            GameObject go = Instantiate(poolList.preset, transform);
            go.SetActive(false);

            poolList.pooledObjects.Add(go);

            return go;
        }

        private ObjectPoolList FindPoolListByPreset(GameObject preset)
        {
            foreach (ObjectPoolList poolList in objectsToPool)
            {
                if (preset.name == poolList.preset.name)
                    return poolList;
            }
            return null;
        }

    }
}