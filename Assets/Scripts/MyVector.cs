using System;
using System.Collections.Generic;

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
        return null;
    }
    public MyVector Add(MyVector pVector)
    {
        return null;
    }
    public MyVector Subtract(MyVector pVector)
    {
        return null;
    }
    public MyVector Multiply(float pScalar)
    {
        return null;
    }
    public MyVector Divide(float pScalar)
    {
        return null;
    }
    public float Magnitude()
    {
        return -1;
    }
    public MyVector Normalise()
    {
        return null;
    }
    public float DotProduct(MyVector pVector)
    {
        return -1;
    }
    public MyVector RotateX(float pRadians)
    {
        return null;
    }
    public MyVector RotateY(float pRadians)
    {
        return null;
    }
    public MyVector RotateZ(float pRadians)
    {
        return null;
    }
    public MyVector LimitTo(float pMax)
    {
        return null;
    }
    public MyVector Interpolate(MyVector pVector, float pInterpolation)
    {
        return null;
    }
    public float AngleBetween(MyVector pVector)
    {
        return -1;
    }
    public MyVector CrossProduct(MyVector pVector)
    {
        return null;
    }
    public override string ToString()
    {
        string result = "X: " + X + " " + "Y: " + Y + " " + "Z: " + Z;
        return result;
    }
}
