public struct ObjectID
{
    public string Namespace;
    public string Name;
    public string AsString => $"{Namespace}:{Name}";
    public ObjectID(string Namespace, string Name)
    {
        this.Namespace = Namespace;
        this.Name = Name;
    }
    public ObjectID StringToItemID(string ItemID)
    {
        string[] sep = ItemID.Split(':');
        if (sep.Length != 2)
        {
            throw new System.Exception("Attempted to parse malformed ObjectID.");
        } else
        {
            return new ObjectID(sep[0], sep[1]);
        }
    }
}