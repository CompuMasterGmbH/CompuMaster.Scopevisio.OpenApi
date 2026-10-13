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

        private readonly Action<string> writeToken;

        public TestConfig(bool forbidAuthorization) : this(forbidAuthorization, InputLine,
            () => InputFromBufferFile("accesstoken"), value => InputToBufferFile("accesstoken", value), AuthorizeWithConfiguredCredentials)
        {
        }

        // Test-only seams keep cold-cache regressions away from workstation credentials and token buffers.
        internal TestConfig(bool forbidAuthorization, Func<string, string> readInput, Func<string> readToken,
            Action<string> writeToken, Action<TestConfig> authorize)
        {
            if (readInput == null) throw new ArgumentNullException(nameof(readInput));
            if (readToken == null) throw new ArgumentNullException(nameof(readToken));
            this.writeToken = writeToken ?? throw new ArgumentNullException(nameof(writeToken));
            if (authorize == null) throw new ArgumentNullException(nameof(authorize));
            this.OrganisationName = null;
            this.Username = readInput("username");
            this.ClientNumber = readInput("customer no.");
            this.Password = readInput("password");
            if (string.IsNullOrWhiteSpace(this.Username))
                NUnit.Framework.Assert.Ignore("Test ignored until buffered user credentials added in directory " + System.IO.Path.GetTempPath());
            if (forbidAuthorization)
                this.AccessToken = null;
            else
            {
                this.AccessToken = readToken();
                if (this.AccessToken == null)
                {
                    var tokenInfo = new TestConfig(true, readInput, readToken, writeToken, authorize);
                    authorize(tokenInfo);
                    this.AccessToken = tokenInfo.AccessToken;
                }
            }
        }

        private static void AuthorizeWithConfiguredCredentials(TestConfig tokenInfo)
        {
            (new Api.AuthorizationApi(tokenInfo)).Token(
                "password",
                tokenInfo.ClientNumber,
                null,
                null,
                tokenInfo.Username,
                null,
                tokenInfo.OrganisationName,
                tokenInfo.Password,
                null, null, null, null);
        }

        public override string AccessToken
        {
            get => base.AccessToken;
            set
            {
                if ((!string.IsNullOrWhiteSpace(value)) && (value != this.AccessToken))
                    writeToken(value);
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
                @"dotnet test --filter ""FullyQualifiedName=CompuMaster.Scopevisio.OpenApi.Test.AdditionalApiTests.PersistInputValue"" --framework net8.0"
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
