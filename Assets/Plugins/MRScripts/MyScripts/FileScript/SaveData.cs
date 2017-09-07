namespace MasujimaRyohei
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using UnityEngine;

    //The class which I can save in Json form.

    /// 最初に値を設定、取得するタイミングでファイル読み出す。
    public class SaveData
    {
        private static SaveDataBase _savedatabase = null;

        private static SaveDataBase savedatabase
        {
            get
            {
                if (_savedatabase == null)
                {
                    string path = Application.persistentDataPath + "/";
                    string fileName = Application.companyName + "." + Application.productName + ".savedata.json";
                    _savedatabase = new SaveDataBase(path, fileName);
                }
                return _savedatabase;
            }
        }

        private SaveData(){}

        #region [ PUBLIC_STATIC_METHOD ]

        /// 指定したキーとT型のクラスコレクションをセーブデータに追加。
        /// Adds the specified key, and some kind of a class collection to the save data.
        /// 
        public static void SetList<T>(string key, List<T> list) where T : class, new()
        {
            savedatabase.SetList<T>(key, list);
        }

        ///  指定したキーとT型のクラスコレクションをセーブデータから取得。
        ///  Gets the specified key, and some kind of a class collection to the save data.
        public static List<T> GetList<T>(string key, List<T> _default) where T : class, new()
        {
            return savedatabase.GetList<T>(key, _default);
        }

        ///  指定したキーとT型のクラスをセーブデータに追加します。
        ///  Adds the specified key, and some kind of a class to the save data.
        public static T GetClass<T>(string key, T _default) where T : class, new()
        {
            return savedatabase.GetClass(key, _default);

        }

        ///  指定したキーとT型のクラスコレクションをセーブデータから取得します。
        ///  Gets the specified key, and some kind of a class to the save data.
        public static void SetClass<T>(string key, T obj) where T : class, new()
        {
            savedatabase.SetClass<T>(key, obj);
        }

        /// 指定されたキーに関連付けられている値を取得します。
        public static void SetString(string key, string value)
        {
            savedatabase.SetString(key, value);
        }

        /// 指定されたキーに関連付けられているString型の値を取得します。
        /// 値がない場合、_defaultの値を返します。省略した場合、空の文字列を返します。
        public static string GetString(string key, string _default = "")
        {
            return savedatabase.GetString(key, _default);
        }

        /// 指定されたキーに関連付けられているInt型の値を取得します。
        public static void SetInt(string key, int value)
        {
            savedatabase.SetInt(key, value);
        }

        /// 指定されたキーに関連付けられているInt型の値を取得します。
        /// 値がない場合、_defaultの値を返します。省略した場合、0を返します。
        public static int GetInt(string key, int _default = 0)
        {
            return savedatabase.GetInt(key, _default);
        }

        /// 指定されたキーに関連付けられているfloat型の値を取得します。
        public static void SetFloat(string key, float value)
        {
            savedatabase.SetFloat(key, value);
        }

        /// 指定されたキーに関連付けられているfloat型の値を取得します。
        /// 値がない場合、_defaultの値を返します。省略した場合、0.0fを返します。
        public static float GetFloat(string key, float _default = 0.0f)
        {
            return savedatabase.GetFloat(key, _default);
        }

        /// セーブデータからすべてのキーと値を削除します。
        /// Delites all key and all value from save data.
        public static void Clear()
        {
            savedatabase.Clear();
        }

        /// 指定したキーを持つ値を セーブデータから削除します。
        public static void Remove(string key)
        {
            savedatabase.Remove(key);
        }

        /// セーブデータ内にキーが存在するかを取得します。
        public static bool ContainsKey(string _key)
        {
            return savedatabase.ContainsKey(_key);
        }

        /// セーブデータに格納されたキーの一覧を取得します。
        public static List<string> Keys()
        {
            return savedatabase.Keys();
        }

        /// 明示的にファイルに書き込みます。
        public static void Save()
        {
            savedatabase.Save();
        }

        #endregion

        #region [ SAVE_DATA_BASE_CLASS ]

        [Serializable]
        private class SaveDataBase
        {
            #region [ FIELDS ]

            private string _path;
            //Save location.
            public string path
            {
                get { return _path; }
                set { _path = value; }
            }

            private string _fileName;

            public string fileName
            {
                get { return _fileName; }
                set { _fileName = value; }
            }

            // set key and strings of json
            private Dictionary<string, string> _saveDictionary;

            #endregion

            #region [ CONSTRUCTOR_AND_DESTRUCTOR ]

            public SaveDataBase(string path, string fileName)
            {
                _path = path;
                _fileName = fileName;
                _saveDictionary = new Dictionary<string, string>();
                Load();

            }

            /// クラスが破棄される時点でファイルに書き込みます。
            ~SaveDataBase()
            {
                Save();
            }

            #endregion

            #region [ PUBLIC_METHODS ]

            public void SetList<T>(string key, List<T> list) where T : class, new()
            {
                KeyCheck(key);
                var serializableList = new Serialization<T>(list);
                string json = JsonUtility.ToJson(serializableList);
                _saveDictionary[key] = json;
            }

            public List<T> GetList<T>(string key, List<T> _default) where T : class, new()
            {
                KeyCheck(key);
                if (!_saveDictionary.ContainsKey(key))
                {
                    return _default;
                }
                string json = _saveDictionary[key];
                Serialization<T> deserializeList = JsonUtility.FromJson<Serialization<T>>(json);

                return deserializeList.ToList();
            }

            public T GetClass<T>(string key, T _default) where T : class, new()
            {
                KeyCheck(key);
                if (!_saveDictionary.ContainsKey(key))
                    return _default;

                string json = _saveDictionary[key];
                T obj = JsonUtility.FromJson<T>(json);
                return obj;

            }

            public void SetClass<T>(string key, T obj) where T : class, new()
            {
                KeyCheck(key);
                string json = JsonUtility.ToJson(obj);
                _saveDictionary[key] = json;
            }

            public void SetString(string key, string value)
            {
                KeyCheck(key);
                _saveDictionary[key] = value;
            }

            public string GetString(string key, string _default)
            {
                KeyCheck(key);

                if (!_saveDictionary.ContainsKey(key))
                    return _default;
                return _saveDictionary[key];
            }

            public void SetInt(string key, int value)
            {
                KeyCheck(key);
                _saveDictionary[key] = value.ToString();
            }

            public int GetInt(string key, int _default)
            {
                KeyCheck(key);
                if (!_saveDictionary.ContainsKey(key))
                    return _default;
                int ret;
                if (!int.TryParse(_saveDictionary[key], out ret))
                {
                    ret = 0;
                }
                return ret;
            }

            public void SetFloat(string key, float value)
            {
                KeyCheck(key);
                _saveDictionary[key] = value.ToString();
            }

            public float GetFloat(string key, float _default)
            {
                float ret;
                KeyCheck(key);
                if (!_saveDictionary.ContainsKey(key))
                    ret = _default;

                if (!float.TryParse(_saveDictionary[key], out ret))
                {
                    ret = 0.0f;
                }
                return ret;
            }

            public void Clear()
            {
                _saveDictionary.Clear();

            }

            public void Remove(string key)
            {
                KeyCheck(key);
                if (_saveDictionary.ContainsKey(key))
                {
                    _saveDictionary.Remove(key);
                }

            }

            public bool ContainsKey(string _key)
            {

                return _saveDictionary.ContainsKey(_key);
            }

            public List<string> Keys()
            {
                return _saveDictionary.Keys.ToList<string>();
            }

            public void Save()
            {
                using (StreamWriter writer = new StreamWriter(_path + _fileName, false, Encoding.GetEncoding("utf-8")))
                {
                    var serialDict = new Serialization<string, string>(_saveDictionary);
                    serialDict.OnBeforeSerialize();
                    string dictJsonString = JsonUtility.ToJson(serialDict);
                    writer.WriteLine(dictJsonString);
                }
            }

            public void Load()
            {
                if (File.Exists(_path + _fileName))
                {
                    using (StreamReader sr = new StreamReader(_path + _fileName, Encoding.GetEncoding("utf-8")))
                    {
                        if (_saveDictionary != null)
                        {
                            var sDict = JsonUtility.FromJson<Serialization<string, string>>(sr.ReadToEnd());
                            sDict.OnAfterDeserialize();
                            _saveDictionary = sDict.ToDictionary();
                        }
                    }
                }
                else { _saveDictionary = new Dictionary<string, string>(); }
            }

            public string GetJsonString(string key)
            {
                KeyCheck(key);
                if (_saveDictionary.ContainsKey(key))
                {
                    return _saveDictionary[key];
                }
                else
                {
                    return null;
                }
            }

            #endregion

            #region [ PRIVATE_METHODS ]

            /// キーに不正がないかチェックします。
            private void KeyCheck(string _key)
            {
                if (string.IsNullOrEmpty(_key))
                {
                    throw new ArgumentException("invalid key!!");
                }
            }

            #endregion
        }

        #endregion

        #region [ SERIALIZATION_CLASS ]

        // List<T>
        [Serializable]
        private class Serialization<T>
        {
            public List<T> target;

            public List<T> ToList()
            {
                return target;
            }

            public Serialization()
            {
            }

            public Serialization(List<T> target)
            {
                this.target = target;
            }
        }

        // Dictionary<TKey, TValue>
        [Serializable]
        private class Serialization<TKey, TValue>
        {
            public List<TKey> keys;
            public List<TValue> values;
            private Dictionary<TKey, TValue> _target;

            public Dictionary<TKey, TValue> ToDictionary()
            {
                return _target;
            }

            public Serialization(){}

            public Serialization(Dictionary<TKey, TValue> target)
            {
                this._target = target;
            }

            public void OnBeforeSerialize()
            {
                keys = new List<TKey>(_target.Keys);
                values = new List<TValue>(_target.Values);
            }

            public void OnAfterDeserialize()
            {
                int count = System.Math.Min(keys.Count, values.Count);
                _target = new Dictionary<TKey, TValue>(count);
                Enumerable.Range(0, count).ToList().ForEach(i => _target.Add(keys[i], values[i]));
            }
        }

        #endregion
    }
}