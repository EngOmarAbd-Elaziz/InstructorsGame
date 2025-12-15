using UnityEngine;

public class AbilitesSO : ScriptableObject
{
    public string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent , Transform target = null) { }
}
