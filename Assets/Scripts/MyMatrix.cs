using Codice.Client.Common.GameUI;
using GluonGui.Dialog;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
    public void SetTransform(Transform pGameObject)
    {
        SetPosition(pGameObject);
        SetScale(pGameObject);
        SetRotation(pGameObject);
    }
    private void SetPosition(Transform pTransformObject)
    {
        Vector3 position = pTransformObject.position;
        pTransformObject.position = position;
    }
    private void SetScale(Transform pTransformObject)
    {
        Vector3 scale = new Vector3(matrix[0, 0], matrix[1, 1], matrix[2, 2]);
        pTransformObject.localScale = scale;
    }
    private void SetRotation(Transform pTransformObject)
    {
        Matrix4x4 rotationMatrix = new Matrix4x4();
        for(int row = 0; row < 3; row++)
        {
            for(int col = 0; col < 3; col++)
            {
                rotationMatrix[row, col] = matrix[row, col];
            }
        }
        Vector3 euler = rotationMatrix.rotation.eulerAngles;
        pTransformObject.rotation = Quaternion.Euler(euler);
        
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
        MyMatrix translationMatrix = CreateIdentity();
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
        MyMatrix rotationMatrix = CreateIdentity();
        float sin = Mathf.Sin(pAngle);
        float cos = Mathf.Cos(pAngle);

        rotationMatrix.matrix[0, 0] = 1;
        rotationMatrix.matrix[1, 1] = cos;
        rotationMatrix.matrix[1, 2] = -sin;
        rotationMatrix.matrix[2, 1] = sin;
        rotationMatrix.matrix[2, 2] = cos;
        //rotationMatrix.matrix[3, 3] = 1;


        return rotationMatrix;
    }

    public static MyMatrix CreateRotationY(float pAngle)
    {
        MyMatrix rotationMatrix = CreateIdentity();
        float sin = Mathf.Sin(pAngle);
        float cos = Mathf.Cos(pAngle);

        rotationMatrix.matrix[0, 0] = cos;
        rotationMatrix.matrix[0, 2] = sin;
        rotationMatrix.matrix[2, 0] = -sin;
        rotationMatrix.matrix[2, 2] = cos;
        //rotationMatrix.matrix[1, 1] = 1;
        return rotationMatrix;
    }

    public static MyMatrix CreateRotationZ(float pAngle)
    {
        MyMatrix rotationMatrix = CreateIdentity();
        float sin = Mathf.Sin(pAngle);
        float cos = Mathf.Cos(pAngle);

        rotationMatrix.matrix[0, 0] = cos;
        rotationMatrix.matrix[0, 1] = -sin;
        rotationMatrix.matrix[1, 0] = sin;
        rotationMatrix.matrix[1, 1] = cos;
        //rotationMatrix.matrix[2, 2] = 1;
        return rotationMatrix;
    }

    public MyVector Multiply(MyVector pVector)
    {
        float[] vector = new float[4];
        

        for(int row = 0; row < 4; row++)
        {
            vector[row] = 0;

            for(int col = 0; col < 4; col++)
            {
                if(col == 0)
                {
                    vector[row] += matrix[row, col] * pVector.X;
                }
                if (col == 1)
                {
                    vector[row] += matrix[row, col] * pVector.Y;
                }
                if (col == 2)
                {
                    vector[row] += matrix[row, col] * pVector.Z;
                }
                if (col == 3)
                {
                    vector[row] += matrix[row, col] * pVector.W;
                }

            }
        }
        return new MyVector(vector[0], vector[1], vector[2], vector[3]);
    }


    public MyMatrix Multiply(MyMatrix pMatrix)
    {
        MyMatrix multiplyMatrix = new MyMatrix();

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                for (int i = 0; i < 4; i++)
                {
                    multiplyMatrix.matrix[row, col] += this.matrix[row, i] * pMatrix.matrix[i, col];
                }
            }
        }

        return multiplyMatrix;
    }

    //public MyMatrix Multiply(MyMatrix pMatrix)
    //{

    //    //if (matrix.GetLength(1) != pMatrix.matrix.GetLength(0))
    //    //{
    //    //    throw new Exception();
    //    //}
    //    //int rows = matrix.GetLength(0);
    //    //int columns = pMatrix.matrix.GetLength(1);
    //    //MyMatrix multiplyMatrix = new MyMatrix(rows, columns);
        
    //    //for(int i = 0; i < rows; i++)
    //    //{
    //    //    for(int j = 0; j < columns; j++)
    //    //    {
    //    //        int sum = 0;
    //    //        for(int k = 0; k < matrix.GetLength(1); k++)
    //    //        {
    //    //            sum = matrix[i, j] = sum;
    //    //        }
    //    //    }
    //    //}
    //    return null;
    //}

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
