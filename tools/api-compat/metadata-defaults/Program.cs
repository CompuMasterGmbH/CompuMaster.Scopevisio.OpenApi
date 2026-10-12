using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length != 2) throw new ArgumentException("Expected baseline and current assembly paths.");
        var left = Read(args[0]); var right = Read(args[1]);
        var differences = left.Where(pair => !right.TryGetValue(pair.Key, out var value) || value != pair.Value).ToList();
        foreach (var pair in differences) Console.Error.WriteLine("Public/protected optional/default/constant metadata differs: " + pair.Key);
        Console.WriteLine("Compared " + left.Count + " existing public/protected parameter and constant records.");
        return differences.Count == 0 ? 0 : 1;
    }
    private static Dictionary<string, string> Read(string path)
    {
        using var input = File.OpenRead(path); using var image = new PEReader(input);
        var reader = image.GetMetadataReader(); var names = new Names();
        var records = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var handle in reader.TypeDefinitions)
        {
            if (!Visible(reader, handle)) continue;
            var type = reader.GetTypeDefinition(handle); var typeName = names.GetTypeFromDefinition(reader, handle, 0);
            foreach (var methodHandle in type.GetMethods())
            {
                var method = reader.GetMethodDefinition(methodHandle); var access = method.Attributes & MethodAttributes.MemberAccessMask;
                if (access != MethodAttributes.Public && access != MethodAttributes.Family &&
                    access != MethodAttributes.FamORAssem && access != MethodAttributes.FamANDAssem) continue;
                var signature = method.DecodeSignature(names, (object)null);
                var key = typeName + "." + reader.GetString(method.Name) + (char)96 + signature.GenericParameterCount +
                    "(" + string.Join(",", signature.ParameterTypes) + ")";
                foreach (var parameterHandle in method.GetParameters())
                {
                    var parameter = reader.GetParameter(parameterHandle); if (parameter.SequenceNumber == 0) continue;
                    var flags = parameter.Attributes & (ParameterAttributes.Optional | ParameterAttributes.HasDefault);
                    records.Add(key + "#" + parameter.SequenceNumber, ((int)flags).ToString() + ":" + Constant(reader, parameter.GetDefaultValue()));
                }
            }
            foreach (var fieldHandle in type.GetFields())
            {
                var field = reader.GetFieldDefinition(fieldHandle); var access = field.Attributes & FieldAttributes.FieldAccessMask;
                if ((access == FieldAttributes.Public || access == FieldAttributes.Family ||
                     access == FieldAttributes.FamORAssem || access == FieldAttributes.FamANDAssem) &&
                    (field.Attributes & FieldAttributes.Literal) != 0)
                    records.Add(typeName + "." + reader.GetString(field.Name), Constant(reader, field.GetDefaultValue()));
            }
        }
        return records;
    }
    private static string Constant(MetadataReader reader, ConstantHandle handle)
    {
        if (handle.IsNil) return "absent"; var value = reader.GetConstant(handle);
        return value.TypeCode + ":" + Convert.ToHexString(reader.GetBlobBytes(value.Value));
    }
    private static bool Visible(MetadataReader reader, TypeDefinitionHandle handle)
    {
        var type = reader.GetTypeDefinition(handle); var visibility = type.Attributes & TypeAttributes.VisibilityMask;
        if (visibility == TypeAttributes.Public) return true;
        if (visibility != TypeAttributes.NestedPublic && visibility != TypeAttributes.NestedFamily &&
            visibility != TypeAttributes.NestedFamORAssem && visibility != TypeAttributes.NestedFamANDAssem) return false;
        var parent = type.GetDeclaringType(); return !parent.IsNil && Visible(reader, parent);
    }
    private sealed class Names : ISignatureTypeProvider<string, object>
    {
        public string GetArrayType(string type, ArrayShape shape) => type + "[" + new string(',', shape.Rank - 1) + "]";
        public string GetByReferenceType(string type) => type + "&";
        public string GetFunctionPointerType(MethodSignature<string> signature) => "fn(" + string.Join(",", signature.ParameterTypes) + ")->" + signature.ReturnType;
        public string GetGenericInstantiation(string type, System.Collections.Immutable.ImmutableArray<string> arguments) => type + "<" + string.Join(",", arguments) + ">";
        public string GetGenericMethodParameter(object context, int index) => "!!" + index;
        public string GetGenericTypeParameter(object context, int index) => "!" + index;
        public string GetModifiedType(string modifier, string type, bool required) => type + (required ? " modreq(" : " modopt(") + modifier + ")";
        public string GetPinnedType(string type) => type + " pinned";
        public string GetPointerType(string type) => type + "*";
        public string GetPrimitiveType(PrimitiveTypeCode code) => code.ToString();
        public string GetSZArrayType(string type) => type + "[]";
        public string GetTypeFromDefinition(MetadataReader reader, TypeDefinitionHandle handle, byte rawKind)
        {
            var type = reader.GetTypeDefinition(handle); var parent = type.GetDeclaringType();
            return parent.IsNil ? Qualified(reader.GetString(type.Namespace), reader.GetString(type.Name)) :
                GetTypeFromDefinition(reader, parent, 0) + "+" + reader.GetString(type.Name);
        }
        public string GetTypeFromReference(MetadataReader reader, TypeReferenceHandle handle, byte rawKind)
        {
            var type = reader.GetTypeReference(handle);
            return type.ResolutionScope.Kind == HandleKind.TypeReference ?
                GetTypeFromReference(reader, (TypeReferenceHandle)type.ResolutionScope, 0) + "+" + reader.GetString(type.Name) :
                Qualified(reader.GetString(type.Namespace), reader.GetString(type.Name));
        }
        public string GetTypeFromSpecification(MetadataReader reader, object context, TypeSpecificationHandle handle, byte rawKind)
            => reader.GetTypeSpecification(handle).DecodeSignature(this, context);
        private static string Qualified(string space, string name) => space.Length == 0 ? name : space + "." + name;
    }
}
