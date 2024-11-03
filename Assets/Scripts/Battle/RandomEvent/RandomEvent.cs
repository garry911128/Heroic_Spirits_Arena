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

    public RandomEvent(string name, BuffType effectType, int value, int duration, float probability)
    {
        this.name = name;
        this.effectType = effectType;
        this.value = value;
        this.duration = duration;
        this.probability = probability; // �]�w���v
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
        newBuff.Apply(character);
        character.buff.Add(newBuff); // �[�J���⪺buff�C��
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
                        continue; // ���L���D��
                    }

                    string[] values = line.Split(',');
                    if (values.Length < 5) // �ˬd�O�_���������C
                    {
                        //Debug.Log("Invalid data format in CSV.");
                        continue;
                    }

                    string name = values[0];
                    //Debug.Log("Loading Event name: " + name);
                    BuffType effectType = (BuffType)System.Enum.Parse(typeof(BuffType), values[1]);
                    int value = int.Parse(values[2]);
                    int duration = int.Parse(values[3]);
                    float probability = float.Parse(values[4]); // Ū�����v

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
