using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomEvent
{
    public string name;
    public BuffType effectType;
    public int value;
    public int duration;
    public float probability; // 機率屬性

    public RandomEvent(string name, BuffType effectType, int value, int duration, float probability)
    {
        this.name = name;
        this.effectType = effectType;
        this.value = value;
        this.duration = duration;
        this.probability = probability; // 設定機率
    }

    public bool TryApplyEvent(Character character)
    {
        // 生成一個隨機數，範圍從 0 到 1
        float randomValue = Random.Range(0f, 1f);

        // 比較隨機數與事件的機率
        if (randomValue <= probability)
        {
            ApplyEvent(character);
            return true; // 事件成功觸發
        }
        return false; // 事件未觸發
    }

    public void ApplyEvent(Character character)
    {
        Buff newBuff = new Buff(name, duration, effectType, value);
        newBuff.Apply(character);
        character.buff.Add(newBuff); // 加入角色的buff列表
    }

    public static List<RandomEvent> LoadEventsFromCSV(string filePath)
    {
        List<RandomEvent> events = new List<RandomEvent>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    //Debug.Log("Loading Event line: " + line);
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue; // 跳過標題行
                    }

                    string[] values = line.Split(',');
                    if (values.Length < 5) // 檢查是否有足夠的列
                    {
                        //Debug.Log("Invalid data format in CSV.");
                        continue;
                    }

                    string name = values[0];
                    //Debug.Log("Loading Event name: " + name);
                    BuffType effectType = (BuffType)System.Enum.Parse(typeof(BuffType), values[1]);
                    int value = int.Parse(values[2]);
                    int duration = int.Parse(values[3]);
                    float probability = float.Parse(values[4]); // 讀取機率

                    RandomEvent randomEvent = new RandomEvent(name, effectType, value, duration, probability);
                    events.Add(randomEvent);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed to load events from CSV: " + e.Message);
        }

        return events;
    }
}
