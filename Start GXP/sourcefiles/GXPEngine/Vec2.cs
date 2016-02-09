using System;

namespace GXPEngine
{
    public class Vec2
    {
        public static Vec2 zero { get { return new Vec2(0, 0); } }

        public float x = 0;
        public float y = 0;

        public Vec2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", x, y);
        }

        public Vec2 Add(Vec2 other)
        {
            x += other.x;
            y += other.y;
            return this;
        }
        public Vec2 Substract(Vec2 other)
        {
            x -= other.x;
            y -= other.y;
            return this;
        }
        public Vec2 Multiply(float other)
        {
            x = x * other;
            y = y * other;
            return this;
        }
        public float Lenght()
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        public Vec2 Scale(float scalar)
        {
            x *= scalar;
            y *= scalar;
            return this;
        }
        public Vec2 Normalize()
        {
            float lenght;
            lenght = Lenght();
            if (lenght != 0)
            {
                x = x / lenght;
                y = y / lenght;
            }
            return this;
        }
        public Vec2 Clone()
        {
            Vec2 clone = new Vec2(x, y);
            return clone;
        }
        public Vec2 SetXY(Vec2 other)
        {
            x = other.x;
            y = other.y;
            return this;
        }

        public void Set(float dx, float dy)
        {
            x = dx;
            y = dy;
        }

        public static float Deg2Rad(float degrees)
        {
            float radians;
            radians = degrees * Mathf.PI / 180;
            return radians;
        }

        public static float Rad2Deg(float radians)
        {
            float degrees;
            degrees = radians * 180 / Mathf.PI;
            return degrees;
        }


        public static Vec2 GetUnitVectorDegrees(float degrees)
        {

            Vec2 vec2 = new Vec2(Mathf.Cos(Deg2Rad(degrees)), Mathf.Sin(Deg2Rad(degrees)));
            return vec2;
        }

        public static Vec2 GetUnitVectorRadians(float radians)
        {
            float a = Rad2Deg(radians);
            return GetUnitVectorDegrees(a);
        }

        public static Vec2 GetRandomVector()
        {
            float a = Utils.Random(0, 360);
            return GetUnitVectorDegrees(a);
        }

        public Vec2 SetAngleDegrees(float degrees)
        {
            x = Mathf.Cos(Deg2Rad(degrees));
            y = Mathf.Sin(Deg2Rad(degrees));
            return this;
        }

        public Vec2 SetAngleRadians(float radians)
        {
            float xX = x;
            float yY = y;
            xX = Mathf.Cos(radians);
            yY = Mathf.Sin(radians);
            return this;
        }

        public float GetAngleDegrees()
        {
            float deg;
            deg = Rad2Deg(Mathf.Cos(x));
            return deg;
        }

       public float GetAngleRadians()
        {
            float rad;
            rad = Mathf.Cos(x);
            return rad;
        }

        public Vec2 RotateDegrees(float degrees)
        {

            x = x * Mathf.Cos(Deg2Rad(degrees)) - y * Mathf.Sin(Deg2Rad(degrees));
            y = x * Mathf.Sin(Deg2Rad(degrees)) + y * Mathf.Cos(Deg2Rad(degrees));
            return this;
        }

        public Vec2 RotateRadians(float radians)
        {
            x = x * Mathf.Cos(radians) - y * Mathf.Sin(Deg2Rad(radians));
            y = x * Mathf.Sin(radians) + y * Mathf.Cos(Deg2Rad(radians));
            return this;
        }

        public Vec2 RotateAroundDegrees(float degrees, Vec2 p)
        {
            x -= p.x;
            y -= p.y;
            RotateDegrees(degrees);
            x += p.x;
            y += p.y;
            return this;
        }

        public Vec2 RotateAroundRadians(float radians, Vec2 p)
        {
            x -= p.x;
            y -= p.y;
            RotateRadians(radians);
            x += p.x;
            y += p.y;
            return this;
        }
        public Vec2 Normal()
        {
            float dX = -1 * y;
            float dY =  x;
            return new Vec2(dX,dY).Normalize();
        }
        public float Dot(Vec2 other)
        {
            float dot = x * other.x + y * other.y;
            return dot;
        }
        public Vec2 Reflect(Vec2 pNormal, float pBounciness = 1)
        {
            pNormal = Normal();
            x = this.x -((1 + pBounciness) * (Dot(pNormal) * pNormal.x));
            y = this.y -((1 + pBounciness) * (Dot(pNormal) * pNormal.y));
            return this;
        }
    }
}
