# Lab Submission Guidlines

Although labs in this course are marked during the lab period, you are still expected to submit you are still expected to submit your source files via GitLab. Failing to do so correctly will result in a 0 for the Lab/Assignment. The guidelines below will help you make sure that you only submit the required files and folders. Furthermore, you are required to attribute sources for all materials included in your projects 

### Setting up your workspace:
To get started, you should clone your personal repository from GitLab.
Go to https://gitlab.scs.ryerson.ca/ and login using your scs.ryerson.ca credentials.
Look for the `CPS841` repository and clone it to your local computer
You should have the following folder structure:
-   CPS841/
    -   example_.gitignore
    -   example_attribution.txt
    -   readme.txt

### Working on your labs:
When you start a new lab, create a new unity project inside `/CPS841` and name it appropriately (Lab1, Lab2, etc) Copy the example gitignore and attribution files and remove 'example_' from their name. Once you save your project in Unity, your folder structure should look like this (For Lab 1):
-   CPS841/
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
    -   example_.gitignore
    -   example_attribution.txt
    -   readme.txt

The files that are ~~crossed out~~ are not essential to your project. In fact, they may not even be there. They are temporary files. Don't worry about deleting them. If you've set up your gitignore correctly, they should be automatically ignored when you commit/push.

### Saving your work.
Periodically, as you're working, it's a good idea to commit and push. (Remember that the lab computers get wiped when you log off). To do this, enter the following commands:

git add *
git commit -m "A brief message here"
git push

### Submitting your work.
Any work you push is automatically submitted. However, submissions will only be accepted until 23:59:59 PM (Toronto time) on the assignment's due date. Don't worry if you're missing attributions in your partial commits as long as you add them before the final due date.

### Attribution
Inside each lab, you should have an Attributions.txt file I which you can declare whether you've completed this lab on your own. Furthermore, you should utilize this file to provide attribution for any assets you've used in your project. This includes Code, 3D models, Audio, Textures, etc. example_Attributions.txt contains an example of sourcing. You can also look at [this resource](https://wiki.creativecommons.org/wiki/Best_practices_for_attribution).

The exception for this are any assets that are linked to you by the lab manual and framework files.(eg Unity, SteamVR, Oculus, etc).

For small code attributions (Smaller than one file), you may opt to include the attribution in the source code itself in the form of an inline comment or a file/function header.

### Folder structure
Within each lab, it's preferable (but not required) if you keep all of your Assets organized. (e.g. all your scripts in a Scripts folder, all your prefabs in a Prefab folder, etc)
