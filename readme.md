# Lab Submission Guidlines

Although labs in this course are marked during the lab period, you are still expected to submit you are still expected to submit your source files via GitLab. Failing to do so correctly will result in a 0 for the Lab/Assignment. The guidelines below will help you make sure that you only submit the required files and folders. Furthermore, you are required to attribute sources for all materials included in your projects 

If you need help with any of these steps, email [dcamaren@ryerson.ca](mailto:dcamaren@ryerson.ca)

______________
### Setting up your project:
__\*You only need to do this once per semester\*__
To get started, you should fork your personal repository on GitLab:
-   Go to https://gitlab.scs.ryerson.ca/ and login using your scs.ryerson.ca credentials (Under Kerberos).
    -   ***Be sure to set up your [profile password](https://gitlab.scs.ryerson.ca/profile/password/edit)*
-   Look for the `CPS841` repository and fork it.
-   On the menu on the left, click on `Members`. Add `tmcinern` and `dcamaren` to the repository as developers.
-   Under repository settings, set the visibility to private.

You should have the following folder structure:
-   CPS841-Labs/
    -   .gitignore (Example)
    -   attribution.txt (Example)
    -   readme.md (This file)
________________
### Working on your labs:
To get your lab files on any PC, you can use [Git](https://git-scm.com/download).
To clone your labs, use the following command:

	
    git clone https://gitlab.scs.ryerson.ca/*SCS_USERNAME*/CPS841-Labs.git
	

When prompted, use the following credentials:

**Login:** (Your whole scs **e-mail**)
**Password:** ([Your GitLab password](https://gitlab.scs.ryerson.ca/profile/password/edit))

When you start a new lab, create a new unity project inside `CPS841-Labs` and name it appropriately (Lab1, Lab2, etc) Copy the example gitignore and attribution files. Once you save your project in Unity, your folder structure should look like this (For Lab 1):
-   CPS841-Labs/
    -   Lab1/
        -   Assets/
        -   ProjectSettings/
        -   .gitignore
        -   attribution.txt
        -   ~~Library/~~
        -   ~~Temp/~~
        -   ~~Build/~~
        -   ~~Lab1.csproj~~
        -   ~~Lab1.sln~~
    -   .gitignore
    -   attribution.txt
    -   readme.txt

The files that are ~~crossed out~~ are not essential to your project. In fact, they may not even be there. They are temporary files. Don't worry about deleting them. If you've set up your gitignore correctly, they should be automatically ignored when you commit/push.

In order for Unity to play nice with git, you have to change the way it stores files:
Go to **Edit**>**Project Settings**>**Editor**
In the inspector, change **Version Control Mode** to `Visible Meta Files`
Also change **Asset Serialization Mode** to `Force Text`

Failure to do so could result in a lot of lost work.
______________
### Saving your work.
Periodically, as you're working, it's a good idea to commit and push. (Remember that the lab computers get wiped when you log off). To do this, enter the following commands:

    git add *
    git commit -m "A brief message here"
    git push

To update your repository (if you made changes on another computer), use the following command:

    git pull
    
You can also use the GUI provided in [Git for Windows](https://git-for-windows.github.io/) or [Visual Studio](https://www.visualstudio.com/en-us/docs/git/gitquickstart).
________________
### Submitting your work.
Any work you push is automatically submitted. However, submissions will only be accepted until 23:59:59 PM (Toronto time) on the assignment's due date. Don't worry if you're missing attributions in your partial commits as long as you add them before the final due date.
____________
### Attribution
Inside each lab, you should have an Attributions.txt file I which you can declare whether you've completed this lab on your own. Furthermore, you should utilize this file to provide attribution for any assets you've used in your project. This includes Code, 3D models, Audio, Textures, etc. `attributions.txt` contains an example of sourcing. You can also look at [this resource](https://wiki.creativecommons.org/wiki/Best_practices_for_attribution).

The exceptions for this are any assets that are linked to you by the lab manuals and framework files.(eg Unity, SteamVR, Oculus, etc).

For small code attributions (Smaller than one file), you may opt to include the attribution in the source code itself in the form of an inline comment or a file/function header.
_________________
### Folder structure
Within each lab, it's preferable (but not required) if you keep all of your Assets organized. (e.g. all your scripts in a Scripts folder, all your prefabs in a Prefab folder, etc)
