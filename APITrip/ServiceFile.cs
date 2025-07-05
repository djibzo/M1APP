using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace APITrip
{
    public class ServiceFile : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Su_ix";
            }
            return composite;
        }

        public bool UploadToTempFolder(byte[] pFileBytes, string pFileName, string pathFolder)
        {
            bool isSuccess = false;
            try
            {
                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }
                if (!string.IsNullOrEmpty(pathFolder) && !string.IsNullOrEmpty(pFileName))
                {
                    string strFileFullPath = Path.Combine(pathFolder, pFileName);
                    using (FileStream fs = new FileStream(strFileFullPath, FileMode.OpenOrCreate))
                    {
                        fs.Write(pFileBytes, 0, pFileBytes.Length);
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return isSuccess;
        }

        public byte[] FichierVersTableauDeByte(string CheminFichier)
        {
            FileInfo MonFichier = new FileInfo(CheminFichier);
            try
            {
                if (MonFichier.Length > 0)
                {
                    using (FileStream MonFileStream = MonFichier.OpenRead())
                    {
                        byte[] TableauDeBytes = new byte[MonFileStream.Length];
                        MonFileStream.Read(TableauDeBytes, 0, (int)MonFileStream.Length);
                        return TableauDeBytes;
                    }
                }
                return Array.Empty<byte>();
            }
            catch
            {
                return Array.Empty<byte>();
            }
        }

        public byte[] GetFileFromFolder(string filename)
        {
            byte[] filedetails = Array.Empty<byte>();
            // Remplacer la récupération du chemin par une valeur par défaut ou une configuration propre .NET Core
            string strTempFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            string fullPath = Path.Combine(strTempFolderPath, filename);
            if (File.Exists(fullPath))
            {
                return File.ReadAllBytes(fullPath);
            }
            else
            {
                return filedetails;
            }
        }

        public bool fileExistOnFolder(string path, string idDossier)
        {
            bool rep = false;
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\hp\Desktop\MIGL 2024-2025\ASP");
            FileInfo[] Files = d.GetFiles("*.zip");
            foreach (FileInfo file in Files)
            {
                if (file.Name.StartsWith(idDossier.ToString()))
                {
                    rep = true;
                }
            }
            return rep;
        }

        public bool TableauDeByteVersFicher(string CheminRep, string CheminFichier, byte[] TableauDeByte)
        {
            bool resultOK = false;
            try
            {
                if (!Directory.Exists(CheminRep))
                {
                    Directory.CreateDirectory(CheminRep);
                }
                string filePath = Path.Combine(CheminRep, CheminFichier);
                if (File.Exists(filePath))
                {
                    string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                    string extension = Path.GetExtension(filePath);
                    string dateNom = string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year,
                        DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    string newFileName = string.Format("{0}_Old_{1}{2}", fileNameOnly, dateNom, extension);
                    string newFullPath = Path.Combine(CheminRep, newFileName);
                    File.Move(filePath, newFullPath);
                }
                using (FileStream MonFileStream = new FileStream(filePath, FileMode.Create))
                {
                    MonFileStream.Write(TableauDeByte, 0, TableauDeByte.Length);
                    resultOK = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return resultOK;
        }
    }

}
