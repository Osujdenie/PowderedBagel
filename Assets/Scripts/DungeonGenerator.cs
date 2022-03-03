using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public int mapWidth; // ширина
    public int mapHeigth; // высота

    public int widthMinRoom;
    public int widthMaxRoom;
    public int hegthMinRoom;
    public int heigthMaxRoom;

    public int maxCorridorLength;
    public int maxFeatures;

    public bool isASCII;

    public void InitializeDungeon() // инициализация переменных, которые будут заполняться генерацией
    {
        MapManager.map = new Tile[mapWidth, mapHeigth]; // определяется размерность массива (карты) ширина на высоту    
    }

    public void GenerateDungeon() // создание подземелья
    {
        FirstRoom();
        DrawMap(isASCII);
    }

    void FirstRoom() // создание начальной комнаты
    {
        Feature room = new Feature();
        room.positions = new List<Position>();

        // случайный размер комнаты, получаемый между максимальными и минимальными высотой и шириной
        int roomWidth = Random.Range(widthMinRoom, widthMaxRoom);
        int roomHeigth = Random.Range(hegthMinRoom, heigthMaxRoom);

        // начальная точка комнаты 0,0 с небольшим рандомным смещением
        int xStartingPoint = mapWidth / 2;
        int yStartingPoint = mapHeigth / 2;

        xStartingPoint -= Random.Range(0, roomWidth); 
        yStartingPoint -= Random.Range(0, roomHeigth);

        room.walls = new Wall[4]; // 4 стены?

        for (int i = 0; i < room.walls.Length; i++)
        {
            room.walls[i] = new Wall();
            room.walls[i].positions = new List<Position>();
            room.walls[i].length = 0;

            // выбор направления стен в комнате
            switch (i)
            {
                case 0:
                    room.walls[i].direction = "South";
                    break;
                case 1:
                    room.walls[i].direction = "North";
                    break;
                case 2:
                    room.walls[i].direction = "West";
                    break;
                case 3:
                    room.walls[i].direction = "East";
                    break;
            }
        }

        for (int y = 0; y < roomHeigth; y++)
        {
            for (int x = 0; x < roomWidth; x++)
            {
                // нахождение позиции ячейки относительно начальной точки 0,0
                Position position = new Position();
                position.x = xStartingPoint + x;
                position.y = yStartingPoint + y;

                room.positions.Add(position);

                // добавление позиций ячеек на карту
                MapManager.map[position.x, position.y] = new Tile();
                MapManager.map[position.x, position.y].xPosition = position.x;
                MapManager.map[position.x, position.y].yPosition = position.y;

                // создание стен. (ш/в - 1) т.к. эти значения считались начиная с 1
                if (y == 0) // если переменная у равна 0, то тайл принадлежит самой нижней строке в комнате (South)
                {
                    room.walls[0].positions.Add(position);
                    room.walls[0].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (y == roomHeigth - 1) // (North)
                {
                    room.walls[1].positions.Add(position);
                    room.walls[1].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (x == 0) // если переменна х равна 0, то тайл принадлежит самому левому столбцу (West)
                {
                    room.walls[2].positions.Add(position);
                    room.walls[2].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                else if (y == roomWidth - 1) // (East)
                {
                    room.walls[3].positions.Add(position);
                    room.walls[3].length++;
                    MapManager.map[position.x, position.y].type = "Wall";
                }
                // создание пола
                else
                {
                    MapManager.map[position.x, position.y].type = "Floor";
                }
            }
        }

        room.width = roomWidth;
        room.heigth = roomHeigth;
        room.type = "Room";
    }

    void DrawMap(bool isASCII) // создание текстовой карты в зависимости от типа элемента
    {
        if (isASCII)
        {
            Text screen = GameObject.Find("ASCIITest").GetComponent<Text>();

            string asciiMap = "";

            for (int y = mapHeigth - 1; y >= 0; y--) // ось у
            {
                for(int x = 0; x < mapWidth; x++) // ось х
                {
                    if (MapManager.map[x,y] != null)
                    {
                        switch(MapManager.map[x,y].type)
                        {
                            case "Wall":
                                asciiMap += "#"; // стены
                                break;
                            case "Floor":
                                asciiMap += "."; // пол
                                break;
                        }
                    }
                    else
                    {
                        asciiMap += " ";
                    }

                    if (x == mapWidth - 1)
                    {
                        asciiMap += "\n";
                    }
                }
            }

            screen.text = asciiMap;
        }
    }
}
