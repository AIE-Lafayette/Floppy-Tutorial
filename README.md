# Floppy Tutorial

In this tutorial we will create a simple clone of the mobile game Flappy Bird. Our very first step is to create a new project in Unity. This tutorial was made using Unity version 2019.3.10f1. It is recommended that you install and create a project using this version.

## Creating the Bird

Once our project is open, let's start by creating something to represent the bird. We'll make a sphere. In the Hierarchy window, right-click an empty space and select 3D Object -> Sphere. This will create a sphere in the scene. After creating the sphere, we can rename it. Call it "Bird". If we run the game now, the sphere probably isn't positioned in a way that makes it easy for us to see it. We can fix this by repositioning it.

Every GameObject in Unity has what's called a Transform matrix. The Transform matrix keeps track of the object's Position, Rotation, and Scale.

Set the Bird's position to (0, 0, 0), which will place it at the center of the scene, just where our camera is looking. Now when we run the game, we'll see the Bird close to the center of the view.

In addition to the Transform, the Sphere object we created has several other components. Sphere (Mesh Filter) and Mesh Renderer are what draw the sphere on the screen. The Sphere Collider gives it the ability to bump into other objects and interact with them. We need to add a new component, the Rigidbody, which gives our Bird many physics properties.

## Giving the Bird Physics

With the Bird selected, scroll all the way to the bottom of the Inspector window. Click on the button that says "Add Component", then search for Rigidbody and click on it to add it to the object.

Now run the game. The bird falls straight down! This is actually what we want to happen, but now we need a way to flap.

Specialized behavior like flapping can be accomplished with scripts. Scripting in Unity allows us to create custom components that can do almost anything we want.

Right click in the Asset window and select Create -> C# Script. Name the script "FlappingBehavior". Double-click the script to open it.

Modify the text of the file so it looks like the following:

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappingBehavior : MonoBehaviour
{
    //The strength of each flap
    public float Power = 1.0f;

    //A reference to the object's Rigidbody, used for physics calculations
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the Rigidbody
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the button is pressed
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            //Calculate the new velocity
            _body.velocity = new Vector3(0, 1, 0) * Power;
        }
    }
}
```

This code will cause any object it is attached to to move upward when either the left mouse button is clicked or the spacebar is pressed. Note the two variables near the top: power and body. One of them is declared as "private", which means that this is a value that only this script will need to see or know about. It can't be changed from outside the script. The other one is "public". Delcaring a variable as public means that it will be visible and changeable by other parts of the program. Let's go back to the scene to see this.

Select the Bird in the Hierarchy window and scroll to the bottom of the Inspector window. Click on "Add Component" and search for "FlappingBehavior". Add it to the Bird.

Now when we run we are able to flap a little bit when we press spacebar or left-click in the game view. However, our flaps are very weak, and we still quickly fall off of the screen. In the Inspector window, if we look at the FlapppingBehavior component we added, we'll see the "Power" field from our script! We can change this value to modify the strength of each flap. Increase it to 5 and run the game. This is much better.

## Creating a Pipe

Now we need an obstacle. For this we can create a simple pipe. Right-click and empty area in the Hierarchy window and select 3D Object -> Cylinder. Name is "Pipe". Change its position to (10, -10, 0). If we run now, not much has changed. We can see the Pipe, but it doesn't do anything. We'll have to add some components.

Something to consider is that the Bird actually doesn't need to move forward at all. We can save ourselves the trouble of moving the camera by simply moving the pipes instead!

Click the Add Component button and give the pipe a RigidBody. Set it to not use gravity. We don't want the pipe to fall! Also set it to be kinematic. This will allow it to move via script. Now add a new Script called "PipeBehavior" to the pipe and change its text to the following:

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehavior : MonoBehaviour
{
    public float Speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-Speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
```

Run the game and see what happens.

Now it's starting to feel a little bit like a game! Let's make it look a little better.

## Applying Materials

Right-click in the Project window and select Create -> Material. Name the Material "Feathers". Materials can be applied to the surface of an object's mesh to apply colors, textures, and other visual effects.  This one will be used to color the Bird's sphere. Select Feathers in the Project window and change the Albedo color to a bright yellow, or any color of your choice. You can also adjust the Metallic and Smoothness down to 0 to give the Bird a very soft appearance.

We can also make another material called Metal. This one will be used for the Pipe. Change its color to a bright green or other color, then set its Metallic to 1 and leave Smoothness at 0.5.

Now select the Bird in the Hierarchy window and find Element 0 of Materials on the Mesh Renderer component. Click the plus and select Feathers. Our Bird should turn yellow! Do the same for Pipe, using Metal instead of Feathers.

## Building a Stage

Next we'll want to add more Pipes. If we were to copy and paste our Pipe all over the stage and later decided to change the color, shape, or other properties of the pipe, we'd have to change it for every single instance. Instead, we can create what's called a "prefab". In Unity, a prefab is the template for an object. If we place several instances of a prefab in a scene then change the prefab, every instance of that prefab will be updated. This can save us a tremendous amount of time.

Create a prefab from the Pipe by dragging the Pipe from the Hierarchy window to the Project window. You'll notice the icon and text turn blue. Now if you drag the Pipe back from the Project window to the Hierarchy window, it will create more of them.

Arrange the pipes to create a short obstacle course.

Run the game and try it out.

You might notice you can simply fly right over or under the pipes. We can fix this by making the pipes much longer. Select the Pipe prefab in the Project window, and then click on "Open Prefab". Change the Pipe's Scale to (1, 10, 1), and then change its Position to (0, -10, 0) to compensate.

Click the arrow in the top left corner of the Hierarchy window to return to the scene. Now all our Pipes are quite long. We will need to reposition them. You can select multiple objects in the scene by control-clicking them. Then you can easily drag an entire row of Pipes at once.

## Creating a Game Over Message

Finally, we're going to send a message to the screen to indicate that the player has lost if they collide with another object. To do this, we'll need to create a UI element. First, in the Hierarchy window, create a UI -> Canvas. The Canvas is the 2D space we'll use to place the UI elements. Next, right-click the Canvas in the Hierarchy window and select UI -> Text. This will create a Text object in the center of the Canvas and thus the center of the screen. In the Inspector window, scroll down to the Text component and change the properties to suit a Game Over message. If you need a good view of the text, you can change the Scene window to 2D mode by clicking the 2D button at the top of it, then zooming out and panning to see the entire Canvas. Once you like the way the text looks (I recommend size 27, center alignment, red color), clear the Text field. We don't want any text to display until the right time.

We want the text to appear when the Bird collides with something. For this, we want another script. Create a new C# Script called GameOverBehavior, and open it.

Change the text of GameOverBehavior to the following:

```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBehavior : MonoBehaviour
{
    //A reference to a Text object
    public Text GameOverText;

    // OnCollisionEnter is called when the object's collider makes contact with another collider
    void OnCollisionEnter(Collision collision)
    {
        //Display the text "Game Over!"
        GameOverText.text = "Game Over!";
    }
}
```

This will display the text "Game Over!" in the Text object that we specify. Add this component to the Bird. Notice the "Game Over Text" field. Click the plus and select the Text object. Now it will display "Game Over!" on the screen when a collision happens.

Run the game to try it out. It actually resembles Flappy Bird now.

## Adding the Finishing Touches

If you right-click on the Bird, you can add objects as children of it. Add a cylinder and position and scale it to resemble a beak. Create a new material for it, too.

You can also add a lip to the pipe using another carefully placed cylinder.

Decorate your scene however you like. See what improvements you can make on your own!

## Creating a Unity Package

To export a package for distribution, click on `Assets` &rarr; `Export Package...`. Then, in the window that appears, ensure that _all_ items are selected and click `Export...`. This will create a unitypackage file which can be sent as an attachment in an email or other message.
