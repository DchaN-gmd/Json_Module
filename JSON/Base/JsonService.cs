using System;
using System.IO;
using UnityEngine;

namespace JSON
{
    public abstract class JsonService<T> : MonoBehaviour, IClearable
    {
        [SerializeField] protected JsonTypePath JsonTypePath;
        [SerializeField] protected string FileName;
        [SerializeField] protected string Path;

        public void Save(T data)
        {
            File.WriteAllText(GetPath(), JsonUtility.ToJson(data));
        }

        public T GetData()
        {
            var data = JsonUtility.FromJson<T>(File.ReadAllText(GetPath()));

            return data;
        }

        protected virtual string GetPath()
        {
            string path;

            switch (JsonTypePath)
            {
                case JsonTypePath.StreamingPath: path = Application.streamingAssetsPath + FileName; break;
                case JsonTypePath.StreamingAndCustomPath: path = Application.streamingAssetsPath + Path + FileName; break;
                case JsonTypePath.CustomPath: path = Path + FileName; break;
                default: throw new ArgumentOutOfRangeException();
            }

            return path;
        }

        public void Clear()
        {
            File.WriteAllText(GetPath(), null);
        }
    }
}
