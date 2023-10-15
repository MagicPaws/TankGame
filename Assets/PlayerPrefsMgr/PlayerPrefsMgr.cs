using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerPrefsDataMgr
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();
    public static PlayerPrefsDataMgr Instance => instance;
    private PlayerPrefsDataMgr()
    {

    }

    /// <summary>
    /// 使用PlayerPrefs保存数据
    /// 注意：保存的对象的类必须包含一个无参构造函数，且如果该类中包含自定义类成员对象则自定义类也需要含有无参构造函数以此类推，因为在加载该对象时需要使用
    /// </summary>
    /// <param name="keyName">关键字名</param>
    /// <param name="obj">所需存储的对象</param>
    public void SaveData(string keyName, object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fieldInfos = type.GetFields();
        string key;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            key = keyName + "_" + type.Name + "_" + fieldInfos[i].FieldType.Name + "_" + fieldInfos[i].Name;
            SaveDataValue(key, fieldInfos[i].GetValue(obj));
        }
        PlayerPrefs.Save();
    }
    private void SaveDataValue(string key, object value)
    {
        Type type = value.GetType();
        if (type == typeof(int))
        {
            PlayerPrefs.SetInt(EncryptionAndDecryption(key), (int)value);
        }
        else if (type == typeof(float))
        {
            PlayerPrefs.SetFloat(EncryptionAndDecryption(key), (float)value);
        }
        else if (type == typeof(string))
        {
            PlayerPrefs.SetString(EncryptionAndDecryption(key), value.ToString());
        }
        else if (type == typeof(bool))
        {
            // 如果是True则使用int类型1存储 如果是False则使用int类型0存储
            PlayerPrefs.SetInt(EncryptionAndDecryption(key), (bool)value == true ? 1 : 0);
        }
        else if (typeof(IList).IsAssignableFrom(type))
        {
            IList list = value as IList;
            PlayerPrefs.SetInt(EncryptionAndDecryption(key + "_Count"), list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                SaveDataValue(key + i, list[i]);
            }
        }
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            IDictionary dic = value as IDictionary;
            PlayerPrefs.SetInt(EncryptionAndDecryption(key + "_Count"), dic.Count);
            int index = 0;
            foreach (object dicKey in dic.Keys)
            {
                SaveDataValue(key + "_Key_" + index, dicKey);
                SaveDataValue(key + "_Value_" + index, dic[dicKey]);
                index++;
            }
        }
        else
        {
            SaveData(key + "|", value);
        }
    }
    /// <summary>
    /// 使用PlayerPrefs加载数据 
    /// 注意：加载的对象的类必须包含一个无参构造函数，且如果该类中包含自定义类成员对象则自定义类也需要含有无参构造函数以此类推
    /// </summary>
    /// <param name="keyName">关键字名</param>
    /// <param name="type">所需加载的类名</param>
    public object LoadData(string keyName, Type type)
    {
        // 如果返回对象的话不知道怎么选择构造函数，或者要规定这个类必须包含无参构造函数或者某种构造函数
        // ****直接规定需要在外面初始化后再传入,直接对初始化之后的传入的对象赋值 但是这个对象的类所包含的自定义成员也需要有无参构造函数
        //ConstructorInfo[] constructorInfos = type.GetConstructors();
        //object obj = Activator.CreateInstance(type);

        //if (constructorInfos.Length == 0)
        //{
        //    return null;
        //}

        object obj = Activator.CreateInstance(type);
        FieldInfo[] fieldInfos = type.GetFields();
        string key;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            key = keyName + "_" + type.Name + "_" + fieldInfos[i].FieldType.Name + "_" + fieldInfos[i].Name;
            fieldInfos[i].SetValue(obj, LoadDataValue(key, fieldInfos[i].FieldType));
        }
        return obj;
    }
    private object LoadDataValue(string key, Type type)
    {
        if (type == typeof(int))
        {
            return PlayerPrefs.GetInt(EncryptionAndDecryption(key), -1);
        }
        else if (type == typeof(float))
        {
            return PlayerPrefs.GetFloat(EncryptionAndDecryption(key), -1);
        }
        else if (type == typeof(string))
        {
            return PlayerPrefs.GetString(EncryptionAndDecryption(key), "1");
        }
        else if (type == typeof(bool))
        {
            return PlayerPrefs.GetInt(EncryptionAndDecryption(key)) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(type))
        {
            int count = PlayerPrefs.GetInt(EncryptionAndDecryption(key + "_Count"), 0);
            IList list = Activator.CreateInstance(type) as IList;
            for (int i = 0; i < count; i++)
            {
                list.Add(LoadDataValue((key + i), type.GetGenericArguments()[0]));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(type))
        {
            int count = PlayerPrefs.GetInt(EncryptionAndDecryption(key + "_Count"), 0);
            IDictionary dic = Activator.CreateInstance(type) as IDictionary;
            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadDataValue(key + "_Key_" + i, type.GetGenericArguments()[0]),
                        LoadDataValue(key + "_Value_" + i, type.GetGenericArguments()[1]));
            }
            return dic;
        }
        else
        {
            return LoadData(key + "|", type);
        }
    }
    /// <summary>
    /// Key加密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    //[Obsolete("加密不能正常使用",true)]
    private string EncryptionAndDecryption(string data)
    {
        string EncryptionKey = "A";

        char[] dataChars = data.ToCharArray();
        char[] EncryptionKeyChars = EncryptionKey.ToCharArray();
        for (int i = 0; i < dataChars.Length; i++)
        {
            char dataChar = dataChars[i];
            char keyChar = EncryptionKeyChars[i % EncryptionKeyChars.Length];
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
        }
        return new string(dataChars);
    }
}
