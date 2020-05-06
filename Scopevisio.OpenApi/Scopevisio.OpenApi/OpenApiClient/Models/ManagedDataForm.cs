﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CompuMaster.Scopevisio.OpenApi.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class ManagedDataForm
    {
        /// <summary>
        /// Initializes a new instance of the ManagedDataForm class.
        /// </summary>
        public ManagedDataForm() { }

        /// <summary>
        /// Initializes a new instance of the ManagedDataForm class.
        /// </summary>
        public ManagedDataForm(string json, long? validFrom = default(long?), long? validTill = default(long?), long? permissionType = default(long?), string readPermissionProfiles = default(string), string readPermissionLogins = default(string), string writePermissionProfiles = default(string), string writePermissionLogins = default(string))
        {
            ValidFrom = validFrom;
            ValidTill = validTill;
            Json = json;
            PermissionType = permissionType;
            ReadPermissionProfiles = readPermissionProfiles;
            ReadPermissionLogins = readPermissionLogins;
            WritePermissionProfiles = writePermissionProfiles;
            WritePermissionLogins = writePermissionLogins;
        }

        /// <summary>
        /// validFrom
        /// </summary>
        [JsonProperty(PropertyName = "validFrom")]
        public long? ValidFrom { get; set; }

        /// <summary>
        /// validTill
        /// </summary>
        [JsonProperty(PropertyName = "validTill")]
        public long? ValidTill { get; set; }

        /// <summary>
        /// the json data to be imported
        /// </summary>
        [JsonProperty(PropertyName = "json")]
        public string Json { get; set; }

        /// <summary>
        /// 0 = Öffentlich, 1 = Benutzerdefiniert, 2 = Persönlich
        /// </summary>
        [JsonProperty(PropertyName = "permissionType")]
        public long? PermissionType { get; set; }

        /// <summary>
        /// comma separated list of profiles for the read permissions,
        /// evaluated only if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "readPermissionProfiles")]
        public string ReadPermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails for the read
        /// permissions, evaluated only if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "readPermissionLogins")]
        public string ReadPermissionLogins { get; set; }

        /// <summary>
        /// comma separated list of profiles for full access, evaluated only
        /// if permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "writePermissionProfiles")]
        public string WritePermissionProfiles { get; set; }

        /// <summary>
        /// comma separated list of user login emails, evaluated only if
        /// permissionType equals 1.
        /// </summary>
        [JsonProperty(PropertyName = "writePermissionLogins")]
        public string WritePermissionLogins { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Json == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Json");
            }
        }
    }
}