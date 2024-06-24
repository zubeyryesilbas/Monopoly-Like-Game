public class CharacterManager : ICharacterManager
{
    private CharacterType _charcterType;
   
    private CharacterStat _characterStat;
    public CharacterType CharacterType => _charcterType;
    public CharacterStat CharacterStat => _characterStat;

    public void UpdateCharacter(CharacterType charcterType , CharacterStat characterStat)
    {
        _charcterType = charcterType;
        _characterStat = characterStat;
    }
}