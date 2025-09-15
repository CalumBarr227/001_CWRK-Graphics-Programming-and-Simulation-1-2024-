# 3D Scene Graph and Physics Simulation - Graphics Programming and Simulation 1 (2024)

## Overview
This Unity project was devleoped as part of the **Graphics Programming and Simulation 1** module. It implements a **custom 3D scene graph** and physics simulation using **user-defined vector and matrix classes.** The project demonstrates object-oriented design, hierarchical scene management, and realistic physics interactions.

## Key features
- Custom **MyVector** and **MyMatrix** classes for linear algebra operations
- Support for **translations, rotation,** and **scaling** transformations
- Hierarchical **Scene Graph** with **Group, Transform** and **Geometry** nodes
- **RenderVisitor pattern** for traversing and rendering the custom scene graph
- Processor-independent **animations** using **Time.deltaTime**
- Collision detection for **spheres, cylinders, planes,** and compound objects with realistic elastic and inelastic responses

## Features & Implementation
### 1. MyVector
- Custom 3D vector class for mathematical operations
- Fully tested in Unity scripts
- Supports addition, subtraction, dot/cross product, and normalisation

### 2. MyMatrix
- Custom 3x3 and 4x4 matrix implementation
- Supports matrix multiplication, transpose, inverse, and identity operations
- Used for implementing transformations in custom scene graph

### 3. Transformations
- **Translation, Rotation,** and **Scaling** supported using **MyMatrix**
- Compound transformations can be applied to any scene node
- Transformation logic operates independently of Unity’s built-in Transform system

### 4. Scene Graph
- Hierarchical node system implemented entirely in C#
- Node types:
  - **Group**: Container for child nodes
  - **TransformNode**: Applies transformations to children
  - **GeometryNode**: Represents a geometric object in the scene graph; stores the object’s data for rendering
- RenderVisitor pattern used to traverse and render nodes
- Operates independently of Unity's internal GameObject hierarchy

### Physics Simulation
- Processor-independent animations using Unity's Time.deltaTime
- Supports moving and stationary objects
- Physics computations implemented in scripts

### Collision Handling
- Collisions supported between:
  - 3D spheres
  - Axis-aligned cylinders
  - Infinite planes and lines
  - Compound objects
- Handles both **elastic** and **inelastic** collisions
- Momentum exchange supported for objects with different masses
- Spatial partitioning implemented for efficiency
