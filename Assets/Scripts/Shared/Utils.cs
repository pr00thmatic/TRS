using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Shared
{
  public class Utils {
    public static class Math {
      public static int Signify (bool condition) => condition? 1: -1;
    }

    public static class Vectors {
      public static Vector3 Abs (Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
      public static bool SmallerByComponent (Vector3 a, Vector3 b) => a.x < b.x && a.y < b.y && a.z < b.z;
      public static bool SmallerByComponent (Vector2 a, Vector2 b) => a.x < b.x && a.y < b.y;
      public static Vector3 SetZ (Vector3 v, float z) { v.z = z; return v; }
      public static Vector3 SetY (Vector3 v, int y) { v.y = y; return v; }
    }
  }
}
