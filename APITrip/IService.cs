using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static APITrip.ServiceFile;
namespace APITrip
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        bool UploadToTempFolder(byte[] pFileBytes, string pFileName, string pathFolder);

        [OperationContract]
        byte[] GetFileFromFolder(string pFileName);

        //[OperationContract]
        //RemoteFileInfo DownloadFile(DownloadRequest request);
        //[OperationContract]
        //void UploadFile(RemoteFileInfo request);

        [OperationContract]
        bool TableauDeByteVersFicher(string CheminRep, string CheminFichier, byte[] TableauDeByte);

        [OperationContract]
        byte[] FichierVersTableauDeByte(string CheminFichier);

        [OperationContract]
        bool fileExistOnFolder(string path, string idDossier);
        // TODO: ajoutez vos opérations de service ici
    }

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";
        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }
        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
