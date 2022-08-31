using System.IO;
using UnityEngine;

namespace Chocolate4.SaveLoad
{
    public class GameDataReader 
    {
    	private BinaryReader reader;
		public readonly int SaveVersion;
    
    	public GameDataReader(BinaryReader reader, int saveVersion) 
        {
    		this.reader = reader;
			SaveVersion = saveVersion;
    	}
        public int ReadInt()
        {
    		return reader.ReadInt32();
    	}
        public float ReadFloat()
        {
    		return reader.ReadSingle();
    	}
        public Vector3 ReadVector3() 
        {
    		Vector3 value;
    		value.x = reader.ReadSingle();
    		value.y = reader.ReadSingle();
    		value.z = reader.ReadSingle();
    		return value;
    	}
        public Quaternion ReadQuaternion() 
		{
			Quaternion value;
			value.x = reader.ReadSingle();
			value.y = reader.ReadSingle();
			value.z = reader.ReadSingle();
			value.w = reader.ReadSingle();
			return value;
		}
        public Vector2 ReadVector2() 
        {
    		Vector2 value;
    		value.x = reader.ReadSingle();
    		value.y = reader.ReadSingle();
    		return value;
    	}

        public string ReadString()
        {
			return reader.ReadString();
        }
    }
}