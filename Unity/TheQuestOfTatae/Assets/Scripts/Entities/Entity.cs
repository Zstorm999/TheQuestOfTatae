using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public enum Direction { NONE, UP, DOWN, LEFT, RIGHT}

    public enum Action { NONE, WALK, ATTACK_0}

    public struct Entityf
    {
        /// <summary>
        /// Returns the 2D Vector corresponding to the given Direction
        /// </summary>
        /// <param name="dir">The Direction provided</param>
        /// <returns>A Vector2 that can be either (0,0), (0,1), (0,-1), (1,0) or (-1,0) depending on the Direction given</returns>
        public static Vector2 GetDirectionAxis(Direction dir)
        {
            switch (dir)
            {
                case Direction.NONE: return Vector2.zero;
                case Direction.UP: return Vector2.up;
                case Direction.DOWN: return Vector2.down;
                case Direction.LEFT: return Vector2.left;
                case Direction.RIGHT: return Vector2.right;

                //default should never happen, but compilers are pretty dumb
                default: return Vector2.zero;
            }
        }

        /// <summary>
        /// Returns the Direction associated with a 2D Vector. If the vector is not an axis of the orthogonal plane, returns NONE
        /// </summary>
        /// <param name="vec">A 2D Vector</param>
        /// <returns>The associated direction, or NONE if the direction is invalid or 0</returns>
        public static Direction GetAxisDirection(Vector2 vec)
        {
            if(vec.x == 0)
            {
                if (vec.y > 0) return Direction.UP;
                else if (vec.y < 0) return Direction.DOWN;
            }
            else if(vec.y == 0)
            {
                if (vec.x > 0) return Direction.RIGHT;
                else if (vec.x < 0) return Direction.LEFT;
            }

            //reaching this point means either 0 vector, or non-axis. Either way this is an invalid direction
            return Direction.NONE;

        }

    }

}
