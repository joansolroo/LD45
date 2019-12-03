using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    [RequireComponent(typeof(RangeWeapon))]
    public class WeaponAudio : InventoryAudio<RangeWeaponData>
    {
        [SerializeField] RangeWeapon weapon;
        // Start is called before the first frame update
        protected override void Reset()
        {
            weapon = GetComponent<RangeWeapon>();
            base.Reset();
        }

        private void OnEnable()
        {
            Reset();
            weapon.OnShotTick += OnShot;
            weapon.OnReloadStart += OnReloadStart;
            weapon.OnReloadEnd += OnReloadEnd;
            weapon.OnReloadTick += OnReloadTick;
        }

        private void OnDisable()
        {
            weapon.OnShotTick -= OnShot;
            weapon.OnReloadStart -= OnReloadStart;
            weapon.OnReloadEnd -= OnReloadEnd;
            weapon.OnReloadTick -= OnReloadTick;
        }

        void OnShot()
        {
            audioSource.loop = false;
            audioSource.volume = 1;
            audioSource.PlayOneShot(weapon.data.clipFire);
        }
        void OnReloadStart()
        {
            if (!weapon.overheat)
            {
                audioSource.clip = weapon.data.clipReload;
            }
            else
            {
                audioSource.clip = weapon.data.clipEmpty;
            }
            audioSource.loop = true;
            audioSource.Play();
        }
        void OnReloadTick()
        {
            audioSource.volume = Mathf.Min(5 * (1.0f - (float)weapon.load / weapon.data.capacity), 1.0f);
        }
        void OnReloadEnd()
        {
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.volume = 1;
        }
    }
}
