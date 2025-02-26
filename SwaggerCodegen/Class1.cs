using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SwaggerCodegen
{
    public class Class1
    {
        string _FilePath;
        JObject _SwaggerJObject;

        public Class1()
        {
            _FilePath = "C:\\Users\\admin\\Desktop\\selling-partner-api-models\\selling-partner-api-models-main\\models\\amazon-warehousing-and-distribution-model\\awd_2024-05-09.json";
            //_FilePath = "C:\\Users\\admin\\Desktop\\selling-partner-api-models\\selling-partner-api-models-main\\models\\fulfillment-inbound-api-model\\fulfillmentInboundV0.json";
            var SwaggerJson = GetFileContent(_FilePath);
            _SwaggerJObject = JObject.Parse(SwaggerJson);
            //GetModelByPath("/inbound/fba/2024-03-20/inboundPlans/{inboundPlanId}/shipments/{shipmentId}","get");
            AllDefinitionsToModel();
        }
        void AllDefinitionsToModel()
        {
            var fullPath = $"C:\\Users\\admin\\Desktop\\Model\\awd_2024-05-09\\";
            //var fullPath = $"C:\\Users\\admin\\Desktop\\Model\\fulfillmentInbound_v0\\";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            var definitions = _SwaggerJObject["definitions"];
            foreach (var definition in definitions)
            {
                var name = ((JProperty)definition).Name;
                Console.WriteLine(name);
                var classStr = GetClassAttributeStr(definition);
                File.WriteAllText(fullPath + name + ".cs", classStr.ToString());
            }
        }
        StringBuilder GetClassAttributeStr(JToken definition)
        {
            if (definition == null)
                return null;
            var SchemaModelJson = definition.FirstOrDefault();
            StringBuilder ClassText = new StringBuilder();
            ClassText.AppendLine("using System;");
            ClassText.AppendLine("using Newtonsoft.Json;");
            ClassText.AppendLine("using System.Collections.Generic;");
            ClassText.AppendLine("");
            ClassText.AppendLine("namespace SPAPI.Awd.awd_2024_05_09");
            ClassText.AppendLine("{");
            ClassText.AppendLine("    /// <summary>");
            ClassText.AppendLine("    /// " + SchemaModelJson["description"]);
            ClassText.AppendLine("    /// </summary>");
            var SchemaModelProperties = SchemaModelJson.SelectToken("properties");
            var SchemaModelEnums = SchemaModelJson.SelectToken("enum");
            if (SchemaModelProperties != null)
            {
                ClassText.AppendLine("    public class " + ((JProperty)definition).Name);
                ClassText.AppendLine("    {");
                foreach (var prop in SchemaModelProperties)
                {
                    ClassText.AppendLine(GetAttributeStr(prop).ToString());
                }
                ClassText.AppendLine("    }");
            }
            else if(SchemaModelEnums != null)
            {
                ClassText.AppendLine("    public enum " + ((JProperty)definition).Name);
                ClassText.AppendLine("    {");

                var SchemaModelEnumExtension = SchemaModelJson.SelectToken("x-docgen-enum-table-extension");
                foreach (var prop in SchemaModelEnums)
                {
                    var enumName = prop.ToString();
                    var EnumExtension = SchemaModelJson.SelectToken($"x-docgen-enum-table-extension[?(@.value == '{enumName}')]");
                    if (EnumExtension != null)
                    {
                        ClassText.AppendLine("        /// <summary>");
                        ClassText.AppendLine("        /// " + EnumExtension["description"]);
                        ClassText.AppendLine("        /// </summary>");
                    }
                    ClassText.AppendLine("        " + enumName + ",");
                }
                ClassText.AppendLine("    }");
            }
            ClassText.AppendLine("}");
            return ClassText;
        }

        void GetModelByPath(string UrlPath, string Method)
        {
            var Path = GetPath(UrlPath, Method);
            var BodyParameter = Path.SelectToken("$.parameters[?(@.in == 'body' && @.name == 'body')]");
            if (BodyParameter != null)
            {
                var BodyParameter_Ref = ((JObject)BodyParameter).SelectToken("schema.$ref").ToString();
            }
            var Schema_Ref = Path.SelectToken("responses.200.schema.$ref");
            if (Schema_Ref != null)
            {
                GetClassAttributeStr(Schema_Ref.ToString());
            }
        }
        StringBuilder GetClassAttributeStr(string ref_url)
        {
            var SchemaModelJson = _SwaggerJObject.SelectToken(ref_url.ToString().Replace("#", "@").Replace("/", "."));
            if (SchemaModelJson == null)
                return null;
            StringBuilder ClassText = new StringBuilder();
            ClassText.AppendLine("using System;");
            ClassText.AppendLine("using Newtonsoft.Json;");
            ClassText.AppendLine("using System.Collections.Generic;");
            ClassText.AppendLine("namespace SPAPI.Fulfillment.Inbound_v2024_03_20");
            ClassText.AppendLine("{");
            ClassText.AppendLine("    /// <summary>");
            ClassText.AppendLine("    /// " + SchemaModelJson["description"]);
            ClassText.AppendLine("    /// </summary>");
            ClassText.AppendLine("    public class Shipment");
            ClassText.AppendLine("    {");
            var SchemaModelProperties = SchemaModelJson.SelectToken("@.properties");
            if (SchemaModelProperties != null)
            {
                foreach (var prop in SchemaModelProperties)
                {
                    ClassText.AppendLine(GetAttributeStr(prop).ToString());
                }
            }
            ClassText.AppendLine("    }");
            ClassText.AppendLine("}");
            return ClassText;
        }
        StringBuilder GetAttributeStr(JToken definition)
        {
            var Name = ((JProperty)definition).Name;
            var PropertieItem = definition.FirstOrDefault();
            string description = string.Empty, type = string.Empty;
            if (PropertieItem.SelectToken("description") != null)
                description = PropertieItem["description"].ToString();
            if (PropertieItem.SelectToken("type") != null)
                type = PropertieItem["type"].ToString();
            if (PropertieItem.SelectToken("$ref") != null)
            {
                var ref_url = PropertieItem["$ref"].ToString();
                var SchemaModelJson = _SwaggerJObject.SelectToken(ref_url.Replace("#", "").Replace("/", "."));
                type = ref_url.Split("/").LastOrDefault().FirstToUpper();
                if(string.IsNullOrEmpty(description))
                    description = SchemaModelJson["description"].ToString();
            }
            if (type == "array" && PropertieItem.SelectToken("items") != null)
            {
                type = "List<>";
                var ArrayType = string.Empty;
                var PropertieItemArray = PropertieItem.SelectToken("items");
                if (PropertieItemArray.SelectToken("$ref") != null)
                {
                    var ref_url = PropertieItemArray["$ref"].ToString();
                    ArrayType = ref_url.Split("/").LastOrDefault().FirstToUpper();
                    
                }
                else if(PropertieItem.SelectToken("type") != null)
                    ArrayType = PropertieItemArray["type"].ToString();
                type = "List<" + ArrayType + ">";
            }
            switch (type)
            {
                case "number":
                    type = "decimal";
                    break;
                case "integer":
                    type = "int";
                    break;
                default:
                    break;
            }
            if (description.Contains("\n\n"))
                description = description.Replace("\n\n", "\n        /// ");
            StringBuilder str = new StringBuilder();
            str.AppendLine("        /// <summary>");
            str.AppendLine("        /// " + description);
            str.AppendLine("        /// </summary>");
            str.AppendLine($"        [JsonProperty(PropertyName = \"{Name}\")]");
            str.AppendLine($"        public {type} {Name.FirstToUpper()} {{ get; set; }}");
            return str;
        }
        
        JObject GetPath(string PathName,string Method)
        {
            var PathJToken = _SwaggerJObject.SelectToken($"paths.{PathName}.{Method.ToLower()}");
            if (PathJToken == null)
                return null;
            return (JObject)PathJToken;
        }
        string GetFileContent(string FilePath)
        {
            if (!File.Exists(FilePath))
                return "";
            var content = File.ReadAllText(FilePath);
            return content;
        }
    }

    public static class ClassTool
    {
        public static string FirstToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}


