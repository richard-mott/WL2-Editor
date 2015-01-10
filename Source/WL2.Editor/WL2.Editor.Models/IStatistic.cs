namespace WL2.Editor.Models
{
    public interface IStatistic
    {
        string Name { get; }
        string Image { get; }
        int CurrentValue { get; set; }
        bool IsDirty { get; }
        void Save();
    }
}
