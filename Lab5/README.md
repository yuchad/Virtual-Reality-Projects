# Lab 5

We will construct a short multiplayer interaction using networking in virtual reality.

##### Warning: 
*   You are expected to completely implement the features in the lab yourself. Marks may be docked if you've copied implementations from third parties. This includes SteamVR.

*   This lab will be completed in teams of 2. Teams will be assembled by seating position during your lab period as follows:
    *   VCL-08 and VCL-09
    *   VCL-05 and VCL-10
    *   VCL-02 and VCL-07
    *   VCL-03 and VCL-04
    *   VCL-01 and VCL-06

##### Due:

*   Task 1 is entirely research. Failure to do so will result in your
*   You must attend your lab section in order for the lab to be marked. Further instructions may be given during the lab section.
*   Commit and push to your repository before March 8th. 23:59:59. No late submissions will be pulled.

##### Objectives:

*   Gain an understanding of server-authoritative infrastructure.
*   Implement multiplayer interaction

##### Tasks:

1. Become acquainted with Unity's server-authoritative infrastructure:
    *   Before your lab period, it is a good idea if you become acquainted with the networking system, there's three ways to go about this, choose the one(s) that better fit your learning methods: 
        *   Browse the Skeleton project and read the scripts included with it. See the hints section below for instructions on how to test multiplayer locally. (You need VR to run it).
        *   Peruse the first few topics of the [UNet documentation](https://docs.unity3d.com/Manual/UNetUsingHLAPI.html)
        *   Complete this short introductory tutorial for non-VR networking. [(Text)](https://unity3d.com/learn/tutorials/topics/multiplayer-networking) [(Video)](https://www.youtube.com/playlist?annotation_id=annotation_3530634901&feature=iv&list=PLwyZdDTyvucw5JhBMJxFwsYc1EbQYxr0G&src_vid=NLnzlwCRjgc)
    *   Make sure you can answer the following questions:
        *   [What is the role of the client and the server?](https://docs.unity3d.com/Manual/UNetConcepts.html)
        *   [What is object authority?](https://docs.unity3d.com/Manual/UNetConcepts.html)
        *   [How can you tell if a script is running on the server or the client?](https://docs.unity3d.com/Manual/UNetConcepts.html)
        *   [What is a NetworkBehaviour?](https://docs.unity3d.com/ScriptReference/Networking.NetworkBehaviour.html)
        *   [How do you create an that all players can see?](https://docs.unity3d.com/Manual/UNetSpawning.html)
        *   [How do you synchronize an object's position?](https://docs.unity3d.com/Manual/class-NetworkTransform.html)
        *   [How do you synchronize script state across clients?](https://docs.unity3d.com/Manual/UNetStateSync.html)
        *   [How do you trigger an action on the server from the client?](https://docs.unity3d.com/Manual/UNetActions.html)   

1.  **Fork**, Clone and explore the [provided skeleton project](https://gitlab.scs.ryerson.ca/CPS-841/Lab5). Use this as a starting point for your lab.
    * Set the access to private
    * Add the following people to the repository:
        * Your teammate as **Master** 
        * `dcamaren` as **Developer**
    * Inspect the following implementations:
        *  Input Processing (NetworkedCameraRig.cs)
        *  Object Creation (NetworkedCameraRig.cs)
        *  Command Sending (NetworkedCameraRig.cs)
        *  Variable Synchronization (ColorSync.cs)

1.  Construct a short 2-player interaction experiment that incorporates the four concepts listed in task 2.
    *   You will receive full marks for even the simplest of interactions that uses all four of the concepts listed in task 2 and fulfills these two conditions:
        *   Your interaction must be either competitive or collaborative. (it must involve both users)
        *   Your interaction must have an end goal and be able to reset to its original condition.
    *   Here are some examples:
        *   Tic tac toe
        *   Collaborative tower building
        *   Volleyball
        *   Jenga
        *   Shoot and dodge
        *   Collaborative jigsaw puzzle
        *   shield and sword
        *   Assembly line simulator
    *   Feel free to implement a simple version of your project's interaction. (Provided your lab partner agrees)

*   _(Optional: Difficult!)_ 
    *   User Avatars: Use a slightly more relatable model to represent the user's head and hands. Then, Do one of the following: 
        *   Animate the eyes using the relative position of the head, hands and other players.
        *   Animate the mouth using Microphone Input
        *   Use a visime plugin to create facial expressions based on sound.
    *   Integrate with Lab4.
        *   Try to port at least one of your Interactables from Lab4 so that it works over the network.

##### Deliverables:

The following functionality will be marked during your lab period. 

Only one of your team members must submit the code to their repository; the other must only submit Attributions.txt.

**Warning: points will not be awarded for the included default implementations**

1.  Networked Input Processing (2 pts)
1.  Networked Object Creation (2 pts)
1.  Networked Command Sending (2 pts max)
1.  Networked Variable Synchronization (2 pts)
1.  Interaction is multiplayer-oriented (2 pts)


*   Total Points : 10
*   Bonus:
    *   User avatar:
        *   Better model (0.5 pts)
        *   Animation (0.5 pts)
    *   Lab4 integration 
        *   Attempt (0.5pts)
        *   functionality (0.5 pts)

*   Penalties:
    *   Project is poorly organized: (Repository config, etc)
        *   This lab's attributions are slightly different. Please fill them out.
    *   Feature implementation was copied/misattributed (variable)
    *   Project does not render at 90fps on Lab computers (-1pts)
        *   You can view the rendering speed by looking at SteamVR-> Settings -> Performance -> Display Frame Timing
    *   State is de-synchronized between players (-.5 per unique instance)
        *   Only applicable to objects on which interaction is dependent.

##### Hints:

*   Although this is not performant, the easiest way to implement multiplayer functionality is to write all of the game logic on the server and simply transmit the input to the server. Then, synchronize any relevant object's positions to the client. As such, any sort of collision detection and score keeping should be calculated on the server, synchronized to the client, and then have the models updated.

*   Here's how to set up a joint development environment on two computers: 
    1.  Clone the repository on each computer. You should be able to use your own credentials even if the repository is in your teammate's account.
    1.  Whenever you need to test, pull the latest version from the server and run it locally.
    1.  Select your local ip from the drop-down and tell it to your teammate. Make sure not to make any errors.
    1.   Your teammate should now be able to communicate using the ip you've given them.

*   When you pull a shared repository, it may have been the case that both you and your teammate have edited the same files
    *   One approach is to develop mostly one machine and use the other for testing only. What you can then do is discard your changes and pull the repository. (There is a script for this in the Lab5 folder.)
    *   If they're source files, You can see a handy visualization of the difference between your files in the [Visual Studio Team Explorer](https://www.visualstudio.com/en-us/docs/git/tutorial/merging) or you could [resolve them manually](https://help.github.com/articles/resolving-a-merge-conflict-using-the-command-line/)
    *   In the case of scenes, prefabs and other assets, merging can become a bit troublesome. If possible, keep either their entire file or your own.


*   It is possible to host a server and a client on the same machine. However, SteamVR only allows one VR application to remain open at a time. In order to get around this:
    *   Build and run your executable. Start it as a host.
    *   On your editor, go to `Edit -> Project Settings -> Player -> Other Settings -> Virtual Reality Supported (Un-check)` 
        *   You may want to Open a second inspector and lock it to this screen for quick access.
    *   Now, press the play button, enter `localhost` and click join as client.


*   This lab is loosely based on these talks:
    *   [Social VR by Google](https://youtu.be/lGUmTQgbiAY?t=17m48s)
    *   [ToyBox demo by Oculus](https://youtu.be/iFEMiyGMa58)
    *   [Oculus Social Demo](https://www.youtube.com/watch?v=YuIgyKLPt3s) and its [explanation by Mike Booth](https://youtu.be/0EZn50XCueI)

*   This lab contains assets from these sources, (you do not need to source these):
    *   [Wacki UI input by Mark Wacker](http://wacki.me/blog/2016/06/vr-gui-input-module-for-unity-htc-vive/)
    *   [Paintings Free by Webcadabra](https://www.assetstore.unity3d.com/en/#!/content/44185)
    *   [SimpleHomeStuff by mohelm97](https://www.assetstore.unity3d.com/en/#!/content/69129) 