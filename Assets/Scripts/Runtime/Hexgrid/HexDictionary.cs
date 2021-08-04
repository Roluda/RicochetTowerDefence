using RTD.Hexagons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTD.Hexgrid {
    [Serializable]
    public class HexDictionary : Dictionary<Hex3, GameObject>, ISerializationCallbackReceiver {

        [SerializeField] List<Hex3> keyList = new List<Hex3>();
        [SerializeField] List<GameObject> valueslist = new List<GameObject>();

        public void OnBeforeSerialize() {
            keyList.Clear();
            valueslist.Clear();

            foreach (var kvp in this) {
                keyList.Add(kvp.Key);
                valueslist.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize() {
            Clear();

            for (var i = 0; i != Math.Min(keyList.Count, valueslist.Count); i++) {
                this[keyList[i]]=valueslist[i];
            }
        }
    }
}
