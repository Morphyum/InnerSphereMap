﻿using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions {
    public static List<T> FindObjectsWithinProximity<T>(this Transform transform, float proximity) where T : MonoBehaviour {
        List<T> objects = new List<T>();

        T[] foundObjects = GameObject.FindObjectsOfType<T>();
        for (int x = 0; x < foundObjects.Length; x++) {
            T obj = foundObjects[x];
            if ((obj.transform.position - transform.position).magnitude <= proximity) {
                objects.Add(obj);
            }
        }

        return objects;
    }

    public static Transform FindRecursive(this Transform transform, string checkName) {
        foreach (Transform t in transform) {
            if (t.name == checkName) return t;

            Transform possibleTransform = FindRecursive(t, checkName);
            if (possibleTransform != null) return possibleTransform;
        }

        return null;
    }
}
