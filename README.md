# TO-Toolbox - A  Prototype Content Editor Multi-tool

## Big Thank You to xtridence and PyroSamurai for the original tool!

* [This tool](https://github.com/TricksterOnline/TO-Toolbox) was originally by xtridence and PyroSamurai. 

* This version has been modified and extended to support additional features,
	files, and workflows in the pursuit of providing accessibility to the maintenance
	and management of S2 Servers.

* NRI file viewing works for all vanilla .nri files

* [TNT](http://github.com/TricksterOnline/TNT), a tool also by PyroSamurai, is used
  	primarily for NRI extraction.
  
* [How to use the tool to edit libconfig](datEditing.md)

## How to build:​

1. Have **Visual Studio 2022** and **.NET 4.8** installed
2. Clone this repository, and extract the downloaded .zip in a new folder
3. Open the **CaballaRE.sln** solution
4. **Build** the solution (**Ctrl+Shift+B**)
5. Open the .exe inside the **CaballaRE/bin/Debug** folder
6. Enjoy!

# Now with:

- Cell Search
- Dedicated Libcmgds & rch.xml Editor
- Filtered export options (CSV & XML) 
- Right Click options that apply to multiple rows:
	* Cut
	* Copy
	* Paste
	* Delete
	* Add Row Above
	* Add Row Below (Hidden-hold shift!)
	* Add Column Left
	* Add Column Right (Hidden- shift!)
	* "Fill Down" (persist data down a column)
	* "Fill Up" (Hidden-hold shift!)
	* "Fill Right" (persist data left in a row)
	* "Fill Left" (Hidden-hold shift!)
- Fill "Down" Data so you can persist data between rows
- Many new ways to select, modify, and update your information
- A BUNCH of key commands to help streamline your workflow
- Did I forget to mention there's key commands to CREATE custom tables 
	in your libconfig? ;)

## New and updated Key Commands:

-- **File Management** --
	
	* Ctrl+O -> Open
	* Ctrl+S -> Save (opens export menu, use arrow keys or further key commands): 
		- x -> Regular xml
		- m -> Filtered xml
		- d -> Dat (unencrypted)
		- e -> Dat (encrypted)
		- c -> CSV (full table)
		- v -> CSV (filtered table)
		- i -> IDX
	* Ctrl+Shift+S -> Quick save over the original XML
	* Ctrl+U -> Update Table
	* Ctrl+M -> Import table
	* Ctrl+L -> Localization Helper
	
-- **Content Management** --
	
	* Ctrl+C -> Copy -> OGs had it first ;) )
	* Ctrl+V -> Paste -> Smart! Strips <![CDATA[ ]]> for you!
	* Ctrl+X -> Cut 
	* Del -> Delete cell contents
	* Ctrl+Z -> Undo
	* Ctrl+Y -> Redo / Repeat (if no undo's were done)
	
-- **Advanced Content Management** --
	
	* Ctrl+D -> Repeat Value down until next stated value (AKA Fill Down)
	* Ctrl+Shift+D -> Repeat Value up until next stated value (AKA Fill Up)
	* Ctrl+R -> Repeat Value right until next stated value (AKA Fill Right)
	* Ctrl+Shift+R -> Repeat Value left until next stated value (Fill Left)
	* Ctrl+T -> New Table (Libconfig only)
	* Ctrl+B -> Rename currently selected Table (Libconfig only)
	
-- **Selection** --
	
	* Ctrl+A -> Select all (xtridence and PyroSamurai had it first ;) )
	* Ctrl+Spacebar -> Select current Column
	* Shift+Spacebar -> Select current Row
	* Shift+Pg Down -> Select all from selected to last cell in Column
	* Shift+Pg Up -> Select all from selected to first cell in Column
	* Shift+End -> Select all from selected to last cell in Row
	* Shift+Home -> Select all from selected to first cell in Row
	* Shift+Arrow Keys -> Extend selection of cells by one Cell
	* Ctrl+Shift+Arrow Keys -> Extend selection to last nonblank Cell
	
-- **Cell & Table Navigation** --
	
	* Ctrl+F -> Jump to Find (or switch between the two types)
	* Ctrl+H -> Find & Replace (or switch between the two options--table only)
	* Ctrl+Home -> Move to the top-left corner of the table (Thanks xtri+Pyro!)
	* Ctrl+End -> Move to last cell (Thanks xtri+Pyro!)
	* Ctrl+Pg Down -> Go to the next listed table (Thanks xtri+Pyro!)
	* Ctrl+Pg Up -> Go to the previously listed table (Thanks xtri+Pyro!)
	
-- **Menu Navigation** --
	
	I remapped these to make table migration easier.
	
	* Ctrl+Shift+Pg Down -> Switch to the next menu (e.g. NRI Viewer -> DAT Viewer)
	* Ctrl+Shift+Pg Up -> Switch to previous menu (e.g. DAT Viewer -> NRI Viewer)
	* Ctrl+Shift+N -> NRI Viewer
	* Ctrl+Shift+E -> Dat Viewer
	* Ctrl+Shift+L -> Libconfig Editor
	* Ctrl+Shift+I -> Libcmgds & Rch Editor
	
All to help better manage your S2 server!

**Note**: This branch has been vibe-coded, so there may be bugs!
	Feel free to submit a report, I may fix it.
	
--------------------------------------------------------------------------------
	
Thanks for everything, Trickster Online. I can only hope to give back the years
	of joy you've given me, throughout everything.
	
	Good luck and Godspeed!
