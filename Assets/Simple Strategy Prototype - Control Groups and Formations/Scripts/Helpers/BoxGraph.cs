using System.Collections;
using UnityEngine;

namespace SimpleRTS.Helpers
{
    [System.Serializable]
    public class BoxGraphNode
    {
        public BoxGraphNode top;
        public BoxGraphNode bottom;
        public BoxGraphNode left;
        public BoxGraphNode right;

        public float collisionRadius;
        public Vector3 relativePosition;
    }
    public class BoxGraph : MonoBehaviour
    {

    }
}