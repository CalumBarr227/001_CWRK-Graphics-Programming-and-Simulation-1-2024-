
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
    public void SetTransform(GameObject pObject)
    {
        Transform transform = pObject.transform;
        SetPosition(pObject);
        SetScale(pObject);
        SetRotation(pObject);
    }

    private void SetPosition(GameObject pObject)
    {
        Vector3 position = new Vector3(GetElement(0, 3), GetElement(1, 3), GetElement(2, 3));
        pObject.transform.position = position;

    }

    private void SetScale(GameObject pObject)
    {
        Vector3 scale;
        MyVector xColumn = new MyVector(GetElement(0, 0), GetElement(1, 0), GetElement(2, 0), GetElement(3, 0));
        float xScale = xColumn.Magnitude();

        MyVector yColumn = new MyVector(GetElement(0, 1), GetElement(1, 1), GetElement(2, 1), GetElement(3, 1));
        float yScale = yColumn.Magnitude();

        MyVector zColumn = new MyVector(GetElement(0, 2), GetElement(1, 2), GetElement(2, 2), GetElement(3, 2));
        float zScale = zColumn.Magnitude();

        scale.x = xScale;
        scale.y = yScale;
        scale.z = zScale;
        pObject.transform.localScale = scale;

    }

    private void SetRotation(GameObject pObject)
    {
        Vector3 direction;
        direction.x = GetElement(0, 2);
        direction.y = GetElement(1, 2);
        direction.z = GetElement(2, 2);

        Vector3 vert;
        vert.x = GetElement(0, 1);
        vert.y = GetElement(1, 1);
        vert.z = GetElement(2, 1);

        pObject.transform.rotation = Quaternion.LookRotation(direction, vert);
    }
    public float GetElement(int pRow, int pColumn)
    {
        return matrix[pRow, pColumn];
    }
    public void SetElement(int pRow, int pColumn, float pValue)
    {
        matrix[pRow, pColumn] = pValue;
    }
    public static MyMatrix CreateIdentity()
    {

        MyMatrix identityMatrix = new MyMatrix();
        for (int i = 0; i < 4; i++)
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
        scaleMatrix.matrix[1, 1] = pScale.Y;
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


        for (int row = 0; row < 4; row++)
        {
            vector[row] = 0;

            for (int col = 0; col < 4; col++)
            {
                if (col == 0)
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
        MyMatrix multiplyMatrix = MyMatrix.CreateIdentity();

        for (int row = 0; row < 4; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                multiplyMatrix.SetElement(
                    row,
                    column,
                    this.GetElement(row, 0) * pMatrix.GetElement(0, column) +
                    this.GetElement(row, 1) * pMatrix.GetElement(1, column) +
                    this.GetElement(row, 2) * pMatrix.GetElement(2, column) +
                    this.GetElement(row, 3) * pMatrix.GetElement(3, column)
                    );
            }
        }

        return multiplyMatrix;
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
