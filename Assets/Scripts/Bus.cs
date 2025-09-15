using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus
{
    public MyVector Position;
    public MyVector Rotation;
    public MyVector Scale;

    public TransformNode busNode { get; private set; }
    private TransformNode positionNode { get; set; }
    private TransformNode rotationNodeX { get; set; }
    private TransformNode rotationNodeY { get; set; }
    private TransformNode rotationNodeZ { get; set; }
    private TransformNode scaleNode { get; set; }

    public Bus(MyVector pPosition, MyVector pRotation, MyVector pScale)
    {
        Position = pPosition;
        Rotation = pRotation;
        Scale = pScale;
        InitiateSceneGraphNode();
    }
    public void Update(MyVector newPos, MyVector newRotY)
    {
        Rotation = newRotY;
        MyMatrix newRotationY = MyMatrix.CreateRotationY(Rotation.Y);
        rotationNodeY.localTransform = newRotationY;

        Position = Position.Add(newPos);
        MyMatrix newTranslation = MyMatrix.CreateTranslation(Position);
        positionNode.localTransform = newTranslation;
    }

    public void InitiateSceneGraphNode()
    {
        MyMatrix translationMatrix = MyMatrix.CreateTranslation(Position);
        positionNode = new TransformNode("BusPosition", translationMatrix);

        MyMatrix rotationXMatrix = MyMatrix.CreateRotationX(Rotation.X);
        rotationNodeX = new TransformNode("BusRotationX", rotationXMatrix);

        MyMatrix rotationYMatrix = MyMatrix.CreateRotationY(Rotation.Y);
        rotationNodeY = new TransformNode("BusRotationY", rotationYMatrix);

        MyMatrix rotationZMatrix = MyMatrix.CreateRotationZ(Rotation.Z);
        rotationNodeZ = new TransformNode("BusRotationZ", rotationZMatrix);

        MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
        scaleNode = new TransformNode("BusScale", scaleMatrix);

        busNode = positionNode;

        positionNode.AddChild(rotationNodeX);
        rotationNodeX.AddChild(rotationNodeY);
        rotationNodeY.AddChild(rotationNodeZ);
        rotationNodeZ.AddChild(scaleNode);

        BuildBody(scaleNode);
        BuildWheels(scaleNode);
        BuildWindows(scaleNode);
    }
    public void BuildBody(TransformNode pParentTransform)
    {
        MyMatrix bodyScaleMatrix = MyMatrix.CreateScale(new MyVector(2, 1, 1));
        TransformNode bodyScaleNode = new TransformNode("BusBodyScale", bodyScaleMatrix);
        pParentTransform.AddChild(bodyScaleNode);

        GameObject bodyBottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bodyBottom.GetComponent<Renderer>().material.color = Color.red;
        GeometryNode busBottomObject = new GeometryNode("BusBottomObject", bodyBottom);
        bodyScaleNode.AddChild(busBottomObject);

        MyMatrix roofMatrix = MyMatrix.CreateTranslation(new MyVector(0, 0.5f, 0));
        TransformNode roofTranslation = new TransformNode("BusRoofTranslation", roofMatrix);
        pParentTransform.AddChild(roofTranslation);

        MyMatrix roofRotationMatrix = MyMatrix.CreateRotationZ(Mathf.PI / 2);
        TransformNode roofRotationZ = new TransformNode("BusRoofRotation", roofRotationMatrix);
        roofTranslation.AddChild(roofRotationZ);

        GameObject bodyRoof = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        bodyRoof.GetComponent<Renderer>().material.color = Color.red;
        GeometryNode busRoofObject = new GeometryNode("BusRoofObject", bodyRoof);
        roofRotationZ.AddChild(busRoofObject);
    }

    public void BuildWheels(TransformNode pParentTransform)
    {
        MyMatrix wheel1 = MyMatrix.CreateTranslation(new MyVector(-0.6f, -0.5f, 0.5f));
        TransformNode wheel1Translation = new TransformNode("Wheel1Translation", wheel1);
        pParentTransform.AddChild(wheel1Translation);
        BuildWheel(wheel1Translation);

        MyMatrix wheel2 = MyMatrix.CreateTranslation(new MyVector(0.6f, -0.5f, 0.5f));
        TransformNode wheel2Translation = new TransformNode("Wheel2Translation", wheel2);
        pParentTransform.AddChild(wheel2Translation);
        BuildWheel(wheel2Translation);

        MyMatrix wheel3 = MyMatrix.CreateTranslation(new MyVector(-0.6f, -0.5f, -0.5f));
        TransformNode wheel3Translation = new TransformNode("Wheel3Translation", wheel3);
        pParentTransform.AddChild(wheel3Translation);
        BuildWheel(wheel3Translation);

        MyMatrix wheel4 = MyMatrix.CreateTranslation(new MyVector(0.6f, -0.5f, -0.5f));
        TransformNode wheel4Translation = new TransformNode("Wheel4Translation", wheel4);
        pParentTransform.AddChild(wheel4Translation);
        BuildWheel(wheel4Translation);
    }

    public void BuildWheel(TransformNode pParentTransform)
    {
        MyMatrix wheelRotationX = (MyMatrix.CreateRotationX(Mathf.PI / 2));
        TransformNode wheelRotationXNode = new TransformNode("WheelRotationX", wheelRotationX);
        pParentTransform.AddChild(wheelRotationXNode); ;

        MyMatrix wheelScale = MyMatrix.CreateScale(new MyVector(0.4f, 0.05f, 0.4f));
        TransformNode wheelScaleNode = new TransformNode("WheelScale", wheelScale);
        wheelRotationXNode.AddChild(wheelScaleNode);

        GameObject wheel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        wheel.GetComponent<Renderer>().material.color = Color.black;
        GeometryNode wheelNode = new GeometryNode("Wheel", wheel);
        wheelScaleNode.AddChild(wheelNode);
    }

    public void BuildWindows(TransformNode pParentTransform)
    {
        MyMatrix frontWindowMatrix = MyMatrix.CreateTranslation(new MyVector(1, 0, 0));
        TransformNode frontWindowTranslation = new TransformNode("FrontWindowTranslation", frontWindowMatrix);
        pParentTransform.AddChild(frontWindowTranslation);

        MyMatrix frontWindowScale = MyMatrix.CreateScale(new MyVector(0.1f, 0.5f, 0.9f));
        TransformNode frontWindowTransformScale = new TransformNode("FrontWindowScale", frontWindowScale);
        frontWindowTranslation.AddChild(frontWindowTransformScale);

        GameObject frontWindowObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        frontWindowObject.GetComponent<Renderer>().material.color = Color.blue;
        GeometryNode frontWindowNode = new GeometryNode("FrontWindow", frontWindowObject);
        frontWindowTransformScale.AddChild(frontWindowNode);

        MyMatrix frontWindow = MyMatrix.CreateTranslation(new MyVector(0.65f, 0, 0));
        TransformNode frontWindowTransform = new TransformNode("FrontWindowTransform", frontWindow);
        pParentTransform.AddChild(frontWindowTransform);
        MiddleWindows(frontWindowTransform);

        MyMatrix middleWindow = MyMatrix.CreateTranslation(new MyVector(0.05f, 0, 0));
        TransformNode middleWindowTransform = new TransformNode("MiddleWindowTransform", middleWindow);
        pParentTransform.AddChild(middleWindowTransform);
        MiddleWindows(middleWindowTransform);

        MyMatrix backWindow = MyMatrix.CreateTranslation(new MyVector(-0.55f, 0, 0));
        TransformNode backWindowTransform = new TransformNode("BackWindowTransform", backWindow);
        pParentTransform.AddChild(backWindowTransform);
        MiddleWindows(backWindowTransform);
    }

    public void MiddleWindows(TransformNode pParentNode)
    {
        MyMatrix windowsScale = MyMatrix.CreateScale(new MyVector(0.5f, 0.4f, 1.1f));
        TransformNode windowScale = new TransformNode("WindowScale", windowsScale);
        pParentNode.AddChild(windowScale);

        GameObject windowsFront = GameObject.CreatePrimitive(PrimitiveType.Cube);
        windowsFront.GetComponent<Renderer>().material.color = Color.blue;
        GeometryNode frontWindows = new GeometryNode("FrontWindows", windowsFront);
        windowScale.AddChild(frontWindows);
    }
    
}