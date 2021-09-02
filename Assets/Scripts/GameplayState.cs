using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    private static Creature _selectedCreature;

    public static Creature SelectedCreature => _selectedCreature;

    private static bool _selectedCreatureIsHero;

    public static void SetSelectedCreature(Creature creature)
    {

        if (_selectedCreature?.GetDataType() == typeof(HeroData) && creature?.GetDataType() == typeof(HeroData))
        {
            print("swappder");
            ChangePlaces(creature);
        }
        else
        {
            _selectedCreature = creature;
        }
    }

    public static void ChangePlaces(Creature creature)
    {
        var initialCell = _selectedCreature.CurrentCell;
        _selectedCreature.SetCell(creature.CurrentCell);
        creature.SetCell(initialCell);
        _selectedCreature = null;
    }
}
