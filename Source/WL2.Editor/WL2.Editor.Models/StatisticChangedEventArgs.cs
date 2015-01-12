namespace WL2.Editor.Models
{
    public class StatisticChangedEventArgs
    {
        private readonly int _oldValue;
        private readonly int _newValue;

        public StatisticChangedEventArgs(int oldValue, int newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public int OldValue
        {
            get { return _oldValue; }
        }

        public int NewValue
        {
            get { return _newValue; }
        }
    }
}
