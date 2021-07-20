using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuMaster.Scopevisio.OpenApi.Test
{
    public class TestConfig : CompuMaster.Scopevisio.OpenApi.Client.Configuration
    {
        public TestConfig() : this(false)
        {
        }

        public TestConfig(bool forbidAuthorization)
        {
            //System.Console.WriteLine("PLEASE NOTE: Following user input will be buffered in directory " + System.IO.Path.GetTempPath());
            this.OrganisationName = null;
            this.Username = InputLine("username");
            this.ClientNumber = InputLine("customer no.");
            this.Password = InputLine("password");
            if (string.IsNullOrWhiteSpace(this.Username))
                NUnit.Framework.Assert.Ignore("Test ignored until buffered user credentials added in directory " + System.IO.Path.GetTempPath());
            if (forbidAuthorization)
                this.AccessToken = null;
            else
            {
                this.AccessToken = InputFromBufferFile("accesstoken");
                if (this.AccessToken == null)
                {
                    TestConfig TokenInfo = new TestConfig(true);
                    (new Api.AuthorizationApi(TokenInfo)).Token(
                            "password",
                            TokenInfo.ClientNumber, 
                            null, 
                            null,
                            TokenInfo.Username, 
                            null,
                            TokenInfo.OrganisationName,
                            TokenInfo.Password, 
                            null, null, null, null);
                }
            }
        }

        public override string AccessToken
        {
            get => base.AccessToken;
            set
            {
                if ((!string.IsNullOrWhiteSpace(value)) && (value != this.AccessToken))
                    InputToBufferFile("accesstoken", value);
                base.AccessToken = value;
            }
        }

        /// <summary>
        /// Ask user for field data and buffer them in local files
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static string InputLine(string fieldName)
        {
            string BufferFile = BufferFilePath(fieldName);
            string EnvVarName = "TEST_" + fieldName.Replace(" ", "").Replace(".", "").ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable(EnvVarName)))
            {
                if (System.Environment.MachineName.StartsWith("WKS"))
                {
                    PersistInputValue(fieldName, System.Environment.GetEnvironmentVariable(EnvVarName));
                    //Console.WriteLine("Persisted ENV:" + fieldName + " to " + BufferFile);
                }
                return System.Environment.GetEnvironmentVariable(EnvVarName);
            }

            string DefaultValue;
            if (System.IO.File.Exists(BufferFile))
                DefaultValue = System.IO.File.ReadAllText(BufferFile);
            else
                DefaultValue = "";

            if (!string.IsNullOrWhiteSpace(DefaultValue))
                return DefaultValue;

            throw new InvalidOperationException("Missing persisted input for field \"" + fieldName + "\", use environment variable " + EnvVarName + " or write to disk by code with method PersistInputValue()\r\n" + 
                "Ex. run following customized batch to create local temp-files-cache for credentials (works on WKSxxxx workstations only):\r\n" +
                "@echo off\r\n" +
                "SET TEST_USERNAME=xy@abc.login\r\n" +
                "SET TEST_CUSTOMERNO=1234567\r\n" +
                "SET TEST_PASSWORD=xxxxxxx(encode with leading ^-char )\r\n" +
                "dotnet test"
                );
        }

        private static string BufferFilePath(string fieldName)
        {
            string HashedFieldName;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(fieldName);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                HashedFieldName = sb.ToString();
            }
            return System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + HashedFieldName + ".tmp");
        }

        /// <summary>
        /// Buffer (write) an input value to disk (to temp path)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        static void PersistInputValue(string fieldName, string value)
        {
            System.IO.File.WriteAllText(BufferFilePath(fieldName), value);
        }

        /// <summary>
        /// Checked user temp data for buffered field data
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static string InputFromBufferFile(string fieldName)
        {
            string BufferFile = BufferFilePath(fieldName);
            if (System.IO.File.Exists(BufferFile))
                return System.IO.File.ReadAllText(BufferFile);
            else
                return null;
        }

        /// <summary>
        /// Checked user temp data for buffered field data
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static void InputToBufferFile(string fieldName, string value)
        {
            System.IO.File.WriteAllText(BufferFilePath(fieldName), value);
        }
    }
}
