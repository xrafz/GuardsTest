using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureShop : MonoBehaviour
{
    [SerializeField]
    private Shop _shop;
    public List<HeroData> Heroes { get; private set; } = new List<HeroData>();
    public List<HeroData> AvailableHeroes { get; private set; } = new List<HeroData>();
    private int _maxHeroes = 4;
    private int _currentQuantity = 0;
    public static CreatureShop Instance;

    private void Awake()
    {
        //загрузить maxheroes с уровня
        //загрузить доступных героев с сейва
        Instance = this;
    }

    private void OnEnable()
    {
        Heroes.Clear();
    }

    public bool TryAdding(HeroData creature)
    {
        if (_currentQuantity < _maxHeroes)
        {
            Heroes.Add(creature);
            _currentQuantity++;
            return true;
        }
        return false;
    }

    public void Remove(HeroData creature)
    {
        Heroes.Remove(creature);
        _currentQuantity--;
    }

    public void LoadShop()
    {
        if (_currentQuantity == _maxHeroes)
        {
            for (int i = 0; i < Heroes.Count; i++)
            {
                Heroes[i] = Instantiate(Heroes[i]);
            }
            GameSession.SetHeroes(Heroes);
            gameObject.SetActive(false);
            _shop.Init();
        }
    }
}
