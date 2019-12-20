using System;
using UnityEditor;
using UnityEngine;

namespace Cindy.Editor
{
    public class Test : ScriptableObject
    {
        [MenuItem("Cindy/Test")]
        public static void TestA()
        {
            Debug.Log("TestA");
        }
    }
}
