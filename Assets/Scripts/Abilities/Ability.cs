using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public abstract void Init(MonoBehaviour mono);
    public abstract void Sub();
    public abstract void Unsub();
    public void Destroy()
    {
        Unsub();
        Destroy(this);
    }
    private void OnDestroy()
    {
        Unsub();
    }
}
