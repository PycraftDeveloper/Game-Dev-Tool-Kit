= Game Dev Tool Kit (v0.11.0) =

Documentation: https://github.com/PycraftDeveloper/Game-Dev-Tool-Kit
E-mail: thomasjebbo@gmail.com

More content is added regularly. Have a recommendation, leave it as an issue on the repo or drop me an email!

The contents listed here have been tested in the Universal Render Pipeline (URP) for Unity 6000, although most of these resources will support other versions and pipelines with a little work (We are also working to support more pipelines!)

Documentation
=============

Editor
======

Quick Links - This script allows you to open the project folder easily from the new 'Game Dev Tool Kit' tab.

Save Render Texture To File - This script allows you to write the contents of the currently selected render texture to an image file on your machine.

Save Screenshot To File - This script allows you to write the current game scene view (at the resolution selected in the inspector for the game to run at) to an image file on your machine. Note: This currently does not support some Canvas configurations.

Materials
=========

Wrap Around Sky - This asset modifies the existing Physics Based Sky material for the Universal Render Pipeline (URP). The default sky material provided by Unity has an ugly 'ground' effect when looking down. This material removes this and replaces it with yet more sky. This is ideal for 3D games that take place in the sky, rarther than on a ground plane.

Terrain Material - This asset adds a realistic, high quality grass/rock material for use on all models. This material blends grass on the flat surface and rock on vertical surfaces. This also adds noise which allows the texture to repeat seamlessly across large models. This supports Unity Terrain (although with warnings that can be ignored). Note: This material is expensive so use with caution on lower power platforms like mobile.

Prefabs
=======
(Just add these to your scene, no setup required!)

Virtual Console - This allows you to place a Canvas in your 3D scene that can be used to display debug content. You can also add your own scripts and use it as a text display in your scene. You can filter Debug - Logs, Warnings, Errors and more!

Utilities
=========
(C# programming helpers and designer tools!)

Camera Adjust - (No programming required) Add this to your scene to force the camera to use a specific aspect ratio! Black bars will be added to displays to ensure the content remains centered on-screen at the desired aspect ratio.

Extended One Shot - This script is used to ensure when an extended sound effect is played, it is destroyed when the sound effect has finished playing to avoid memory leaking. You do not need to interact with this script at all.

Finger IK (Inverse Kinematics) - (No programming required) You can use this script to add inverse kinematics to Meta hands with support for skinned mesh renderers. The script takes a target point in the scene, and will move the joints in the fingers (no thumb support) to realistically try and reach that target point (the target point may not be reached). No programming is required, however you may want to use programming or animation techniques to move the target point around the scene (yes, physics works too!).

Sound Effect Manager - This script can be used to create and play Extended One Shots. These are an extension of the existing one shot setup utility provided by Unity, allowing for sound effects to have their own independant volume, pitch and stereo panning. Add this script to your scene manager and call 'PlayExtendedOneShot' with a reference to your scene manager to start playing sound effects!

Vector Extensions - This script allows you to swizzle and flatten Vector2 and Vector3 data types in Unity to your hearts content. This is similar to the Swizzling API commonly seen in shaders! Flattened Vectors allow you to take one or more of the values inside it, and set them to zero when getting it!

Virtual Console - Used to make the Virtual console prefab work! You do not need to interact with this script at all.