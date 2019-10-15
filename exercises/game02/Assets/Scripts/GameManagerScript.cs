using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
    
    public GameObject cellPrefab;
    int gridWidth = 10;
    int gridHeight = 10;
    float cellDimension = 3.3f; //Size of cell
    float cellSpacing = 0.2f; //Space bw cells
    public CellScript[,] grid; //2D array of cell scripts 
    float generationRate = 1f;
    float generationTimer;
    int time = 0;
    bool simulate = false;
    public int aliveCounter = 0;
    public Text alertText;

    public int tresCounter = 0;
    public GameObject treasurePrefab;
    public int maxX = 0; //x position of highest cell 
    public int maxY = 0; //y position of highest cell

    // Start is called before the first frame update
    void Start() {
        //Instantiating 2D array for cell scripts 
        grid = new CellScript[gridWidth, gridHeight];

        /* Using nested for loops, instantiate cubes with cell scripts in a way such that
        each cell will be places in a top left oriented coodinate system.
        The top left cell will have the x, y coordinates of (0,0), and the bottom right will
        have the coodinate (gridWidth-1, gridHeight-1).
        */
        for (int x=0; x<gridWidth; x++) {
            for (int y=0; y<gridHeight; y++) {

                //Create a cube, position/scale it, add the CellScript to it.
                Vector3 pos = new Vector3(x * (cellDimension + cellSpacing), 0, y * (cellDimension + cellSpacing));

                //Creates a new game object like we do in the Unity engine, but in code form
                GameObject cellObj = Instantiate(cellPrefab, pos, Quaternion.identity);

                //Casting the cell script from the Unity engine, type of component 
                CellScript cs = cellObj.AddComponent<CellScript>();
                cs.x = x;
                cs.y = y;

                //Question ?: true - do first thing, false - do the second thing 
                cs.alive = (Random.value > 0.5f) ? true : false;

                cs.updateColor();

                //Position
                cellObj.transform.position = pos;
                cellObj.transform.localScale = new Vector3(cellDimension, cellDimension, cellDimension);
                
                grid[x, y] = cs; //adding it to 2D array
            }
        }
        generationTimer = generationRate;
        alertText.text = "Step onto the white and press toggle.";
    }

    // Update is called once per frame
    void Update() {
        int c = 0;
        generationTimer -= Time.deltaTime; 
        if (generationTimer < 0 && simulate) {
            //Generate next state
            c = generate();
            if (c>0) {
                findTreasure();
            }
            //Reset timer 
            generationTimer = generationRate;
        }
    }

    int generate() {
        //How many times the simulation has generated a new state
        time++;
        aliveCounter++;
        int numAliveNow = 0;

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                List<CellScript> liveNeighbors = gatherLiveNeighbors(x, y);

                //A new cell appears on top of the current cell if it's alive 
                if (grid[x,y].alive) {
                    Vector3 newPos = new Vector3(grid[x,y].x * (cellDimension + cellSpacing), aliveCounter * (cellDimension + cellSpacing), grid[x, y].y * (cellDimension + cellSpacing));
                    GameObject newCellObj = Instantiate(cellPrefab, newPos, Quaternion.identity);
                    CellScript newCS = newCellObj.AddComponent<CellScript>();
                    newCS.x = grid[x, y].x;
                    newCS.y = grid[x, y].y;
                    newCS.alive = (Random.value > 0.5f) ? true : false;
                    newCS.updateColor();
                    newCellObj.transform.position = newPos;
                    newCellObj.transform.localScale = new Vector3(cellDimension, cellDimension, cellDimension);
                    grid[x, y] = newCS;

                    maxX = grid[x, y].x;
                    maxY = grid[x, y].y;
                }                             

                //Rules of the Game of Life 

                //1) Any live cell with fewer than two live neighbors dies, as if caused by underpopulation.
                if (grid[x, y].alive && liveNeighbors.Count < 2) {
                    grid[x, y].nextAlive = false;
                }

                //2) Any live cell with 2 or 3 live neighbors lives on to the next generation.
                else if (grid[x, y].alive && (liveNeighbors.Count == 2 || liveNeighbors.Count == 3)) {
                    grid[x, y].nextAlive = true;
                }

                //3) Any live cell with more than 3 live neighbors dies, as if by overpopulation.
                else if (grid[x, y].alive && liveNeighbors.Count > 3) {
                    grid[x, y].nextAlive = false;
                }

                //4) Any dead cell with exactly 3 live neighbors becomes a live cell, as if by reproduction.
                else if (!grid[x, y].alive && liveNeighbors.Count == 3) {
                    grid[x, y].nextAlive = true;
                }
            }
        }

        /* Now that we have looped through all of the cells in the grid, and stored what their alive status should
		be in each cell's nextAlivevariable, update each cell's alive state to be that value.
		*/
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                grid[x, y].alive = grid[x, y].nextAlive;
                if (grid[x, y].alive) {
                    numAliveNow++;
                }
            }
        }

        return numAliveNow;
    }

    List<CellScript> gatherLiveNeighbors(int x, int y) {
        List<CellScript> neighbors = new List<CellScript>();

        for (int i = Mathf.Max(0,x-1); i <= Mathf.Min(gridWidth-1, x+1); i++) {
            for (int j = Mathf.Max(0, y - 1); j <= Mathf.Min(gridHeight - 1, y + 1); j++) {
                if (grid[i,j].alive && !(i==x && j==y)) {
                    neighbors.Add(grid[i, j]);
                }
            }
        }
        return neighbors;
    }

    //Called by the UI toggle's event system 
    public void toggleSimulate (bool value) {
        simulate = value;
    }

    //Game begins when the game of life stops 
    void findTreasure() {
        alertText.text = "Climb through life to find the treasures!";

        Vector3 posTreasure = new Vector3(maxX * (cellDimension + cellSpacing), (aliveCounter + 1) * (cellDimension + cellSpacing), maxY * (cellDimension + cellSpacing));
        GameObject obj = Instantiate(treasurePrefab, posTreasure, Quaternion.identity);
        TreasureScript tres = obj.AddComponent<TreasureScript>();
        obj.transform.position = posTreasure;
        tresCounter++;
    }
}