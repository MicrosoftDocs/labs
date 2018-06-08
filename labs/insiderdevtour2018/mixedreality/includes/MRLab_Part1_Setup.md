## Intro to Windows Mixed Reality and this lab

Mixed reality blends real-world and virtual content into hybrid environments where physical and digital objects coexist and interact.  

In this lab, you will build an experience for both Windows Mixed Reality immersive headsets (VR) and Microsoft HoloLens (AR). You will use Unity to create a virtual room where the users will interact with a speaker playing Spatial Sound. The users will get to experience the sound as they teleport themselves across the room, or as they move the speaker throughout the room. 

The experience will be built with the <a href="https://github.com/microsoft/mixedrealitytoolkit-unity" target="_blank">Mixed Reality Toolkit</a>, therefore maximizing your reuse with for HoloLens; towards the end of the lab, you will make a few tweaks to re-target the experience the VR experience and optimize it for a HoloLens device.    

After you complete this lab you will:
       
* Gain familiarity and a solid understanding on how to use Unity to create a VR experience    
* Know how to configure Unity to target UWP Mixed Reality applications   
* Understand basic Mixed Reality building blocks like setting up your cameras, handling input (via gaze, motion controllers, and gestures), and using spatial sound in an MR experience.  
* Understand how to create a Mixed Reality experience that is adaptive to both occluded headsets and HoloLens. 


### Hardware Requirements

* An <a href="https://docs.microsoft.com/windows/mixed-reality/install-the-tools#system-requirements" target="_blank">MR capable machine</a>
* [Optional] A Windows Mixed Reality immersive headset with Motion Controllers
* [Optional] A HoloLens device
>**Note:** If you do not have access to an immersive headset or a HoloLens , you can view your app using the Mixed Reality simulator or the HoloLens emulator. See links below. 

### Software Requirements
* <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017</a>. Any SKU will work, including the free community edition SKU. Ensure you select these workloads and features during setup: 
    *  The Universal Windows Platform development 
    *  The Game Development with Unity workload
    *  <a href="https://developer.microsoft.com/windows/downloads/windows-10-sdk" target="_blank">Windows 10 Fall Creators Update SDK or later</a> (this is included in the latest version of Visual Studio) 2017
	>**Note:** Workloads can be accessed by navigating to Tools -> Get Tools and Features when in Visual Studio

*  <a href="https://unity3d.com/get-unity/download/archive" target="_blank">Unity 2017.4.3</a> (be sure to include the .Net Scripting Backend when selecting components at setup)
* Windows 10 Fall Creators Update OS (or later) 
    * To enable Developer mode: go to system Settings -> Update & security -> For developers
* [Optional] <a href="https://docs.microsoft.com/windows/mixed-reality/using-the-hololens-emulator" target="_blank">HoloLens Emulator</a> or <a href="https://docs.microsoft.com/windows/mixed-reality/using-the-windows-mixed-reality-simulator" target="_blank">Mixed Reality Simulator</a>. 
