using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.IO;

public class File_IO : MonoBehaviour
{
    public string path;
    public string[] con;
    public string[] playerA;
    public string[] playerB;
    public List<string[]> Skills = new List<string[]>();

    
    async void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "PlayerData.csv");
        string content = await Readfile(path);
        print(content);
        con = content.Split('\n');
        playerA = con[1].Split(',');
        playerB = con[2].Split(',');

        path = Path.Combine(Application.streamingAssetsPath, "PlayerSkill.csv");
        content = await Readfile(path);
        print(content);
        con = content.Split('\n');
        
        for (int i = 0; i < con.Length - 1; i++)
        {
            Skills.Add( con[i].Split(','));
            print(Skills[i]);
        } 
    }

    public async Task<string> Readfile(String filePath)
    {
        string content = "";

        try
        {
            content = await File.ReadAllTextAsync(filePath);
        }
        catch (Exception ex)
        {
            print(ex);
        }
        return content;
    }

    



}