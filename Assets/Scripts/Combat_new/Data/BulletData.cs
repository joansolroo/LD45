using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Equipment
{
    public class BulletData : InventoryData
    {
        public int velocity = 1;
        public int damage = 1;
        public float range = 10;
        public LayerMask target;
        public LayerMask ignore;

        public float radius = 0;

        public bool guided = false;
        public bool nuke = false;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Inventory/" + "BulletData", priority = 1)]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<BulletData>();
        }
#endif
    }

}