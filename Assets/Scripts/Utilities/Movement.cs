using System;
using UnityEngine;

namespace Utilities
{
    public class Movement : MonoBehaviour, IMovable
    {
        public Action<Vector3> PositionChanged;

        public Vector3 Position
        {
            get {
                return transform.position;
            }
            protected set {
                if (IsMoving) {
                    transform.position = value;
                    PositionChanged?.Invoke(transform.position);
                }
            }
        }

        public bool IsMoving { get; protected set; }

        public virtual void Move()
        {
            IsMoving = true;
        }

        public virtual void Stay()
        {
            IsMoving = false;
        }
    }
}
