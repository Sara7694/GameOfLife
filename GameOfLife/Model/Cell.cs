using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Model
{
    public class Cell : BindableBase
    {
        private bool _alive; 

        public bool Alive
        {
            get => _alive;
            set => SetProperty(ref _alive, value);
        }

        public DelegateCommand ButtonClickCommand { get; set; }

        public Cell()
        {
            ButtonClickCommand = new DelegateCommand(() => Alive = !Alive);
        }
    }
}
