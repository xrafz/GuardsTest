using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public abstract void Init(MonoBehaviour mono);
    public abstract void Action();
}
