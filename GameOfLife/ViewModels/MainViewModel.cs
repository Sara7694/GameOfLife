using GameOfLife.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : BindableBase
    {
        CellGridCalculations calculations;

        private ObservableCollection<Cell> _cells;
        private string _simualtionText; 

        private bool SimulationRunning;

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

        public String SimulationButtonText 
        {
            get => _simualtionText;
            set => SetProperty(ref _simualtionText, value); 
        }

        public MainViewModel()
        {
            
            calculations = new CellGridCalculations();
            Rows = 10;
            Columns = 10;
            SimulationButtonText = " Start Simulation";

            Cells = calculations.GenerateGrid(Rows, Columns);
            ClearCommand = new DelegateCommand(() => calculations.ClearGrid(Cells));
            RandomPopulationCommand = new DelegateCommand(() => calculations.FillGridRandom(0.3, Cells));
            SimulateOneStepCommand = new DelegateCommand(() => Cells = calculations.SimulateOneStep(Cells, Rows, Columns));
            StartSimulationCommand = new DelegateCommand(() => StartSimulationAsync());
        }

        private async Task StartSimulationAsync()
        {
            SimulationRunning = !SimulationRunning;
            SimulationButtonText = SimulationRunning ? "Stop Simulation" : "Start Simulation";
            while(SimulationRunning)
            {
                Cells = calculations.SimulateOneStep(Cells, Rows, Columns);
                await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(false);
            }
        }
    }
}
