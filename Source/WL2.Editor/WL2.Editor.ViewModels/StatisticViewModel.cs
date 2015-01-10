using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Assisticant;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class StatisticViewModel
    {
        private readonly IStatistic _statistic;
        private readonly int _minimumValue = 0;
        private readonly int _maximumValue = 10;

        public StatisticViewModel(IStatistic statistic, int minimumValue, int maximumValue)
        {
            _statistic = statistic;
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }

        public string Name
        {
            get { return _statistic.Name; }
        }

        public string Image 
        {
            get { return Path.Combine(Environment.CurrentDirectory, "Images", _statistic.Image); }
        }

        public int CurrentValue
        {
            get { return _statistic.CurrentValue; }
            set { _statistic.CurrentValue = value; }
        }

        public ICommand Increment
        {
            get
            {
                return MakeCommand
                    .When(() => CurrentValue < _maximumValue)
                    .Do(() =>
                    {
                        CurrentValue++;
                    });
            }
        }

        public ICommand Decrement
        {
            get
            {
                return MakeCommand
                    .When(() => CurrentValue > _minimumValue)
                    .Do(() =>
                    {
                        CurrentValue--;
                    });
            }
        }
    }
}
