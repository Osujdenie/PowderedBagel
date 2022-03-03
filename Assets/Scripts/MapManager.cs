using System.Collections;
using System; // так скрипт сможет использовать сериализацию
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public static Tile[,] map; // двумерный массив с информацией дл€ всех €чеек
}

[Serializable] // делает класс сериализованным, так что его можно сохранить в файл
public class Tile // содержит информацию по каждой €чейке на карте
{
    public int xPosition; // позици€ на оси ’
    public int yPosition; // позици€ на оси Y
    [NonSerialized]
    public GameObject baseObject; // игровой объект на карте, прив€занный к этой позиции, пол, стена и т.д.
    public string type; // тип €чейки, если это стена, пол и т.д.
}

[Serializable]
public class Position // класс, который сохран€ет позицию любой €чейки
{
    public int x;
    public int y;
}

[Serializable]
public class Wall // класс, который сохран€ет информацию о стене, дл€ алгоритма генерации подземель€
{
    public List<Position> positions;
    public string direction; // направление, в котором стена смотрит относительно центра комнаты
    public int length;
    public bool hasFeature = false;
}

[Serializable]
public class Feature // класс, который сохран€ет информацию об объекте (корридор или комната), дл€ алгоритма генерации подземель€
{
    public List<Position> positions;
    public Wall[] walls;
    public string type;
    public int width;
    public int heigth;
}
