using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class LazyAimUpdate : LazyAim {
    void Update () => PerformUpdate();
  }
}
