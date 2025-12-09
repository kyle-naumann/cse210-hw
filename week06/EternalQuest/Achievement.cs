public class Achievement
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsEarned { get; set; }

    public Achievement(string name, string description)
    {
        Name = name;
        Description = description;
        IsEarned = false;
    }
}