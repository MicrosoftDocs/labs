## Intro to Windows Mixed Reality and this lab

Mixed reality blends real-world and virtual content into hybrid environments where physical and digital objects coexist and interact.  

In this lab, you will build an experience for both Windows Mixed Reality immersive headsets (VR) and Microsoft HoloLens (AR). You will use Unity to create a virtual room where the users will interact with a speaker playing Spatial Sound. The users will get to experience the sound as they teleport themselves across the room, or as they move the speaker throughout the room. 

The experience will be built with the [Mixed Reality Toolkit](https://github.com/microsoft/mixedrealitytoolkit-unity), therefore maximizing your reuse with for HoloLens; towards the end of the lab, you will make a few tweaks to re-target the experience the VR experience and optimize it for a HoloLens device.    

After you complete this lab you will:
       
* Gain familiarity and a solid understanding on how to use Unity to create a VR experience    
* Know how to configure Unity to target UWP Mixed Reality applications   
* Understand basic Mixed Reality building blocks like setting up your cameras, handling input (via gaze, motion controllers, and gestures), and using spatial sound in an MR experience.  
* Understand how to create a Mixed Reality experience that is adaptive to both occluded headsets and HoloLens. 


### Hardware Requirements

* An [MR capable machine](https://docs.microsoft.com/windows/mixed-reality/install-the-tools#system-requirements)
* [Optional] An Windows Mixed Reality immersive headset with Motion Controllers
* [Optional] A HoloLens device
>**Note:** If you do not have access to an immersive headset or a HoloLens , you can view your app using the Mixed Reality simulator or the HoloLens emulator. See links below. 

### Software Requirements
* [Visual Studio 2017](https://www.visualstudio.com/downloads/), any SKU, including the free community edition SKU. Ensure you select these workloads and features during setup: 
    *  The Universal Windows Platform development 
    *  The Game Development with Unity workload
    *  [Windows 10 Fall Creators Update SDK or later](https://developer.microsoft.com/windows/downloads/windows-10-sdk) (this is included in the latest version of Visual Studio) 2017
	>**Note:** Workloads can be accessed by navigating to Tools -> Get Tools and Features when in Visual Studio

*  [Unity 2017.4.3](https://unity3d.com/get-unity/download/archive) (be sure to include the .Net Scripting Backend when selecting components at setup)
* Windows 10 Fall Creators Update OS (or later) 
    * To enable Developer mode: go to system Settings -> Update & security -> For developers
* [Optional] [HoloLens Emulator](https://docs.microsoft.com/windows/mixed-reality/using-the-hololens-emulator) or [Mixed Reality Simulator](https://docs.microsoft.com/windows/mixed-reality/using-the-windows-mixed-reality-simulator). 
