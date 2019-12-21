using System;
using UnityEditor;
using UnityEngine;

namespace Cindy.Editor
{
    public class Test : ScriptableObject
    {
        [MenuItem("Cindy/TestA")]
        public static void TestA()
        {
            Debug.Log("A");
        }
    }
}
