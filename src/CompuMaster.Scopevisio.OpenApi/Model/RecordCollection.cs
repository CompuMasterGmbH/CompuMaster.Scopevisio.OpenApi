using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = CompuMaster.Scopevisio.OpenApi.Client.OpenAPIDateConverter;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>
    /// A list of strong-typed records created from JObject records
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecordCollection<T> : Records<T> where T : RecordItem, new()
    {
        /// <summary>
        /// A list of strong-typed records created from JObject records
        /// </summary>
        /// <param name="records"></param>
        public RecordCollection(Records<Newtonsoft.Json.Linq.JObject> records) 
        {
            this.Items = new List<T>();
            foreach (Newtonsoft.Json.Linq.JObject record in records.Items)
            {
                T newItem = (T)Activator.CreateInstance(typeof(T), record);
                this.Items.Add(newItem);
            }
        }

        private List<int> ToOverviewTableColumnSizes()
        {
            var result = new List<int>();
            List<string> captions = new T().ToOverviewTableCaptions();
            foreach(string caption in captions)
            {
                result.Add(caption.Length);
            }
            for (int rowcounter = 0; rowcounter < result.Count; rowcounter++)
            {
                List<string> rowValues = this.Items[rowcounter].ToOverviewTableRow();
                for (int colcounter = 0; colcounter < result.Count; colcounter++)
                {
                    string rowValue;
                    if (rowValues[colcounter] == null)
                        rowValue = "";
                    else
                        rowValue = rowValues[colcounter].ToString();
                    result[colcounter] = System.Math.Max(result[colcounter], rowValue.Length);
                }
            }
            return result;
        }

        private string FixedStringSize(string text, int size)
        {
            if (text == null)
                return new String(' ', size);
            else if (text.Length == size)
                return text;
            else if (text.Length < size)
                return text + new String(' ', size - text.Length);
            else //if (text.Length > size)
                return text.Substring(0, text.Length - 3) + "...";
        }

        /// <summary>
        /// Create an overview table with auto-sized column length
        /// </summary>
        /// <returns></returns>
        public string ToOverviewTable()
        {
            var sb = new StringBuilder();
            List<int> ColSizes = this.ToOverviewTableColumnSizes();
            List<string> captions = new T().ToOverviewTableCaptions();
            for (int colcounter = 0; colcounter < captions.Count; colcounter++)
            {
                string caption = captions[colcounter];
                if (colcounter > 0)
                    sb.Append('|');
                sb.Append(this.FixedStringSize(caption, ColSizes[colcounter]));
            }
            sb.AppendLine();
            for (int colcounter = 0; colcounter < captions.Count; colcounter++)
            {
                if (colcounter > 0)
                    sb.Append('+');
                sb.Append(new String('-', ColSizes[colcounter]));
            }
            sb.AppendLine();
            for (int rowcounter = 0; rowcounter < this.Items.Count; rowcounter++)
            {
                List<string> rowValues = this.Items[rowcounter].ToOverviewTableRow();
                for (int colcounter = 0; colcounter < rowValues.Count; colcounter++)
                {
                    string rowValue;
                    if (rowValues[colcounter] == null)
                        rowValue = "";
                    else
                        rowValue = rowValues[colcounter].ToString();
                    if (colcounter > 0)
                        sb.Append('|');
                    sb.Append(this.FixedStringSize(rowValue, ColSizes[colcounter]));
                }
                if (rowcounter < this.Items.Count - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
