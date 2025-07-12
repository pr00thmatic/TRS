using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class LazyAimFixedUpdate : LazyAim {
    void FixedUpdate () => PerformUpdate();
  }
}
