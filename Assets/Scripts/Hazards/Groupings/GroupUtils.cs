using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GroupUtils
{
    public static void ApplyParameter(object spawn, string path, object value)
    {
        string[] memberNames = path.Split('.', '/');
        ref object targetObject = ref spawn;
        FieldInfo targetMember = targetObject.GetType().GetField(memberNames[0]);
        for (int i = 1; i < memberNames.Length; i++)
        {
            targetObject = targetMember.GetValue(targetObject);
            if (memberNames[i].StartsWith("modifiers["))
            {
                targetObject = targetObject.GetType().GetField("modifiers").GetValue(targetObject);
                targetObject = (targetObject as IList)[int.Parse(memberNames[i][10..^1])];
                i++;
            }
            targetMember = targetObject.GetType().GetField(memberNames[i]);
        }
        targetMember.SetValue(targetObject, value);
    }
}

[Serializable]
public class AngleParam
{
    public string path;
    public bool useVector;
    public float vectorMagnitude = 1;

    public void ApplyParameter(object spawn, float angle)
    {
        if (useVector)
            GroupUtils.ApplyParameter(spawn, path, new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * vectorMagnitude);
        else
            GroupUtils.ApplyParameter(spawn, path, angle);
    }
}

[Serializable]
public class PositionParam
{
    public string path;

    public void ApplyParameter(object spawn, Vector2 position)
    {
        GroupUtils.ApplyParameter(spawn, path, position);
    }
}

[Serializable]
public class IndexParam
{
    public string path;

    public void ApplyParameter(object spawn, int index)
    {
        GroupUtils.ApplyParameter(spawn, path, index);
    }
}
