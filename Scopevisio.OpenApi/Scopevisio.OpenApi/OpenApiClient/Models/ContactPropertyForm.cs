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

    public partial class ContactPropertyForm
    {
        /// <summary>
        /// Initializes a new instance of the ContactPropertyForm class.
        /// </summary>
        public ContactPropertyForm() { }

        /// <summary>
        /// Initializes a new instance of the ContactPropertyForm class.
        /// </summary>
        public ContactPropertyForm(string type = default(string), string subtype = default(string), string description = default(string), long? validFrom = default(long?), long? validTo = default(long?), bool? employeeDomain = default(bool?), string spheresDisplay = default(string), string employeeFilterDisplay = default(string), long? permissionType = default(long?), string readPermissionProfiles = default(string), string readPermissionLogins = default(string), string writePermissionProfiles = default(string), string writePermissionLogins = default(string))
        {
            Type = type;
            Subtype = subtype;
            Description = description;
            ValidFrom = validFrom;
            ValidTo = validTo;
            EmployeeDomain = employeeDomain;
            SpheresDisplay = spheresDisplay;
            EmployeeFilterDisplay = employeeFilterDisplay;
            PermissionType = permissionType;
            ReadPermissionProfiles = readPermissionProfiles;
            ReadPermissionLogins = readPermissionLogins;
            WritePermissionProfiles = writePermissionProfiles;
            WritePermissionLogins = writePermissionLogins;
        }

        /// <summary>
        /// Sets the values in column "Art der Information". For allowable
        /// values see catalog Systemadministration-&gt;System -&gt;
        /// Auswahllisten Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Art
        /// der Information
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Sets the values in column "Untergruppierung". For allowable values
        /// see catalog Systemadministration-&gt;System -&gt; Auswahllisten
        /// Verwalten -&gt; Allgemeine Kontaktdaten -&gt; Untergruppierung
        /// </summary>
        [JsonProperty(PropertyName = "subtype")]
        public string Subtype { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// validFrom
        /// </summary>
        [JsonProperty(PropertyName = "validFrom")]
        public long? ValidFrom { get; set; }

        /// <summary>
        /// validTo
        /// </summary>
        [JsonProperty(PropertyName = "validTo")]
        public long? ValidTo { get; set; }

        /// <summary>
        /// employeeDomain
        /// </summary>
        [JsonProperty(PropertyName = "employeeDomain")]
        public bool? EmployeeDomain { get; set; }

        /// <summary>
        /// spheres. Not set means visible in all spheres. . Possible values
        /// include: 'Kontakt', 'Lead', 'Interessent', 'Kunde', 'Lieferant',
        /// 'Debitor', 'Kreditor', 'Mitarbeiter', 'Partner', 'Arbeitnehmer'
        /// </summary>
        [JsonProperty(PropertyName = "spheresDisplay")]
        public string SpheresDisplay { get; set; }

        /// <summary>
        /// In which employer contact should this information be visible. Not
        /// set equals "Bei allen AG". Possible values include: 'Bei allen
        /// AG', 'Bei keinem AG', 'comma separated list of the employer
        /// contact names'
        /// </summary>
        [JsonProperty(PropertyName = "employeeFilterDisplay")]
        public string EmployeeFilterDisplay { get; set; }

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

    }
}
