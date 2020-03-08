using UnityEngine;

namespace Positions
{
    public class PositionsGenerator
    {
        /// <summary>
        /// Produces a Vector of the location of a point on a sphere on a 2D plane.(x,y)
        /// </summary>
        /// <param name="center">The center location of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="ang">The angle produced from the center to the point from the positive x axis</param>
        public static Vector3 CircleLocation(Vector3 center, float radius, float ang)
        {
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.z = center.z;
            return pos;
        }

        public static Vector3 SphereLocation(Vector3 center, float radius, float rotation, float elevation = 0)
        {
            Vector3 pos = new Vector3();
            pos.y = center.y + (radius * Mathf.Sin(elevation * Mathf.Deg2Rad));
            float _xz = radius * Mathf.Cos(elevation * Mathf.Deg2Rad);
            pos.x = center.x + (_xz * Mathf.Cos(rotation * Mathf.Deg2Rad));
            pos.z = center.z + (_xz * Mathf.Sin(rotation * Mathf.Deg2Rad));
            return pos;
        }

        public static Vector3 CircleLocation(float radius, float ang, string facing = "x")
        {
            switch (facing)
            {
                case "x":
                    return new Vector3(0, Mathf.Sin(ang) * radius, Mathf.Cos(ang) * radius);
                case "y":
                    return new Vector3(Mathf.Sin(ang) * radius, 0, Mathf.Cos(ang) * radius);
                case "z":
                    return new Vector3(Mathf.Sin(ang) * radius, Mathf.Cos(ang) * radius, 0);
                default:
                    return new Vector3(0, 0, 0);
            }
        }

        public static Vector3 TriangleLocation(float ang_A, float ang_B, float side_C)
        {
            float ang_C = 180 - (ang_A + ang_B);
            float side_B = (side_C * Mathf.Sin(Mathf.Deg2Rad * ang_C)) / Mathf.Sin(Mathf.Deg2Rad * ang_B);
            Vector3 pos = CircleLocation(side_B, 90 - ang_A);
            return pos;
        }

        public static Vector3 TriangleLocation(Vector3 center, float ang_A, float ang_B, float side_C)
        {
            float ang_C = 180 - (ang_A + ang_B);
            float side_B = Mathf.Sin(Mathf.Deg2Rad * ang_B) / side_C * Mathf.Sin(Mathf.Deg2Rad * ang_C);
            Vector3 pos = CircleLocation(center, side_B, 90 - ang_A);
            return pos;
        }
    }
}