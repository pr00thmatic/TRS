using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared
{
  public enum HandSide { Left = 0, Right = 1 }

  public static class HandSideExtensions {
    public static HandSide GetOpposite (this HandSide handSide) => handSide == HandSide.Left? HandSide.Right : HandSide.Left;
  }
}
