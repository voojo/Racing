using Racing.Commands;
using Racing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Racing.ViewModels
{
    class RaceViewModel : INotifyPropertyChanged
    {
        private Track _track;
        private Car[] _cars;
        private int _startLineCoordinateX;
        private int _finishLineCoordinateX;
        public int OrangeCarActualPositionX
        {
            get
            {
                return _cars[0].ActualCoordinateX;
            }
        }
        public int BlueCarActualPositionX
        {
            get
            {
                return _cars[1].ActualCoordinateX;
            }
        }
        public int YellowCarActualPositionX
        {
            get
            {
                return _cars[2].ActualCoordinateX;
            }
        }
        public int RedCarActualPositionX
        {
            get
            {
                return _cars[3].ActualCoordinateX;
            }
        }



        public int OrangeCarActualPositionY
        {
            get
            {
                return _cars[0].ActualCoordinateY;
            }
        }

        public int BlueCarActualPositionY
        {
            get
            {
                return _cars[1].ActualCoordinateY;
            }
        }
        public int YellowCarActualPositionY
        {
            get
            {
                return _cars[2].ActualCoordinateY;
            }
        }
        public int RedCarActualPositionY
        {
            get
            {
                return _cars[3].ActualCoordinateY;
            }
        }

        private void ShowStartButton()
        {
            StartButtonIsVisible = true;
            NotifyPropertyChanged("StartButtonIsVisible");
        }
        private void HideStartButton()
        {
            StartButtonIsVisible = false;
            NotifyPropertyChanged("StartButtonIsVisible");
        }

        private void NotifyAboutXLineChange()
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                _cars[i].Move();
                if (i == 0)
                    NotifyPropertyChanged("OrangeCarActualPositionX");
                if (i == 1)
                    NotifyPropertyChanged("BlueCarActualPositionX");
                if (i == 2)
                    NotifyPropertyChanged("YellowCarActualPositionX");
                else
                    NotifyPropertyChanged("RedCarActualPositionX");
            }
        }
        public void ShowMessageBoxForWinner()
        {
            string winnerText = "Wygrało ";
            foreach (var car in _cars)
            {
                if (car.IsWinner)
                    winnerText += car.Name;
            }
            MessageBox.Show(winnerText);
        }

        public bool StartButtonIsVisible { get; set; }

        public void MoveCarsToStartLine()
        {
            foreach (var car in _cars)
            {
                car.MoveToStarLine(_startLineCoordinateX);
                NotifyPropertyChanged("OrangeCarActualPositionX");
                NotifyPropertyChanged("BlueCarActualPositionX");
                NotifyPropertyChanged("YellowCarActualPositionX");
                NotifyPropertyChanged("RedCarActualPositionX");
            }
        }



        public RaceViewModel()
        {
            StartButtonIsVisible = true;
            _finishLineCoordinateX = 1100;
            _startLineCoordinateX = 3;
            _track = new Track() { StartLineCoordinateX = _startLineCoordinateX, FinishLineCoordinateX = _finishLineCoordinateX };
            _cars = new Car[4];
            _cars[0] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 12, Name = "Pomaranczowe Auto" };
            _cars[1] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 80, Name = "Niebieskie Auto" };
            _cars[2] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 157, Name = "Zolte Auto" };
            _cars[3] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 228, Name = "Czerwone Auto" };

            StartRaceCommand = new RelayCommand(Move);


        }
        public ICommand StartRaceCommand { get; set; }

        public async void Move(object parameter)
        {
            HideStartButton();
            while (!_cars.Any(x => x.IfWin(_finishLineCoordinateX)))
            {
                await Task.Delay(2);
                NotifyAboutXLineChange();
            }
            ShowMessageBoxForWinner();
            MoveCarsToStartLine();
            ShowStartButton();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
