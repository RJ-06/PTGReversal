using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Spawnable
{
    public void Spawn();
}

public abstract class ProjectileBase : MonoBehaviour
{

}

public abstract class ProjectileDescriptor<T> : ScriptableObject, Spawnable where T : ProjectileBase
{
    public List<ProjectileModifier<T>> modifiers;
    public abstract void Spawn();
}

public abstract class ProjectileGroup : ScriptableObject, Spawnable
{
    public abstract void Spawn();
}

public abstract class ProjectileModifier<T> : ScriptableObject where T : ProjectileBase
{
    public abstract void Modify(T projectile);
}