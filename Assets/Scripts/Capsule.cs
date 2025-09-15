using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule
{
    public TransformNode RootNode { get; private set; }
    private TransformNode positionNode { get; set; }
    private TransformNode scaleNode { get; set; }
    private TransformNode rotationXNode { get; set; }
    private TransformNode rotationYNode { get; set; }
    private TransformNode rotationZNode { get; set; }

    public MyVector Position { get; set; }
    public MyVector Rotation { get; set; }
    public MyVector Scale { get; set; }
    public Color Colour { get; set; }

    public MyVector StartPosition
    {
        get
        {
            MyVector v = new MyVector(0, 1, 0);
            MyMatrix translationMatrix = MyMatrix.CreateTranslation(Position);
            MyMatrix rotationXMatrix = MyMatrix.CreateRotationX(Rotation.X);
            MyMatrix rotationYMatrix = MyMatrix.CreateRotationY(Rotation.Y);
            MyMatrix rotationZMatrix = MyMatrix.CreateRotationZ(Rotation.Z);
            MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
            return translationMatrix.Multiply(rotationXMatrix).Multiply(rotationYMatrix).Multiply(rotationZMatrix).Multiply(scaleMatrix).Multiply(v);
        }
    }

    public MyVector EndPosition
    {
        get
        {
            MyVector v = new MyVector(0, -1, 0);
            MyMatrix translationMatrix = MyMatrix.CreateTranslation(Position);
            MyMatrix rotationXMatrix = MyMatrix.CreateRotationX(Rotation.X);
            MyMatrix rotationYMatrix = MyMatrix.CreateRotationY(Rotation.Y);
            MyMatrix rotationZMatrix = MyMatrix.CreateRotationZ(Rotation.Z);
            MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
            return translationMatrix.Multiply(rotationXMatrix).Multiply(rotationYMatrix).Multiply(rotationZMatrix).Multiply(scaleMatrix).Multiply(v);
        }
    }

    public Capsule(MyVector pPosition, MyVector pRotation, MyVector pScale, Color pColour)
    {
        Position = pPosition;
        Rotation = pRotation;
        Scale = pScale;
        Colour = pColour;
        InitialiseSceneGraph();
    }

    public void InitialiseSceneGraph()
    {
        MyMatrix capsuleTranslation = MyMatrix.CreateTranslation(Position);
        positionNode = new TransformNode("capsuleTranslation", capsuleTranslation);

        MyMatrix rotationX = MyMatrix.CreateRotationX(Rotation.X);
        rotationXNode = new TransformNode("RotationX", rotationX);
        positionNode.AddChild(rotationXNode);

        MyMatrix rotationY = MyMatrix.CreateRotationY(Rotation.Y);
        rotationYNode = new TransformNode("RotationY", rotationY);
        rotationXNode.AddChild(rotationYNode);

        MyMatrix rotationZ = MyMatrix.CreateRotationZ(Rotation.Z);
        rotationZNode = new TransformNode("RotationZ", rotationZ);
        rotationYNode.AddChild(rotationZNode);

        MyMatrix capsuleScale = MyMatrix.CreateScale(Scale);
        scaleNode = new TransformNode("Scale", capsuleScale);
        rotationZNode.AddChild(scaleNode);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        capsule.GetComponent<Renderer>().material.color = Colour;
        GeometryNode capsuleObject = new GeometryNode("Capsule Object", capsule);
        scaleNode.AddChild(capsuleObject);

        RootNode = positionNode;
    }
    public void BuildCapsule(TransformNode pParentTransform)
    {

    }
}
