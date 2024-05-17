using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRing : ProjectileGroup
{
    public Spawnable spawn;

    public Vector2 position;
    public float radius;
    public float startAngle;
    public float endAngle;
    public int numSpawns = 5;
    public bool spawnEnds = true;

    public List<PositionParam> positionAffects;
    public List<AngleParam> angleAffects;
    public List<IndexParam> indexAffects;

    public override void Spawn()
    {
        float angleStep = (endAngle-startAngle)/(numSpawns - (spawnEnds ? 1 : 0)) * Mathf.Deg2Rad;
        float currentAngle = startAngle * Mathf.Deg2Rad;
        for(int i = 0; i < numSpawns; i++, currentAngle += angleStep)
        {
            Vector2 angleVector = new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
            Vector2 spawnPos = position + angleVector * radius;
            foreach(var param in positionAffects)
            {
                param.ApplyParameter(spawn, spawnPos);
            }
            foreach(var param in angleAffects)
            {
                param.ApplyParameter(spawn, currentAngle);
            }
            foreach(var param in indexAffects)
            {
                param.ApplyParameter(spawn, i);
            }
            spawn.Spawn();
        }
    }
}
