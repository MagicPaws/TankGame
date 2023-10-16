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
        // 私有构造函数用于实现单例模式。
    }

    /// <summary>
    /// 保存数据到PlayerPrefs。
    /// 注意：要保存的对象的类必须包含一个无参数构造函数，如果类中包含自定义类成员对象，这些自定义类也需要具有无参数构造函数，以此类推。
    /// 因为在加载该对象时需要使用无参数构造函数。
    /// </summary>
    /// <param name="keyName">数据的关键字</param>
    /// <param name="obj">要存储的对象</param>
    public void SaveData(string keyName, object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fieldInfos = type.GetFields();
        string key;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            // 生成用于存储字段值的唯一键。
            key = keyName + "_" + type.Name + "_" + fieldInfos[i].FieldType.Name + "_" + fieldInfos[i].Name;
            SaveDataValue(key, fieldInfos[i].GetValue(obj));
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 根据数据类型存储数据。
    /// 支持的数据类型包括：int、float、string、bool、List、Dictionary、自定义类。
    /// </summary>
    /// <param name="key">数据的键</param>
    /// <param name="value">要存储的数据</param>
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
            // 如果是True则使用int类型1存储，如果是False则使用int类型0存储。
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
            // 处理自定义类的递归存储。
            SaveData(key + "|", value);
        }
    }

    /// <summary>
    /// 从PlayerPrefs加载数据。
    /// 注意：加载的自定义类时，类中必须包含一个公开的无参数构造函数。
    /// 若包含成员变量类，
    /// </summary>
    /// <param name="keyName">数据的关键字</param>
    /// <param name="type">要加载的类类型</param>
    public object LoadData(string keyName, Type type)
    {
        object obj = Activator.CreateInstance(type);
        FieldInfo[] fieldInfos = type.GetFields();
        string key;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            // 生成用于存储字段值的唯一键。
            key = keyName + "_" + type.Name + "_" + fieldInfos[i].FieldType.Name + "_" + fieldInfos[i].Name;
            // 使用反射设置字段的值。
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
            // 处理自定义类的递归加载。
            return LoadData(key + "|", type);
        }
    }

    /// <summary>
    /// 用于对数据键进行加密和解密的方法。
    /// </summary>
    /// <param name="data">需要加密的数据</param>
    /// <returns>传入密钥返回原始值，传入原始值返回密钥</returns>
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
