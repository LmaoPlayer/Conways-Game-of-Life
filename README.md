# Conway's Game of Life

> ## What is Conway's Game of Life?
>
> ### It's a simulation of cells with a few rules:
> 1. A cell can *spawn* with 3 living cells surrounding it.
> 2. A cell *stays alive* when there are 2 or 3 living cells around it.
> 3. A cell *dies* when there less than 2 cells alive around it or when there are more than 3 alive around it.

> ## Controls:
> 
> - A Start button to start the simulation
> - A Stop button to stop the simulation, this only appears when start is clicked.
> - A speed up button to let the simulation run nearly double as fast.
> - A slow down button to let the simulation run slower, just like the stop button, this only appears when speed up is clicked.
> - A Reset button to clear out the field. This also will stop the simulation if it is running.
> - A combo box is added which contains a few shapes. To use it, select the shape you want to place down and then press the "Place" button.
>    - (see ['A Few Extras'](#a-few-extras) for more details on these shapes and how to add or remove them.)
> - Clicking the field will draw or delete the cell you've clicked on.


> ## A Few Extras:
>
> - You can add extra shapes can be added by inserting files in ConwaysGameOfLifeButInAPictureBox\bin\Debug
> These files however need to end with .txt, all that comes infront of it don't matter.
>> For a quick example of files, you can find them at [LifeWiki](https://conwaylife.com/wiki "A big website that contains a lot of shapes"), click on the links in the top right corner of the Wiki, select a shape you wish for, open the "Pattern files" on the sidebar and download the Plaintext.
> - If this is done while the game is launched, you need to press the "Reload" button.
> - You can also delete the file that you have selected in the combo box.
> - Lastly, the field has connected borders, which means that cells can interact with eachother at the other end of the field, like PacMan.

