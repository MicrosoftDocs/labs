## 8. Graphics (optional)

We made a few extra tweaks to the visual aspects of the lab - a little can go a long way. If you want to learn how easy it is to give the experience some extra spice, feel free to follow these steps.

1. From the Menu click Window -> Lighting -> Settings.
2. Drag the Cloudymorning skybox from the Project window Textures folder into the Skybox Material property.

> **Note:** You can do the following for any scene camera, but you won't be able to see it in the editor.
  
1. Select the MixedRealityCameraParent -> MixedRealityCamera in the scene Hierarchy.
2. Click Add Component -> Post Processing Behaviour.
3. Find CustomProfile.asset in the Assets -> PostProcessing folder and drag it into the Profile property of the Post Processing Behaviour script. 
    
Play with the Post Processing profile as much as you please, but keep in mind the [overhead that accompanies them.](https://docs.unity3d.com/Manual/PostProcessing-Stack.html)