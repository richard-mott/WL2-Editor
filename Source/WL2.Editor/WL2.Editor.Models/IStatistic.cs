namespace WL2.Editor.Models
{
    public interface IStatistic
    {
        string Name { get; }
        int CurrentValue { get; set; }

        void Save();
    }
}
