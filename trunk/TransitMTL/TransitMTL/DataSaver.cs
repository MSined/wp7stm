using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TransitMTL
{
    public class DataSaver<MyDataType>
    {
        public string TargetFolderName { get; set; }

        private readonly DataContractSerializer _mySerializer;
        private IsolatedStorageFile _isoFile;

        IsolatedStorageFile IsoFile
        {
            get
            {
                return _isoFile ?? (_isoFile = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication());
            }
        }

        public DataSaver()
        {
            TargetFolderName = "ScoreData";
            _mySerializer = new DataContractSerializer(typeof(MyDataType));
        }

        public void SaveMyData(MyDataType sourceData, String targetFileName)
        {
            string TargetFileName;
            TargetFileName = String.Format("{0}/{1}.dat", TargetFolderName, targetFileName);
            if (!IsoFile.DirectoryExists(TargetFolderName))
                IsoFile.CreateDirectory(TargetFolderName);
            try
            {
                using (var targetFile = IsoFile.CreateFile(TargetFileName))
                {
                    _mySerializer.WriteObject(targetFile, sourceData);
                }
            }
            catch (Exception e)
            {
                IsoFile.DeleteFile(TargetFileName);
            }
        }




        public MyDataType LoadMyData(string sourceName)
        {
            var retVal = default(MyDataType);
            var targetFileName = String.Format("{0}/{1}.dat", TargetFolderName, sourceName);
            if (IsoFile.FileExists(targetFileName))
                using (var sourceStream = IsoFile.OpenFile(targetFileName, FileMode.Open))
                {
                    retVal = (MyDataType)_mySerializer.ReadObject(sourceStream);
                }
            return retVal;
        }
    }
}
