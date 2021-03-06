▓█████ ▓█████▄  ██▓▄▄▄█████▓ ▒█████   ██▀███     ▄▄▄█████▓ ▒█████   ▒█████   ██▓        ▄▄▄▄    ▒█████  ▒██   ██▒
▓█   ▀ ▒██▀ ██▌▓██▒▓  ██▒ ▓▒▒██▒  ██▒▓██ ▒ ██▒   ▓  ██▒ ▓▒▒██▒  ██▒▒██▒  ██▒▓██▒       ▓█████▄ ▒██▒  ██▒▒▒ █ █ ▒░
▒███   ░██   █▌▒██▒▒ ▓██░ ▒░▒██░  ██▒▓██ ░▄█ ▒   ▒ ▓██░ ▒░▒██░  ██▒▒██░  ██▒▒██░       ▒██▒ ▄██▒██░  ██▒░░  █   ░
▒▓█  ▄ ░▓█▄   ▌░██░░ ▓██▓ ░ ▒██   ██░▒██▀▀█▄     ░ ▓██▓ ░ ▒██   ██░▒██   ██░▒██░       ▒██░█▀  ▒██   ██░ ░ █ █ ▒ 
░▒████▒░▒████▓ ░██░  ▒██▒ ░ ░ ████▓▒░░██▓ ▒██▒     ▒██▒ ░ ░ ████▓▒░░ ████▓▒░░██████▒   ░▓█  ▀█▓░ ████▓▒░▒██▒ ▒██▒
░░ ▒░ ░ ▒▒▓  ▒ ░▓    ▒ ░░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░     ▒ ░░   ░ ▒░▒░▒░ ░ ▒░▒░▒░ ░ ▒░▓  ░   ░▒▓███▀▒░ ▒░▒░▒░ ▒▒ ░ ░▓ ░
 ░ ░  ░ ░ ▒  ▒  ▒ ░    ░      ░ ▒ ▒░   ░▒ ░ ▒░       ░      ░ ▒ ▒░   ░ ▒ ▒░ ░ ░ ▒  ░   ▒░▒   ░   ░ ▒ ▒░ ░░   ░▒ ░
   ░    ░ ░  ░  ▒ ░  ░      ░ ░ ░ ▒    ░░   ░      ░      ░ ░ ░ ▒  ░ ░ ░ ▒    ░ ░       ░    ░ ░ ░ ░ ▒   ░    ░  
   ░  ░   ░     ░               ░ ░     ░                     ░ ░      ░ ░      ░  ░    ░          ░ ░   ░    ░  
        ░                                                                                    ░                   
                                                                                                                 
                                           Developed By : Christopher Cooke
										   Dated : 12/19/2016   


Table of Contents
I. Developer Preface
II. Installation
	A. Required Software
	B. Importing
III. Tool Descriptions
	A. Tools
		i. Create
			1. Create Light Wizard
			2. Create Material
			3. Create Prefab
		ii. Tags
			1. Tag Children
			2. Tag Lineage
		iii. Clipboard
			1. Copy				
			2. Cut
			3. Paste
	B. Game Object
		i. Reset Global Transform
		ii. Reset Local Transform
		iii. Reset Lineage Global Transform
		iv. Reset Lineage Local Transform
		v. Reset Lineage Local Transform - Exclude Parent
	C. Editor Settings
		i. Configure Autosave
		ii. Cache
			1. Clear All Cache
			2. Clear Autosaves
			3. Clear Clipboard
			4. Clear Level Template
	D. Project
		i. Create Folders
		ii. Add New Level
			1. Empty Level
			2. From Template
		iii. Restructure Levels
		iv. Create Level Template
			1. From All
			2. From Selected

						     Developer Preface

This tool box was designed to enhance productivity during both the design and development process. From setting up your project initially, to manipulating game
objects, and even allowing designers to store changes made in play mode. Every tool can be found under the new menu item, "Project Tools". While many of the tools
have right-click menu selections as well, not all of them do and some of the menues are context/window dependent. Lastly, not all of the classes included in 
the unity package are tools, and merely serve to assist tools with simple tasks. Some tools use functionality from other tools as well, making some interdependent.
It is recommended to not delete any of the files included in this tool box to maintain functionality.

								Installation

Required Software - Unity 5.0 & Higher. Unknown compatability with lower versions.

Importing - The following steps will enable you to use the Editor Tool Box within your Unity project.

	1 - Save the unity package file, "EditorToolBox", in a destination of your choice. 
	2 - Open Unity
	3 - Open the project in which you'd like to include the EditorToolBox
	4 - On the menu bar, select Assets>Import Package>Custom Package...
	5 - In the following file explorer, select the EditorToolBox from the location previously saved.
	6 - Press Open
	7 - Press Import



						     Tool Descriptions

Tools - Contains the Create, Tags, and Clipboard tools

	Create - Automates simple creation tasks
		Create Light Wizard - Displays a wizard that allows you to create a light by choosing it's properties beforehand.
		Create Material - Creates a material from a selected texture.
		Create Prefab - Creates a prefab from a selected game object (used in multiple classes)

	Tags - Allows you to immediate tag related game objects.
		Tag Children - Assigns the selected game objects tag to it's immediate children.
		Tag Lineage - Assigns the selected game objects tag to it's immediate children and all children that follow.

	Clipboard - Allows your to copy/cut/paste any selection of game objects from play or edit mode. Can be used to save editing changes made in play mode.
		Copy - Can be used to copy the selection or all game objects
		Cut - Can be used to cut the selection or all game objects
		Paste - Can paste into current scene or can paste into new scene (calls add new level)

		
Game Object - Allows you to manipulate the transforms of any selection of game objects

	Reset Global Transform - Sets the transform of the selected game object to (0,0,0) in global space

	Reset Local Transform - Sets the transform of the selected game object to (0,0,0) in local space

	Reset Lineage Global Transform - Sets the transform of the selected game object and all attached game objects to (0,0,0) in global space

	Reset Lineage Local Transform - Sets the transform of the selected game object and all attached game objects to (0,0,0) in local space

	Reset Lineage Local Transform Excluding Parent - Sets the transform of all attached game objects to (0,0,0) in local space


Editor Settings - Allows for configuring the autosave feature and clearing out resource files

	Configure Autosave - Displays a custom inspector that allows you to enable/disable autosave, set the autosave time interval in minutes and hours, and to restore default settings.
	Cache - This item's submenu allows you to clear out resource files created by this tool.
		
		Clear All Cache - Runs all three clear operations
		
		Clear Autosaves - Recursively deletes and recreates /Assets/Scenes/Autosaves/

		Clear Clipboard - Recursively deletes and recreates /Assets/Editor/Resources/Clipboard/

		Clear Level Template - Recursively deletes and recreates /Assets/Editor/Resources/Level Template/
		

Project - Allows for the creation of simple project related items

	Create Folders - Creates a number of misc. named folders believed to be good for organizing your project (used in multiple classes)
	Add New Level - Creates a new level in directory /Assets/Scenes/Levels/ with a counted name eg. Level_XX
		
	From Empty Level - Creates the new scene with nothing in it

	From Template - Uses the user defined level template to create a level with those objects instantiated
	
	Restructure Levels - If counted scenes get deleted for any reason, restructure levels renames all the scenes in /Assets/Scenes/Levels/ using the default
						 counted format

	Create Level Template - These options allow the user to define the level template

	From All - Template based on all game objects in the current scene

	From Selection - Template based on the current selection of game objects

