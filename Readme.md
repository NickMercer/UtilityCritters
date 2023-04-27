
## Introduction

Welcome! This repo is a demo of a simplified Utility AI system. It's not something I'd consider production ready, but it's a good example of how you might get started approaching Utility AI. 

The demo is a simulated savannah ecosystem, with Lions, Giraffes, and Elephants with Food, Drink, and Sleep needs. When you get the repo set up in a Unity project, running it will start the simulation and you can watch the animals interact and try to fulfill their survival needs. Once you see how it works, I'd recommend looking at the code for the project to get an idea of how it works. 

## Setup 
To get this running on your PC, you'll want to clone the repo and open it using Unity **2021.3.21f1** or **2021.3.23f1**. You may be able to use a newer version and upgrade the project, but this is the version it was developed on and tested with.

When you open the project in the Unity Editor, it may ask about Safe Mode, because it's missing DOTween. You can boot in safe mode. 

Just go to Window -> Package Manager and import DOTween. If you don't have DOTween, you can get it for free on the Unity asset store here: https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676

I just couldn't include it in the gitlab repo because it is a third party asset and not something from Unity.

Once you import DOTween, it should pop up with a setup window. If it does not, you can force it by going to Tools -> Demigiant -> DOTween Utility Panel

On the DOTween Utility Panel, click "Setup DOTween", leave the settings at default, and hit apply.

You should now have no compilation errors and no longer be in safe mode.

To start the demo, Go to Assets/Scenes/ and open "SampleScene". Then hit the play button.

## Important Code Folders
If you want to take a look at the code and see how the Utility AI System works, here are the most important folders and files to look at:

**Assets/SimpleUtilityFramework/UtilitySystem** - Houses the Utility AI system itself. 
 - Agent/Brain.cs contains the decision making logic, this is the core of the utility system.
 - Actions/AIBehaviour.cs contains the base class for all AI actions

**Assets/SimpleUtilityFramework/Animals/Scripts/AI Behaviours** - Houses all the actions that AI Agents (animals) in the demo can take. These are examples of how you would extend agent behaviour in a Utility AI system.

## Utility AI Resources
Here are some great resources for learning/implementing Utility AI in Unity or in general:

#### Theory / General Introduction
- (Youtube Essay) [How Utility AI Helps NPCs Decide What to Do Next - AI and Games](https://www.youtube.com/watch?v=p3Jbp2cZg3Q)
- (Conference Talk) [Building a Better Centaur: AI at Massive Scale - Mike Lewis, Dave Mark](https://www.gdcvault.com/play/1021848/Building-a-Better-Centaur-AI)
- (Article) [An Introduction to Utility Theory - Rez Graham](http://www.gameaipro.com/GameAIPro/GameAIPro_Chapter09_An_Introduction_to_Utility_Theory.pdf)
- (Article) [AI Decision Making with Utility Scores - Jon McGuire](https://mcguirev10.com/2019/01/03/ai-decision-making-with-utility-scores-part-1.html)

#### Tutorials / Implementation Details
- (Youtube Tutorial Series) [Utility AI For Unity - TooLoo](https://www.youtube.com/watch?v=ejKrvhusU1I&list=PLDpv2FF85TOp2KpIGcrxXY1POzfYLGWIb)
- (Conference Talk) [Improving AI Decision Modeling Through Utility Theory - Kevin Dill, Dave Mark](https://www.gdcvault.com/play/1012410/Improving-AI-Decision-Modeling-Through)
- (Conference Talk) [Embracing the Dark Art of Mathematical Modeling in AI - Kevin Dill, Dave Mark](https://www.gdcvault.com/play/1015421/Embracing-the-Dark-Art-of)
- (Conference Talk) [Winding Road Ahead: Designing Utility AI with Curvature - Mike Lewis](https://www.youtube.com/watch?v=TCf1GdRrerw&t=4s)

## Final Thoughts

If you were an attendee of GameDevGuild 2023, I suggest looking through this repo while following along with my Modular AI with Utility Systems talk for a better idea of what is going on. 
Apart from that, please feel free to reach out to me on Twitter @NatickGames if you have any questions and I'll gladly talk with you about them.


