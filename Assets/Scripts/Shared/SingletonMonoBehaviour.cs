using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T> {
    private static T _instance;
    public static T Instance {
      get { if (!_instance) _instance = FindFirstObjectByType<T>(); return _instance; }
    }

    virtual protected void Awake () {
      if (_instance && _instance != this) {
        Destroy(gameObject);
        return;
      }

      Assert.IsTrue(this is T casted && casted, "Something wen't wrong on the implementation of this singleton, please make sure the generic type fits the class implementing this singleton base class");
      _instance = this as T;
    }
  }
}
