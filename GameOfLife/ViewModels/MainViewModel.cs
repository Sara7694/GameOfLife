using GameOfLife.Model;
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
        CellGridCalculations calculations;

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
            
            calculations = new CellGridCalculations();
            Rows = 10;
            Columns = 10; 

            Cells = calculations.GenerateGrid(Rows, Columns);
            ClearCommand = new DelegateCommand(() => calculations.ClearGrid(Cells));
            RandomPopulationCommand = new DelegateCommand(() => calculations.FillGridRandom(0.3, Cells));
            SimulateOneStepCommand = new DelegateCommand(() => Cells = calculations.SimulateOneStep(Cells, Rows, Columns));
        }
    }
}
