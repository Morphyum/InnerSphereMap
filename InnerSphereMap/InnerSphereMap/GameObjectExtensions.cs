﻿using UnityEngine;
using System.Collections.Generic;

public static class GameObjectExtensions {
    public static List<GameObject> FindAllContains(this GameObject go, string name) {
        List<GameObject> gameObjects = new List<GameObject>();

        foreach (Transform t in go.transform) {
            if (t.name.Contains(name)) {
                gameObjects.Add(t.gameObject);
            }
        }

        return gameObjects;
    }
}
