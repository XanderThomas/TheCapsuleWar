
[System.Serializable]
public struct ResourceTypes {
    
    public int gold;
    public int wood;
    public int iron;
	


    public ResourceTypes(int gold, int wood, int iron)
    {
        this.gold = gold;
        this.wood = wood;
        this.iron = iron;
    }

    public static ResourceTypes operator + (ResourceTypes a, ResourceTypes b)
    {
        return new ResourceTypes(a.gold + b.gold, a.wood + b.wood, a.iron + b.iron);
    }

    public static ResourceTypes operator - (ResourceTypes a)
    {
        return new ResourceTypes(-a.gold, -a.wood, -a.iron);
    }

}
