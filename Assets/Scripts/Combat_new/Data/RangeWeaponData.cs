using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Equipment
{
    public class RangeWeaponData : InventoryData
    {
        public enum Type
        {
            Minigun,
            Plasma,
            Autogun
        };
        public Type weaponType;

        #region weapon
        [SerializeField] public int capacity;
        [SerializeField] public float cooldown = 0;
        [SerializeField] public float loadTime;
        [SerializeField] public int cost = 1;
        [SerializeField] public int bulletsPerShot = 1;
        [SerializeField] public float spread = 0;
        [SerializeField] public bool reloadInterruptSupported = false;
        #endregion
        #region Sound
        [SerializeField] public AudioClip clipFire;
        [SerializeField] public AudioClip clipReload;
        [SerializeField] public AudioClip clipEmpty;
        #endregion
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Inventory/"+"Range Weapon",priority =1)]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<RangeWeaponData>();
        }
#endif
    }
}
