using GameOfLife.Model.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameOfLife.Model
{
    public class CellGrid : BindableBase
    {
        private ObservableCollection<Cell> _grid;

        private int _rows;
        private int _columns;

        public int Rows
        {
            get => _rows;
            private set => _rows = value;
        }
        public int Columns
        {
            get => _columns;
            private set => _columns = value;
        }
        public ObservableCollection<Cell> Grid
        {
            get => _grid;
            private set => SetProperty(ref _grid, value);
        }
        public CellGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns; 
            FillGrid();
        }

        /// <summary>
        /// Method that fills the grid with Random values
        /// </summary>
        /// <param name="RandomFactor">Factor the percentage of live cells</param>
        public void FillGridRandom(double RandomFactor)
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
        public void ClearGrid()
        {
            foreach ( var cell in Grid)
            {
                cell.Alive = false;
            }
        }

        public ObservableCollection<Cell> SimulateOneStep()
        {
            ObservableCollection<Cell> newGrid = new ObservableCollection<Cell>();
            for(int row = 0; row < Rows; row ++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    int index = GetGridIndex(row, column);
                    int numberOfLifeNeighbours = GetNumberOfAliveNeighbours(row, column);
                    if (!Grid[index].Alive && numberOfLifeNeighbours == 3)
                    {
                        newGrid.Add(new Cell() { Alive = true });
                    }
                    else
                    {
                        newGrid.Add(new Cell() { Alive = false });
                    }
                    if(Grid[index].Alive)
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
            Grid = newGrid;
            return Grid;
        }

        private int GetNumberOfAliveNeighbours(int row, int column)
        {
            int index = 0;
            int numberOfAliveNeighbours = 0; 
            for (int rowIndex = row - 1; rowIndex < row + 2; rowIndex ++)
            {
                for (int columnIndex = column -1; columnIndex < column + 2; columnIndex ++ )
                {
                    if (columnIndex == column && rowIndex == row)
                    {
                        continue;
                    }

                    try
                    {
                        index = GetGridIndex(rowIndex, columnIndex);
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



        /// <summary>
        /// method to set a certain cell to a certain value
        /// </summary>
        /// <param name="alive">bool if the cell should be alive or dead</param>
        /// <param name="rowIndex">RowIndex of cell</param>
        /// <param name="columnIndex">ColumnIndex of cell</param>
        private void SetCell(bool alive, int rowIndex, int columnIndex)
        {
            var gridIndex = GetGridIndex(rowIndex, columnIndex);
            Grid[gridIndex].Alive = alive;
        }


        private int GetGridIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex >= Rows || columnIndex >= Columns|| rowIndex < 0 || columnIndex < 0)
                throw new IndexOutOfRangeException();
            return rowIndex * Rows + columnIndex;
        }

        private void FillGrid()
        {
            Grid = new ObservableCollection<Cell>();
            for (int indexRows = 0; indexRows < Rows; indexRows++)
            {
                for (int indexColumns = 0; indexColumns < Columns; indexColumns ++)
                {
                    Grid.Add(new Cell() { Alive = false});
                }
            }
        }


    }
}
