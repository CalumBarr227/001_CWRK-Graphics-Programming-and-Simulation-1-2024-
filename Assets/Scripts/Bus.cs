using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bus
{
    public MyVector Position { get; set; }
    public MyVector Scale { get; set; }
    public MyVector Rotation { get; set; }

    public Bus(MyVector pPosition, MyVector pRotation, MyVector pScale)
    {
        Position = pPosition;
        Rotation = pRotation;
        Scale = pScale;
    }

    public static void BuildBus()
    {
        CreateBottomOfBus();
        CreateTopOfBus();
        for(int i = 0; i <4; i++)
        {
            MyMatrix buildWheelParentTransform = MyMatrix.CreateTranslation(new MyVector(0.6f, 0.8f + i * 0.4f, 0f));
            BuildWheel(buildWheelParentTransform);
        }
    }
    public static void CreateBottomOfBus()
    {
        GameObject bodyBottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bodyBottom.GetComponent<Renderer>().material.color = Color.red;
        MyVector localTranslation = new MyVector(1, 0, 0);
        MyMatrix bottomTransformMatrix = MyMatrix.CreateTranslation(localTranslation);
        bottomTransformMatrix.SetTransform(bodyBottom.transform);

         
    }
    public static void CreateTopOfBus()
    {
        GameObject bodyTop = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        bodyTop.GetComponent<Renderer>().material.color = Color.red;
        MyVector localTranslation = new MyVector(1, 0, 0);
        MyMatrix topTransformMatrix = MyMatrix.CreateTranslation(localTranslation);
        topTransformMatrix.SetTransform(bodyTop.transform);
    }
    private static void BuildWheel(MyMatrix pParentTransform)
    {
        MyMatrix localTranslation = MyMatrix.CreateTranslation(new MyVector(2, 2, 2));
        MyMatrix localRotation = MyMatrix.CreateRotationY(Mathf.PI / 4);
        MyMatrix localScale = MyMatrix.CreateScale(new MyVector(0.2f, 0.2f, 0.2f));

        MyMatrix localTransform = localTranslation.Multiply(localRotation).Multiply(localScale);
        MyMatrix comboTransform = pParentTransform.Multiply(localTransform);

        GameObject wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        wheel.GetComponent<Renderer>().material.color = Color.black;
        comboTransform.SetTransform(wheel.transform);

    }

   
}
