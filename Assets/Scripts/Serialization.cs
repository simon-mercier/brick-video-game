using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Serilization
{
    static readonly string directory = $"{Application.dataPath}/Levels/";

    static Serilization()
    {
         SerializeLevels();
    }

    public static void SerializeLevels()
    {
        var mapsSprites = Resources.LoadAll<Sprite>("Sprites/Levels");
        for (int i = 1; i <= mapsSprites.Length; i++)
        {
            var myTexture = mapsSprites[i - 1].texture;
            var data = new char[(int)mapsSprites[i - 1].rect.width, (int)mapsSprites[i - 1].rect.height];

            var trueMapWidth = int.MinValue;
            var trueMapHeight = int.MinValue;
            for (int row = 0; row < mapsSprites[i - 1].rect.width; row++)
            {
                for (int col = 0; col < mapsSprites[i - 1].rect.height; col++)
                {

                    var thisPixel = myTexture.GetPixel(row, col);

                    if (thisPixel.a != 0 && row + 1 > trueMapWidth)
                        trueMapWidth = row + 1;

                    if (thisPixel.a != 0 && col + 1 > trueMapHeight)
                        trueMapHeight = col + 1;

                    //tiles
                    if (MatchColor(thisPixel, 0, 1, 0, 1))
                        data[row, col] = 't';
                    //EndTile
                    else if (MatchColor(thisPixel, 0, 0, 1, .2f))
                        data[row, col] = 'e';
                    //StartTile
                    else if (MatchColor(thisPixel, 1, 0, 0, .2f))
                        data[row, col] = 's';
                    //hiddenTileActivators
                    else if (MatchColor(thisPixel, 1, 1, 0, .2f))
                        data[row, col] = 'a';
                    //hiddenTiles
                    else if (MatchColor(thisPixel, 0, 1, 1, .2f))
                        data[row, col] = 'h';
                    //hiddenTempTile
                    else if (MatchColor(thisPixel, 1, 1, 1, .2f))
                        data[row, col] = 'p';
                    //hiddenTempTileActivators
                    else if (MatchColor(thisPixel, 1, 0, 1, .2f))
                        data[row, col] = 'c';
                    //qsTilesReference
                    else if (MatchColor(thisPixel, 0, 0, 0, .2f))
                        data[row, col] = 'q';
                    //flashTiles
                    else if (MatchColor(thisPixel, 1, 0, 0.5f, .2f, 0.1f))
                        data[row, col] = 'f';
                    //Transparent
                    else if (TouchesAnotherTile(row, col, myTexture))
                        data[row, col] = '0';
                }
            }
            SerializeLevel(i, new LevelData(trueMapWidth, trueMapHeight, data));
        }
    }

    private static void SerializeLevel(int level, LevelData data)
    {
        var destination = $"{directory}Level{level}.dat";
        Debug.Log(destination);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

        var bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public static LevelData DeserializeLevel(int level)
    {
        var destination = $"{directory}Level{level}.dat";
        FileStream file;

        if (File.Exists(destination)) 
            file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            return null;
        }

        var bf = new BinaryFormatter();
        var data = bf.Deserialize(file) as LevelData;
        file.Close();
        return data;
    }

    private static bool TouchesAnotherTile(int x, int y, Texture2D myTexture)
    {
        return (myTexture.GetPixel(x, y + 1).a > 0.1f || myTexture.GetPixel(x, y - 1).a > 0.1f || myTexture.GetPixel(x + 1, y).a > 0.1f || myTexture.GetPixel(x - 1, y).a > 0.1f);
    }

    private static bool MatchColor(Color thisPixel, float r, float g, float b, float a)
    {
        return MatchColor(thisPixel, r, g, b, a, 0);
    }
    private static bool MatchColor(Color thisPixel, float r, float g, float b, float a, float increment)
    {
        return thisPixel.r <= r + increment && thisPixel.r >= r - increment && thisPixel.g <= g + increment && thisPixel.g >= g - increment && thisPixel.b <= b + increment && thisPixel.b >= b - increment && thisPixel.a >= a;
    }
}

[System.Serializable]
public class LevelData
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public char[,] Data { get; private set; }
    public LevelData(int width, int height, char[,] data)
    {
        Width = width;
        Height = height;
        Data = data;
    }
}
