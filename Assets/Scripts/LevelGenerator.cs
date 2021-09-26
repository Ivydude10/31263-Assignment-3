using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject[] levelSegments;
    public GameObject grid;
    public GameObject manualGrid;
    GameObject levelPiece;

    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    // Start is called before the first frame update
    void Start()
    {
        Destroy(manualGrid);
        CreateMap(0);
        CreateMap(1);
        CreateMap(2);
        CreateMap(3);
        //grid.transform.position = new Vector2(-(levelMap.GetLength(0)-2), levelMap.GetLength(1));
        //Instantiate(grid, new Vector2(0, 0), Quaternion.Euler(0, 180, 0)); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMap(int dir)
    {
        Vector2 startPos;
        
        for(int i=0; i<levelMap.GetLength(0); i++)
        {
            for(int j=0; j<levelMap.GetLength(1); j++)
            {
                switch (dir)
                {
                    case 0: startPos = new Vector2(j-(levelMap.GetLength(1)-1), -i+(levelMap.GetLength(0)-1));
                            break; 
                    case 1: startPos = new Vector2(-j+(levelMap.GetLength(1)), -i+(levelMap.GetLength(0)-1));
                            break;
                    case 2: startPos = new Vector2(-j+(levelMap.GetLength(1)), i-(levelMap.GetLength(0)-1));
                            break; 
                    case 3: startPos = new Vector2(j-(levelMap.GetLength(1)-1), i-(levelMap.GetLength(0)-1));
                            break;  
                    default: startPos = new Vector2(j-(levelMap.GetLength(1)-1), -i+(levelMap.GetLength(0)-1));
                            break;
                }
                int currSegment = levelMap[i, j];
                if(currSegment!=0)
                {
                    levelPiece = Instantiate(levelSegments[currSegment], startPos, Quaternion.identity, grid.transform);
                    levelPiece.transform.rotation = detRotation(i, j, currSegment, dir);
                }
            }
        }
    }

    Quaternion detRotation(int i, int j, int currSegment, int dir)
    {
        switch (currSegment)
        {
            case 1:  return cornerRotation(i, j, dir);
            case 2:  return wallRotation(i, j);
            case 3:  return cornerRotation(i, j, dir);
            case 4:  return wallRotation(i, j);
            case 7:  return juncRotation(i, j, dir); 
            default: return Quaternion.identity;
        }
    }

    Quaternion juncRotation(int i, int j, int dir)
    {
        if(i==0)
            switch (dir)
            {  
                case 0: return Quaternion.Euler(0, 0, 270); break;
                case 1: return Quaternion.Euler(0, 180, 270); break;
                case 2: return Quaternion.Euler(0, 0, 90); break;
                case 3: return Quaternion.Euler(0, 180, 90); break;
                default: return Quaternion.identity; break;
            }
            // if(dir%2!=0)
            //     return Quaternion.Euler(0, 180, -90);
            // else
            //     return Quaternion.Euler(0, 0, -90);
        else
            return Quaternion.identity;
    }

    Quaternion wallRotation(int i, int j)
    {
        if(i==0)
        {
            if(levelMap[i, j+1] == 1 || levelMap[i, j+1] == 2 || levelMap[i, j+1] == 7)
                return Quaternion.Euler(0, 0, -90);
            else
                return Quaternion.identity;    
        }
        if(j==0)
        {
            if(levelMap[i, j+1] == 1 ||levelMap[i, j+1] == 2 || levelMap[i, j+1] == 7)
                return Quaternion.Euler(0, 0, -90);
            else
                return Quaternion.identity;
        }
        if(levelMap[i, j] == 2)
        {
            if(levelMap[i, j-1] == 1 ||levelMap[i, j-1] == 2 || levelMap[i, j-1] == 7)
                return Quaternion.Euler(0, 0, -90);
            else
                return Quaternion.identity;
        }
        if(levelMap[i, j] == 4)
        {
            if(levelMap[i, j-1] != 4 && levelMap[i, j-1] != 3)
                return Quaternion.identity;
            else
                return Quaternion.Euler(0, 0, 90);
        }
        else
                return Quaternion.identity;
    }

    Quaternion cornerRotation(int i, int j, int dir)
    {
        float offset = dir*90;
        if(i==0 && j==0)
            return Quaternion.Euler(0, 0, -90-offset);
        if(i==0 || j==0)
            return Quaternion.Euler(0, 0, 0+offset);
        else if(levelMap[i, j] == 1)
        {
            if(levelMap[i, j-1] == 1 ||levelMap[i, j-1] == 2 || levelMap[i, j-1] == 7)
            {
                if(levelMap[i-1, j] == 1 ||levelMap[i-1, j] == 2 || levelMap[i-1, j] == 7)
                    return Quaternion.Euler(0, 0, 90-offset);
                else
                    return Quaternion.Euler(0, 0, 180+offset);
            }
            else
                    return Quaternion.identity;
        }
        else if(levelMap[i, j] == 3)
        {
            if(levelMap[i-1, j] != 4 && levelMap[i-1, j] != 3)
            {
                if(levelMap[i, j-1] != 4 && levelMap[i, j-1] != 3)
                    return Quaternion.Euler(0, 0, 270-offset);
                else
                    return Quaternion.Euler(0, 0, 180+offset);
            }
            else if(levelMap[i, j-1] != 4 && levelMap[i, j-1] != 3)
            {
                return Quaternion.Euler(0, 0, 0+offset);
            }
            else
                return Quaternion.Euler(0, 0, 90-offset);
        }
        else
            return Quaternion.identity;
    }
}
