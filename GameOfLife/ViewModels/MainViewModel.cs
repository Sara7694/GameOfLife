using GameOfLife.Model;
using GameOfLife.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : BindableBase
    {
        CellGrid _cellGrid;

        private ObservableCollection<Cell> _cells;

        public ObservableCollection<Cell> Cells 
        {
            get => _cells;
            private set => SetProperty(ref _cells, value);
        }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public DelegateCommand ClearCommand { get; set; }

        public DelegateCommand StartSimulationCommand { get; set; }

        public DelegateCommand SimulateOneStepCommand { get; set; }

        public DelegateCommand RandomPopulationCommand { get; set; }

        public MainViewModel()
        {
            _cellGrid = new CellGrid(10,10);
            Cells = _cellGrid.Grid;
            Rows = _cellGrid.Rows;
            Columns = _cellGrid.Columns;
            ClearCommand = new DelegateCommand(() => _cellGrid.ClearGrid());
            RandomPopulationCommand = new DelegateCommand(() => _cellGrid.FillGridRandom(0.3));
            SimulateOneStepCommand = new DelegateCommand(() => Cells = _cellGrid.SimulateOneStep());
        }
    }
}
