using System;
using System.Collections.Generic;


public class MyMatrix
{
    
    

    public MyMatrix(float pRow0Column0,
        float pRow0Column1,
        float pRow0Column2,
        float pRow0Column3,
        float pRow1Column0,
        float pRow1Column1,
        float pRow1Column2,
        float pRow1Column3,
        float pRow2Column0,
        float pRow2Column1,
        float pRow2Column2,
        float pRow2Column3,
        float pRow3Column0,
        float pRow3Column1,
        float pRow3Column2,
        float pRow3Column3)
    {
        
    }

    public float GetElement(int pRow, int pColumn)
    {
        return -1;
    }
    
    public static MyMatrix CreateIdentity()
    {
        return null;
    }

    public static MyMatrix CreateTranslation(MyVector pTranslation)
    {
        return null;
    }

    public static MyMatrix CreateScale(MyVector pScale)
    {
        return null;
    }

    public static MyMatrix CreateRotationX(float pAngle)
    {
        return null;
    }

    public static MyMatrix CreateRotationY(float pAngle)
    {
        return null;
    }

    public static MyMatrix CreateRotationZ(float pAngle)
    {
        return null;
    }

    public MyVector Multiply(MyVector pVector)
    {
        return null;
    }

    public MyMatrix Multiply(MyMatrix pMatrix)
    {
        return null;
    }

    public MyMatrix Inverse()
    {
        return null;
    }

    public override string ToString()
    {
        string result = GetElement(0, 0) + GetElement(0, 1) + GetElement(0, 2) + GetElement(0, 3) + "\n" +
            GetElement(1, 0) + GetElement(1, 1) + GetElement(1, 2) + GetElement(1, 3) + "\n" +
            GetElement(2, 0) + GetElement(2, 1) + GetElement(2, 2) + GetElement(2, 3) + "\n" +
            GetElement(3, 0) + GetElement(3, 1) + GetElement(3, 2) + GetElement(3, 3) + "\n";
        return result;
    }
}
