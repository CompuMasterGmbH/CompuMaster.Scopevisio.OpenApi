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
                    (new Api.AuthorizationApi(new TestConfig())).Token("password", (new TestConfig()).ClientNumber, null, null, (new TestConfig()).Username, null, (new TestConfig()).OrganisationName, (new TestConfig()).Password, null, null, null, null);
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
            string EnvVarName = "TEST_" + fieldName.ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable(EnvVarName)))
                return System.Environment.GetEnvironmentVariable(EnvVarName);

            string BufferFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + fieldName.GetHashCode().ToString() + ".tmp");
            string DefaultValue;
            if (System.IO.File.Exists(BufferFile))
                DefaultValue = System.IO.File.ReadAllText(BufferFile);
            else
                DefaultValue = "";

            if (!string.IsNullOrWhiteSpace(DefaultValue))
                return DefaultValue;

            throw new InvalidOperationException("Missing persisted input for field \"" + fieldName + "\", use environment variable " + EnvVarName + " or write to disk by code with method PersistInputValue()");
        }

        /// <summary>
        /// Buffer (write) an input value to disk (to temp path)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        static void PersistInputValue(string fieldName, string value)
        {
            string BufferFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + fieldName.GetHashCode().ToString() + ".tmp");
            System.IO.File.WriteAllText(BufferFile, value);
        }

        /// <summary>
        /// Checked user temp data for buffered field data
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        static string InputFromBufferFile(string fieldName)
        {
            string BufferFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + fieldName.GetHashCode().ToString() + ".tmp");
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
            string BufferFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "~Buffer.Sample." + fieldName.GetHashCode().ToString() + ".tmp");
            System.IO.File.WriteAllText(BufferFile, value);
        }
    }
}
