using UnityEngine;
public class Loot : MonoBehaviour
{
    public GameObject HEART, DOUBLEHEART, HEARTCANISTER;
    public GameObject DecideDrop(LootTable table)
    {
        Reward reward = ParseTable(table);
        return ParseReward(reward);
    }
    private GameObject ParseReward(Reward reward)
    {
        return null;
    }
    private Reward ParseTable(LootTable table)
    {
        int value;
        switch (table)
        {
            case LootTable.NONE:
                return Reward.NONE;
            case LootTable.EASYROOM:
                value = Random.Range(0, 1);
                // if value = 0 return none, otherwise return heart
                return value == 0 ? Reward.NONE : Reward.HEART;
            case LootTable.HARDROOM:
                value = Random.Range(0, 10);
                if (ValueWithinRange(value, 0, 2)) return Reward.HEARTCANISTER;
                else if (ValueWithinRange(value, 3, 5)) return Reward.DOUBLEHEART;
                else if (ValueWithinRange(value, 6, 10)) return Reward.HEART;
                return Reward.NONE;
            default:
                return Reward.NONE;
        }
    }
    public enum LootTable
    {
        NONE, // Default table, doesn't drop anything
        EASYROOM, // 50% heart
        HARDROOM, // 20% heart canister, 50% heart, 30% double heart
    }
    public enum Reward
    {
        NONE,
        HEART,
        DOUBLEHEART,
        HEARTCANISTER
    }
    // return true when variable value is >= minimum and <= maximum
    private bool ValueWithinRange(int value, int min, int max) => value >= min && value <= max;
}