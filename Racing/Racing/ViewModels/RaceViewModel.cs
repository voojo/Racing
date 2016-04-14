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
using System.Collections.ObjectModel;

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

        private void NotifyAboutXLineChange(Random random)
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                _cars[i].Move(random);
                CheckBonus(_cars[i]);
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

        private void CheckBonus(Car car)
        {
            Bonus bonusToDelete = null;
            foreach (var bonus in Bonuses)
            {
                if (bonus.PositionY == car.ActualCoordinateY)
                    if (car.ActualCoordinateX + car.CarLength <= bonus.PositionX + bonus.Length + 30 &&
                        car.ActualCoordinateX + car.CarLength >= bonus.PositionX + 30)
                    {
                        car.ActualCoordinateX += bonus.GetExtraSpeed();
                        bonusToDelete = bonus;
                    }
            }
            if (bonusToDelete != null)
                Bonuses.Remove(bonusToDelete);
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

        public ObservableCollection<Bonus> Bonuses { get; set; }

        public RaceViewModel()
        {
            StartButtonIsVisible = true;
            _finishLineCoordinateX = 1100;
            _startLineCoordinateX = 3;
            _track = new Track() { StartLineCoordinateX = _startLineCoordinateX, FinishLineCoordinateX = _finishLineCoordinateX };
            _cars = new Car[4];
            _cars[0] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 10, Name = "Pomaranczowe Auto" };
            _cars[1] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 90, Name = "Niebieskie Auto" };
            _cars[2] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 150, Name = "Zolte Auto" };
            _cars[3] = new Car() { ActualCoordinateX = _track.StartLineCoordinateX, ActualCoordinateY = 230, Name = "Czerwone Auto" };

            StartRaceCommand = new RelayCommand(Move);

            Bonuses = new ObservableCollection<Bonus>();
        }
        public ICommand StartRaceCommand { get; set; }

        public async void Move(object parameter)
        {
            Random random = new Random();
            HideStartButton();

            while (!_cars.Any(x => x.IfWin(_finishLineCoordinateX)))
            {
                await Task.Delay(30);
                NotifyAboutXLineChange(random);
                RandomBonus(random);
            }

            ShowMessageBoxForWinner();
            MoveCarsToStartLine();
            ClearBonusesList();
            ShowStartButton();
        }

        private void StaticBonus()
        {
            Banana banana = new Banana(_finishLineCoordinateX, new Random());
            banana.PositionX = 500;
            banana.PositionY = 90;
            Bonuses.Add(banana);
        }

        private void ClearBonusesList()
        {
            Bonuses.Clear();
        }

        private void RandomBonus(Random random)
        {
            var randomNumber = random.Next(0, 80);
            if (randomNumber != 5)
                return;

            randomNumber = random.Next(0, 2);

            if (randomNumber == 0)
                Bonuses.Add(new Banana(_finishLineCoordinateX, random));
            else
                Bonuses.Add(new Coin(_finishLineCoordinateX, random));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
