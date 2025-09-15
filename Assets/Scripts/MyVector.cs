using System;
using System.Collections.Generic;
using UnityEngine;

public class MyVector
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }
    public float W { get; private set; }

    public MyVector(float pX, float pY, float pZ, float pW = 1)
    {
        X = pX;
        Y = pY;
        Z = pZ;
        W = pW;
    }
    public MyVector Copy()
    {
        return new MyVector(this.X, this.Y, this.Z, this.W);
    }
    public MyVector Add(MyVector pVector)
    {
        MyVector tempVector = new MyVector(0, 0, 0);

        float newX = this.X + pVector.X;
        float newY = this.Y + pVector.Y;
        float newZ = this.Z + pVector.Z;
        float newW = this.W + pVector.W;

        return new MyVector(newX, newY, newZ, newW);
    }
    public MyVector Subtract(MyVector pVector)
    {
        float newX = this.X - pVector.X;
        float newY = this.Y - pVector.Y;
        float newZ = this.Z - pVector.Z;
        float newW = this.W - pVector.W;

        return new MyVector(newX, newY, newZ, newW);
    }
    public MyVector Multiply(float pScalar)
    {
        float newX = this.X * pScalar;
        float newY = this.Y * pScalar;
        float newZ = this.Z * pScalar;
        float newW = this.W * pScalar;

        return new MyVector(newX, newY, newZ, newW);
    }
    public MyVector Divide(float pScalar)
    {
        float newX = this.X / pScalar;
        float newY = this.Y / pScalar;
        float newZ = this.Z / pScalar;
        float newW = this.W / pScalar;

        return new MyVector(newX, newY, newZ, newW);


    }
    public float Magnitude()
    {
        return Mathf.Sqrt(X * X + Y * Y + Z * Z);
    }
    public MyVector Normalise()
    {
        float magnitude = Magnitude();
        if (magnitude > 0)
        {
            float newX = this.X / magnitude;
            float newY = this.Y / magnitude;
            float newZ = this.Z / magnitude;

            return new MyVector(newX, newY, newZ);
        }
        return this;

    }
    public float DotProduct(MyVector pVector)
    {
        float dotProduct = this.X * pVector.X + this.Y * pVector.Y + this.Z * pVector.Z + this.W * pVector.W; ;
        return dotProduct - 1;
    }
    public MyVector RotateX(float pRadians)
    {
        float sin = Mathf.Sin(pRadians);
        float cos = Mathf.Cos(pRadians);

        float newY = Y * cos - Z * sin;
        float newZ = Y * sin + Z * cos;
        return new MyVector(X, newY, newZ);

    }
    public MyVector RotateY(float pRadians)
    {
        float sin = Mathf.Sin(pRadians);
        float cos = Mathf.Cos(pRadians);
        float newX = X * cos + Z * sin;
        float newZ = -X * sin + Z * cos;
        return new MyVector(newX, Y, newZ);
    }
    public MyVector RotateZ(float pRadians)
    {
        float sin = Mathf.Sin(pRadians);
        float cos = Mathf.Cos(pRadians);
        float newX = X * cos - Y * sin;
        float newY = X * sin + Y * cos;
        return new MyVector(newX, newY, Z);
    }
    public MyVector LimitTo(float pMax)
    {
        float magnitude = Magnitude();
        if (magnitude > pMax)
        {
            float result = pMax / magnitude;
            float newX = X * result;
            float newY = Y * result;
            float newZ = Z * result;
            return new MyVector(newX, newY, newZ);
        }
        else
        {
            return new MyVector(X, Y, Z);
        }
    }
    public MyVector Interpolate(MyVector pVector, float pInterpolation)
    {
        float newX = X + (pVector.X - X) * pInterpolation;
        float newY = Y + (pVector.Y - Y) * pInterpolation;
        float newZ = Z + (pVector.Z - Z) * pInterpolation;
        return new MyVector(newX, newY, newZ);
    }
    public float AngleBetween(MyVector pVector)
    {
        float dotProduct = DotProduct(pVector);

        float magnitudeOne = Magnitude();
        float magnitudeTwo = pVector.Magnitude();

        float cos = dotProduct / (magnitudeOne * magnitudeTwo);

        float radians = Mathf.Acos(cos);
        return radians;
    }
    public MyVector CrossProduct(MyVector pVector)
    {
        float x = Y * pVector.Z - Z * pVector.Y;
        float y = Z * pVector.X - X * pVector.Z;
        float z = X * pVector.Y - Y * pVector.X;
        return new MyVector(x, y, z);
    }
    public override string ToString()
    {
        string result = "X: " + X + " " + "Y: " + Y + " " + "Z: " + Z;
        return result;
    }
}
