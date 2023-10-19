using Codice.Client.Common.GameUI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MyMatrix
{

    private float[,] matrix = new float[4, 4];
    public MyMatrix() { }
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
        matrix[0, 0] = pRow0Column0;
        matrix[0, 1] = pRow0Column1;
        matrix[0, 2] = pRow0Column2;
        matrix[0, 3] = pRow0Column3;
        matrix[1, 0] = pRow1Column0;
        matrix[1, 1] = pRow1Column1;
        matrix[1, 2] = pRow1Column2;
        matrix[1, 3] = pRow1Column3;
        matrix[2, 0] = pRow2Column0;
        matrix[2, 1] = pRow2Column1;
        matrix[2, 2] = pRow2Column2;
        matrix[2, 3] = pRow2Column3;
        matrix[3, 0] = pRow3Column0;
        matrix[3, 1] = pRow3Column1;
        matrix[3, 2] = pRow3Column2;
        matrix[3, 3] = pRow3Column3;
    }

    public float GetElement(int pRow, int pColumn)
    {
        return matrix[pRow, pColumn];
    }
    
    public static MyMatrix CreateIdentity()
    {
        
        MyMatrix identityMatrix = new MyMatrix();
        for(int i = 0; i < 4; i++)
        {
  
            identityMatrix.matrix[i, i] = 1.0f;

        }
        return identityMatrix;
    }

    public static MyMatrix CreateTranslation(MyVector pTranslation)
    { 
        MyMatrix translationMatrix = new MyMatrix();
        translationMatrix.matrix[0, 3] = pTranslation.X;
        translationMatrix.matrix[1, 3] = pTranslation.Y;
        translationMatrix.matrix[2, 3] = pTranslation.Z;
        translationMatrix.matrix[3, 3] = pTranslation.W;
        return translationMatrix;
    }

    public static MyMatrix CreateScale(MyVector pScale)
    {
        MyMatrix scaleMatrix = new MyMatrix();
        scaleMatrix.matrix[0, 0] = pScale.X;
        scaleMatrix.matrix[1,1] = pScale.Y;
        scaleMatrix.matrix[2, 2] = pScale.Z;
        scaleMatrix.matrix[3, 3] = 1.0f;
        return scaleMatrix;
    }

    public static MyMatrix CreateRotationX(float pAngle)
    {
        MyMatrix rotateMatrix = new MyMatrix();
        float sin = Mathf.Sin(pAngle);
        float cos = Mathf.Cos(pAngle);

        rotateMatrix.matrix[0, 0] = 1;
        rotateMatrix.matrix[1, 1] = cos;
        rotateMatrix.matrix[1, 2] = -sin;
        rotateMatrix.matrix[2, 1] = sin;
        rotateMatrix.matrix[2, 2] = cos;
        rotateMatrix.matrix[3, 3] = 1;


        return rotateMatrix;
    }

    public static MyMatrix CreateRotationY(float pAngle)
    {
        //MyMatrix rotateMatrix = new MyMatrix();
        //float sin = Mathf.Sin(pAngle);
        //float cos = Mathf.Cos(pAngle);

        //rotateMatrix.matrix[0, 0] = cos;
        //rotateMatrix.matrix[0, 2] = sin;
        //rotateMatrix.matrix[2, 0] = -sin;
        //rotateMatrix.matrix[2, 2] = cos;
        //return rotateMatrix;
        return null;
    }

    public static MyMatrix CreateRotationZ(float pAngle)
    {
        //MyMatrix rotateMatrix = new MyMatrix();
        //float sin = Mathf.Sin(pAngle);
        //float cos = Mathf.Cos(pAngle);

        //rotateMatrix.matrix[0, 0] = cos;
        //rotateMatrix.matrix[0, 1] = -sin;
        //rotateMatrix.matrix[1, 0] = sin;
        //rotateMatrix.matrix[1, 1] = cos;
        //return rotateMatrix;
        return null;
    }

    public MyVector Multiply(MyVector pVector)
    {
        return null;
    }

    public MyMatrix Multiply(MyMatrix pMatrix)
    {
        MyMatrix multiplyMatrix = new MyMatrix();

        //float newX = this.X * pScalar;
        //float newY = this.Y * pScalar;
        //float newZ = this.Z * pScalar;
        //float newW = this.W * pScalar;

        //return new MyVector(newX, newY, newZ, newW);
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
