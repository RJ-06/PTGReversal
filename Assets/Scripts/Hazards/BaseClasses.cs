using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class Spawnable : ScriptableObject
{
    public abstract void Spawn();
}

public abstract class ProjectileBase : MonoBehaviour
{

}

public abstract class ProjectileDescriptor<T> : Spawnable where T : ProjectileBase
{
    public List<ProjectileModifier<T>> modifiers;
}

public abstract class ProjectileGroup : PlayableAsset
{
    public abstract void Spawn();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<GroupBehaviour>.Create(graph);
        playable.GetBehaviour().OnSpawn = Spawn;
        return playable;
    }
}

public class GroupBehaviour : PlayableBehaviour
{
    public Action OnSpawn;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        OnSpawn();
    }
}

public abstract class ProjectileModifier<T> : ScriptableObject where T : ProjectileBase
{
    public abstract void Modify(T projectile);
}