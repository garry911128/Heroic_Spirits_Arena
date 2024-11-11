using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomEvent
{
    public string name;
    public BuffType effectType;
    public int value;
    public int duration;
    public float probability; // ���v�ݩ�
    public string description;

    public RandomEvent(string name, BuffType effectType, int value, int duration, float probability, string description)
    {
        this.name = name;
        this.effectType = effectType;
        this.value = value;
        this.duration = duration;
        this.probability = probability; // �]�w���v
        this.description = description;
    }

    public bool TryApplyEvent(Character character)
    {
        // �ͦ��@���H���ơA�d��q 0 �� 1
        float randomValue = Random.Range(0f, 1f);

        // ����H���ƻP�ƥ󪺾��v
        if (randomValue <= probability)
        {
            ApplyEvent(character);
            return true; // �ƥ󦨥\Ĳ�o
        }
        return false; // �ƥ�Ĳ�o
    }

    public void ApplyEvent(Character character)
    {
        Buff newBuff = new Buff(name, duration, effectType, value);
        newBuff.Apply(character, character);
        character.buffs.Add(newBuff); // �[�J���⪺buff�C��
    }

    public static List<RandomEvent> LoadEventsFromCSV(string fileName)
    {
        List<RandomEvent> events = new List<RandomEvent>();

        try
        {
            TextAsset csvFile = Resources.Load<TextAsset>(fileName);
            if (csvFile == null)
            {
                Debug.LogError("CSV file load error, file maybe not in resource folder");
                return events;
            }

            string[] lines = csvFile.text.Split('\n');
            bool isFirstLine = true;

            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] values = line.Split(',');
                if (values.Length < 5)
                {
                    Debug.LogWarning("CSV data format error, this line will be skip" + line);
                    continue;
                }

                string name = values[0];
                BuffType effectType = (BuffType)System.Enum.Parse(typeof(BuffType), values[1]);
                int value = int.Parse(values[2]);
                int duration = int.Parse(values[3]);
                float probability = float.Parse(values[4]);
                string description = values.Length > 5 ? values[5] : "";

                RandomEvent randomEvent = new RandomEvent(name, effectType, value, duration, probability, description);
                events.Add(randomEvent);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("error when load csv file " + e.Message);
        }

        return events;
    }

}
