using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace SeguridadFinalJM.Utilerias
{
    public class Utils
    {
        public string MuestraSha(string dato)
        {
            if (dato != null)
            {
               
                using SHA256 sha256 = SHA256.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream;
                StringBuilder sb = new();
                stream = sha256.ComputeHash(encoding.GetBytes(dato));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
       

        private static byte[] MyKey()
        {
             byte[] key = new byte[16] { 92,81,141,65,99,100,78,65,23,88,97,32,10,53,98,45, };
            return key;
        }
        private static byte[] MyIV()
        {
            byte[] iv = new byte[16] { 87,96,25,38,41,69,82,54,36,55,87,64,12,49,34,10 };
            return iv;
        }
        public byte[] Encrypt(string dato)
        {
            byte[] result;
            using Aes aes = Aes.Create();
            // si se guarda la informacion en la base de datos necesito mi key y mi iv fijos para garantizar el resultado
            ICryptoTransform encryptor = aes.CreateEncryptor(MyKey(), MyIV());
            using MemoryStream stream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
            {
                streamWriter.Write(dato);
            }
                
            result = stream.ToArray();
            //envio mi resultado
            return result;
        }
        public string Decrypt(byte[] data)
        {
            string result = string.Empty;
            using Aes aes = Aes.Create();
            // si se guarda la informacion en la base de datos necesito mi key y mi iv fijos para garantizar el resultado
            ICryptoTransform decryptor = aes.CreateDecryptor(MyKey(), MyIV());
            using MemoryStream stream = new MemoryStream(data);
            using CryptoStream cryptoStream =new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);
            result = streamReader.ReadToEnd();
            return result;
        }

        public string MaskTarjetaCRedito(string plastico)
        {
            if(plastico.Length < 16)
            {
                return "0";
            }
            string ultimosCuatroDigitos = plastico.Substring (12);
            string maskNumber = new string('*', plastico.Length - 4) + ultimosCuatroDigitos;
            return maskNumber;

        }
    }
}
