using UnityEngine;

public abstract class ItemAction : ScriptableObject
{
    public abstract void Init(MonoBehaviour mono, int value);
}
