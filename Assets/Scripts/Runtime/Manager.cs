using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD {
    public class Manager<T> : MonoBehaviour where T : Manager<T> {

        public static T instance = default;

        protected virtual void Awake() {
            if(instance == null) {
                DontDestroyOnLoad(gameObject);
                instance = this as T;
            } else {
                DestroyImmediate(gameObject);
            }
        }

        protected virtual void OnDestroy() {
            if(instance == this) {
                instance = null;
            }
        }
    }
}
