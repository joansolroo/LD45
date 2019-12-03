using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Equipment
{
    public class ConsumableData : InventoryData
    {
        public enum Type
        {
            None, HP, Energy, Ammo, Data
        }
        public Type pickableType;

        public AudioClip audioclip;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Inventory/" + "ConsumableData", priority = 1)]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<ConsumableData>();
        }
#endif
    }
}
