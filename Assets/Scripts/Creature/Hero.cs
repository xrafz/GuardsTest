using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hero : Creature
{
    private bool _inFirstMode = false;

    protected override void Awake()
    {
        base.Awake();
        _transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public override void Init()
    {
        base.Init();
        AddHeroSpecificAbilities();
        ChangeMode();
    }

    private void AddHeroSpecificAbilities()
    {
        List<Ability> abilities = _abilities.OfType<Ability>().ToList();
        var heroData = (HeroData)_data;
        for (int i = 0; i < heroData.FirstModeAbilities.Length; i++)
        {
            var ability = heroData.FirstModeAbilities[i];
            ability = Instantiate(ability);
            ability.Init(this);
            ability.name = heroData.FirstModeAbilities[i].name;
            abilities.Add(ability);
        }

        for (int i = 0; i < heroData.SecondModeAbilities.Length; i++)
        {
            var ability = heroData.SecondModeAbilities[i];
            ability = Instantiate(ability);
            ability.Init(this);
            ability.name = heroData.SecondModeAbilities[i].name;
            abilities.Add(ability);
        }
        _abilities = abilities.ToArray();
    }

    public void ChangeMode()
    {
        foreach (Ability ability in _abilities)
        {
            print(ability.name);
        }
        _inFirstMode = !_inFirstMode;
        var data = (HeroData)_data;
        var firstMode = data.FirstModeAbilities;
        var secondMode = data.SecondModeAbilities;
        if (_inFirstMode)
        {
            foreach (Ability ability in _abilities)
            {
                foreach (Ability modeAbility in firstMode)
                {
                    if (ability.name == modeAbility.name)
                    {
                        ability.Sub();
                    }
                }
                foreach (Ability modeAbility in secondMode)
                {
                    if (ability.name == modeAbility.name)
                    {
                        ability.Unsub();
                    }
                }
            }
        }
        else
        {
            foreach (Ability ability in _abilities)
            {
                foreach (Ability modeAbility in firstMode)
                {
                    if (ability.name == modeAbility.name)
                    {
                        ability.Unsub();
                    }
                }
                foreach (Ability modeAbility in secondMode)
                {
                    if (ability.name == modeAbility.name)
                    {
                        ability.Sub();
                    }
                }
            }
        }
        Debug.Log("changed mode");
    }

    private void Start()
    {
        Animator.Play("Spawn");
    }
}
