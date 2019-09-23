using UnityEngine;
using System.Collections;

public class Settings
{
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void SetTimer(int value)
    {
        PlayerPrefs.SetInt("time", value);
    }

    public static int GetTimer
    {
        get
        { 
            if (!Settings.HasKey("time"))
            {
                Settings.SetTimer(0);
            }
            return PlayerPrefs.GetInt("time"); 
        }
    }

    //-------------------------------------------

    public static void SetBest(int value)
    {
        PlayerPrefs.SetInt("best", value);
    }

    public static int GetBest
    {
        get
        { 
            if (!Settings.HasKey("best"))
            {
                Settings.SetBest(0);
            }
            return PlayerPrefs.GetInt("best"); 
        }
    }

    //-------------------------------------------
    public static void SetContinue(int value)
    {
        PlayerPrefs.SetInt("continue", value);
    }

    public static int GetContinue
    {
        get
        { 
            if (!Settings.HasKey("continue"))
            {
                Settings.SetContinue(0);
            }
            return PlayerPrefs.GetInt("continue"); 
        }
    }

    //-------------------------------------------

    public static void SetLevel(int value)
    {
        PlayerPrefs.SetInt("level", value);
    }

    public static int GetLevel
    {
        get
        { 
            if (!Settings.HasKey("level"))
            {
                Settings.SetLevel(1);
            }
            return PlayerPrefs.GetInt("level"); 
        }
    }

    //-------------------------------------------

    public static void SetMaxLevel(int value)
    {
        PlayerPrefs.SetInt("maxlevel", value);
    }

    public static int GetMaxLevel
    {
        get
        { 
            if (!Settings.HasKey("maxlevel"))
            {
                Settings.SetMaxLevel(1);
            }
            return PlayerPrefs.GetInt("maxlevel"); 
        }
    }

    //-------------------------------------------

    public static void SetTypeMove(int value)
    {
        PlayerPrefs.SetInt("typemove", value);
    }

    public static int GetTypeMove
    {
        get { return PlayerPrefs.GetInt("typemove"); }
    }


    //-------------------------------------------

    public static void SetSound(int value)
    {
        PlayerPrefs.SetInt("sound", value);
    }

    public static int GetSound
    {
        get
        { 
            if (!Settings.HasKey("sound"))
            {
                Settings.SetSound(1);
            }

            return PlayerPrefs.GetInt("sound");
        }
    }
        

    //-------------------------------------------

    public static void SetStar(int value, int index)
    {
        PlayerPrefs.SetInt("star" + index, value);
    }

    public static int GetStar(int index)
    {
        if (!Settings.HasKey("star" + index))
        {
            Settings.SetStar(1, index);
        }

        return PlayerPrefs.GetInt("star" + index);
    }

}


