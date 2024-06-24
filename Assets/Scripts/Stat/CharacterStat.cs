[System.Serializable]
public class CharacterStat
{
    public int Luck;
    public int Bonus;

    public CharacterStat(int luck, int bonus)
    {
        this.Luck = luck;
        this.Bonus = bonus;
    }
}