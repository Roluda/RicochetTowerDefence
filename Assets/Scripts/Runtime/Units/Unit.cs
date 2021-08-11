using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Units {
    public class Unit : MonoBehaviour {
        Dictionary<Type, List<UnitComponent>> componentsDict = new Dictionary<Type, List<UnitComponent>>();

        public void RegisterUnitComponent(UnitComponent unitComponent) {
            if(componentsDict.TryGetValue(unitComponent.GetType(), out var list)){
                list.Add(unitComponent);
            } else {
                componentsDict[unitComponent.GetType()] = new List<UnitComponent>();
                componentsDict[unitComponent.GetType()].Add(unitComponent);
            }
        }

        public void UnregisterUnitComponent(UnitComponent unitComponent) {
            if (componentsDict.TryGetValue(unitComponent.GetType(), out var list)) {
                list.Remove(unitComponent);
            }
        }

        public T UnitComponent<T>() where T : UnitComponent {
            if (componentsDict.TryGetValue(typeof(T), out var candidates)) {
                return candidates[0] as T;
            }
            return null;
        }

        public bool UnitComponent<T>(out T component) where T : UnitComponent {
            if(componentsDict.TryGetValue(typeof(T), out var candidates)){
                component = candidates[0] as T;
                return true;
            }
            component = null;
            return false;
        }

        public IEnumerable<T> UnitComponents<T>() where T: UnitComponent {
            if (componentsDict.TryGetValue(typeof(T), out var candidates)) {
                return candidates as List<T>;
            }
            return null;
        }
    }
}
