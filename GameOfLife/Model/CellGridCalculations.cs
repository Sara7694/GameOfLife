using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameOfLife.Model
{
    public class CellGridCalculations
    {

        /// <summary>
        /// Method that fills the grid with Random values
        /// </summary>
        /// <param name="RandomFactor">Factor the percentage of live cells</param>
        public void FillGridRandom(double RandomFactor, ObservableCollection<Cell> Grid)
        {
            var random = new Random();
            foreach (var cell in Grid)
            {
                var randomDouble = random.NextDouble();
                cell.Alive = randomDouble < RandomFactor;
            }
        }

        /// <summary>
        /// sets all cells in field to not alive
        /// </summary>
        public void ClearGrid(ObservableCollection<Cell> Grid)
        {
            foreach ( var cell in Grid)
            {
                cell.Alive = false;
            }
        }

        public ObservableCollection<Cell> SimulateOneStep(ObservableCollection<Cell> Grid, int Rows, int Columns)
        {
            ObservableCollection<Cell> newGrid = new ObservableCollection<Cell>();
            for(int rowIndex = 0; rowIndex < Rows; rowIndex ++)
            {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
                {
                    int index = GetGridIndex(rowIndex, columnIndex, Rows, Columns);
                    int numberOfLifeNeighbours = GetNumberOfAliveNeighbours(Grid, rowIndex, columnIndex, Rows, Columns);
                    if (!Grid[index].Alive)
                    {
                        if(numberOfLifeNeighbours == 3)
                        {
                            newGrid.Add(new Cell() { Alive = true });
                        }
                        else
                        {
                            newGrid.Add(new Cell() { Alive = false });
                        }
                    }
                    else
                    {
                        switch(numberOfLifeNeighbours)
                        {
                            case 2:
                            case 3:
                                newGrid.Add(new Cell() { Alive = true });
                                break;
                            default:
                                newGrid.Add(new Cell() { Alive = false });
                                break;

                        }
                    }
                }
            }
            return newGrid;
        }

        private int GetNumberOfAliveNeighbours(ObservableCollection<Cell> Grid, int cellRow, int cellColumn, int numberOfRows, int numberOfColumns)
        {
            int numberOfAliveNeighbours = 0; 
            for (int rowIndex = cellRow - 1; rowIndex < cellRow + 2; rowIndex ++)
            {
                for (int columnIndex = cellColumn -1; columnIndex < cellColumn + 2; columnIndex ++ )
                {
                    if (columnIndex == cellColumn && rowIndex == cellRow)
                    {
                        continue;
                    }
                    //catch out of Bounds 
                    if(columnIndex < 0 || rowIndex < 0 || columnIndex >= numberOfColumns || rowIndex >= numberOfRows )
                    { 
                        continue;
                    }

                    try
                    {
                        var index = GetGridIndex(rowIndex, columnIndex,  numberOfRows, numberOfColumns);
                        if(Grid[index].Alive)
                        {
                            numberOfAliveNeighbours++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                    }
                }
            }
            return numberOfAliveNeighbours;
        }



        private int GetGridIndex(int rowIndex, int columnIndex, int numberOfRows, int numberOfColumns)
        {
            if (rowIndex >= numberOfRows || columnIndex >= numberOfColumns|| rowIndex < 0 || columnIndex < 0)
                throw new IndexOutOfRangeException();
            return rowIndex * numberOfRows + columnIndex;
        }

        public ObservableCollection<Cell> GenerateGrid(int Rows, int Columns)
        {
            var Grid = new ObservableCollection<Cell>();
            for (int indexRows = 0; indexRows < Rows; indexRows++)
            {
                for (int indexColumns = 0; indexColumns < Columns; indexColumns ++)
                {
                    Grid.Add(new Cell() { Alive = false});
                }
            }
            return Grid;
        }


    }
}
