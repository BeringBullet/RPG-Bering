using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static implicit operator Vector3(SerializableVector3 s)
            => new Vector3(s.x, s.y, s.z);

        public static implicit operator SerializableVector3(Vector3 v)
            => new SerializableVector3(v);
    }
}