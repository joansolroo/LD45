using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment {
    public interface IDroppable
    {
        bool Drop();
    }
    public interface IPickable
    {
        bool Pick();
    }
    public interface IUsable
    {
        bool Use();
    }
    public abstract class InventoryElementController<T> : MonoBehaviour where T : InventoryData
    {
        public enum InventoryElementType
        {
            Weapon_primary, Weapon_secondary, Passive 
        }
        public InventoryElementType elementType;
        public T data;
    }
}