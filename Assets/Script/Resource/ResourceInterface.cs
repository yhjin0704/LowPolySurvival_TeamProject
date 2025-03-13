using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DropResource
{
    [System.Serializable]
    public class DropItem
    {
        public GameObject dropItemPrefab;
        public int minDropCount;
        public int maxDropCount;
    }

    interface IBreakableObject
    {
        public void TakeDamage(float _damage);
        void Break();
    }

    interface IRespawnable
    {
        IEnumerator CRespawnCoroutine(float _respawnTime);
    }

    public class ResourceInterface : MonoBehaviour
    {

    }
}
