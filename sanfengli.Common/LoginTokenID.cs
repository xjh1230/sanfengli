using System;

namespace sanfengli.Common
{
    [Serializable]
    public class LoginTokenID
    {
        static readonly string token_ekey = "I'vek0w\n";
        int _uid;
        string _pwd;
        int _timestamp;
        int _machine;
        short _pid;
        int _increment;
        UniqueObjectID _uniqueObjectID;

        public UniqueObjectID UniqueObjectid
        {
            get { return _uniqueObjectID; }
        }
        // public properties
        /// <summary>Gets the timestamp. 
        /// </summary>
        public int Timestamp
        {
            get { return _timestamp; }
        }
        /// <summary>Gets the machine.
        /// </summary>
        public int Machine
        {
            get { return _machine; }
        }
        /// <summary>Gets the PID.
        /// </summary>
        public short Pid
        {
            get { return _pid; }
        }
        /// <summary>Gets the increment.
        /// </summary>
        public int Increment
        {
            get { return _increment; }
        }
        /// <summary>Gets the creation time (derived from the timestamp).
        /// </summary>
        public DateTime CreationTime
        {
            get { return _uniqueObjectID.CreationTime; }
        }
        /// <summary>Gets the visit time (derived from the timestamp).
        /// </summary>
        public DateTime LastVisitTime
        {
            get { return BsonConstants.UnixEpoch.AddSeconds(_timestamp); }
        }
        /// <summary>用户Id
        /// </summary>
        public int Uid { get { return _uid; } }
        /// <summary>密码（密文）
        /// </summary>
        public string Pwd { get { return _pwd; } }






        public LoginTokenID(string value)
        {
            //int test = UniqueObjectID.GetTimestampFromDateTime(DateTime.UtcNow);
            //DateTime saddas = BsonConstants.UnixEpoch.AddSeconds(test);
            if (value == null)
            {
                return;
            }

            try
            {
                value = EncryptionFunc.Decrypt(value, token_ekey);
            }
            catch (Exception)
            {
                return;
            }
            string[] _stemp = value.Split(new string[] { "\f" }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (_stemp.Length == 4)
                {
                    _uid = int.Parse(_stemp[0]);
                    _pwd = _stemp[1];
                    _uniqueObjectID = new UniqueObjectID(_stemp[2]);
                    _timestamp = Convert.ToInt32(_stemp[3]);
                }
                else if (_stemp.Length == 3)
                {
                    _uid = int.Parse(_stemp[0]);
                    _pwd = _stemp[1];
                    _uniqueObjectID = new UniqueObjectID(_stemp[2]);
                    _timestamp = UniqueObjectID.GetTimestampFromDateTime(DateTime.UtcNow);
                }

            }
            catch (Exception)
            {
                return;
            }
            if (_uniqueObjectID == null)
            {
                return;
            }
            _machine = _uniqueObjectID.Machine;
            _pid = _uniqueObjectID.Pid;
            _increment = _uniqueObjectID.Increment;


        }
        public LoginTokenID(int uid, string pwd="niciaiwomima")
        {
            _uniqueObjectID = UniqueObjectID.GenerateNewId();
            _timestamp = UniqueObjectID.GetTimestampFromDateTime(DateTime.UtcNow);
            _machine = _uniqueObjectID.Machine;
            _pid = _uniqueObjectID.Pid;
            _increment = _uniqueObjectID.Increment;
            _uid = uid;
            _pwd = pwd;
        }
        public LoginTokenID(int uid, string pwd, UniqueObjectID oldUniqueObjectID)
        {
            _uniqueObjectID = oldUniqueObjectID;
            _timestamp = UniqueObjectID.GetTimestampFromDateTime(DateTime.UtcNow);
            _machine = _uniqueObjectID.Machine;
            _pid = _uniqueObjectID.Pid;
            _increment = _uniqueObjectID.Increment;
            _uid = uid;
            _pwd = pwd;
        }



        public override string ToString()
        {

            return EncryptionFunc.Encrypt(Uid + "\f" + Pwd + "\f" + _uniqueObjectID.ToString() + "\f" + _timestamp, token_ekey);
        }
    }
}
