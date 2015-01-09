using System.Windows.Input;
using Assisticant;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class StatisticViewModel
    {
        private readonly IStatistic _statistic;
        
        public StatisticViewModel(IStatistic statistic)
        {
            _statistic = statistic;
            IsDirty = false;
        }

        public string Name
        {
            get { return _statistic.Name; }
        }

        public int CurrentValue
        {
            get { return _statistic.CurrentValue; }
            set { _statistic.CurrentValue = value; }
        }

        public bool IsDirty { get; private set; }

        public ICommand Increment
        {
            get
            {
                return MakeCommand
                    .When(() => CurrentValue < 10)
                    .Do(() =>
                    {
                        CurrentValue++;
                        IsDirty = true;
                    });
            }
        }

        public ICommand Decrement
        {
            get
            {
                return MakeCommand
                    .When(() => CurrentValue > 0)
                    .Do(() =>
                    {
                        CurrentValue--;
                        IsDirty = true;
                    });
            }
        }

        public void Save()
        {
            if (IsDirty)
            {
                _statistic.Save();
            }
        }
    }
}
