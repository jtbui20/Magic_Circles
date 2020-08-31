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
        /// <param name="rotation">The angle produced from the center to the point from the positive x axis</param>
        public static Vector3 CircleLocation(Vector3 center, float radius, float rotation, string facing = "z")
        {
            float ang = rotation * Mathf.Deg2Rad;
            float a = Mathf.Cos(ang) * radius;
            float b = Mathf.Sin(ang) * radius;
            switch (facing)
            {
                case "x": return center + new Vector3(0, b, a);
                case "y": return center + new Vector3(b, 0, a);
                case "z": return center + new Vector3(b, a, 0);
                default: return center + new Vector3(0, 0, 0);
            }
        }

        public static Vector3 SphereLocation(Vector3 center, float radius, float rotation, float elevation = 0)
        {
            float rot = rotation * Mathf.Deg2Rad;
            float ele = elevation * Mathf.Deg2Rad;
            Vector3 pos = new Vector3();

            pos.y = center.y + (radius * Mathf.Sin(ele));
            float _xz = radius * Mathf.Cos(ele);
            pos.x = center.x + (_xz * Mathf.Cos(rot));
            pos.z = center.z + (_xz * Mathf.Sin(rot));
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