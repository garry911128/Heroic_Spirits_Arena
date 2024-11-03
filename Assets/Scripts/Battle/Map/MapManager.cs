using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager
{
    private List<Sprite> mapSprites = new List<Sprite>();
    private Sprite lastMap;

    public void LoadMapSprites()
    {
        Sprite[] loadedBattleMaps = Resources.LoadAll<Sprite>("Scene/BattleScene/Maps");
        mapSprites.AddRange(loadedBattleMaps);
    }

    public Sprite SelectRandomMap()
    {
        if (mapSprites.Count > 1)
        {
            Sprite newMap;
            do
            {
                newMap = mapSprites[Random.Range(0, mapSprites.Count)];
            } while (newMap == lastMap);

            lastMap = newMap;
            return newMap;
        }
        else if (mapSprites.Count == 1)
        {
            return mapSprites[0];
        }
        return null;
    }
}
