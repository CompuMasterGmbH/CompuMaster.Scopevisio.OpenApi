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

    public partial class ProductForm
    {
        /// <summary>
        /// Initializes a new instance of the ProductForm class.
        /// </summary>
        public ProductForm() { }

        /// <summary>
        /// Initializes a new instance of the ProductForm class.
        /// </summary>
        public ProductForm(string unit, string number = default(string), string name = default(string), string description = default(string), long? type = default(long?), string typeDisplay = default(string), string invoicePostingMode = default(string), double? taxRate = default(double?), long? revenueAccountMode = default(long?), string revenueAccount0 = default(string), string revenueAccount1 = default(string), string revenueAccount2 = default(string), string revenueAccount3 = default(string), bool? asIfSold = default(bool?), bool? billable = default(bool?), bool? locked = default(bool?), double? planValue = default(double?), double? costPrice = default(double?), double? singleAmount = default(double?), double? singleAmount2 = default(double?), double? singleAmount3 = default(double?), double? singleAmountGross = default(double?), double? singleAmountGross2 = default(double?), double? singleAmountGross3 = default(double?), string nameLng1 = default(string), string descriptionLng1 = default(string), string productGroupName = default(string))
        {
            Number = number;
            Name = name;
            Description = description;
            Type = type;
            TypeDisplay = typeDisplay;
            InvoicePostingMode = invoicePostingMode;
            TaxRate = taxRate;
            RevenueAccountMode = revenueAccountMode;
            RevenueAccount0 = revenueAccount0;
            RevenueAccount1 = revenueAccount1;
            RevenueAccount2 = revenueAccount2;
            RevenueAccount3 = revenueAccount3;
            Unit = unit;
            AsIfSold = asIfSold;
            Billable = billable;
            Locked = locked;
            PlanValue = planValue;
            CostPrice = costPrice;
            SingleAmount = singleAmount;
            SingleAmount2 = singleAmount2;
            SingleAmount3 = singleAmount3;
            SingleAmountGross = singleAmountGross;
            SingleAmountGross2 = singleAmountGross2;
            SingleAmountGross3 = singleAmountGross3;
            NameLng1 = nameLng1;
            DescriptionLng1 = descriptionLng1;
            ProductGroupName = productGroupName;
        }

        /// <summary>
        /// number(Produktnummer). "number" is a mandatory field when saving a
        /// product. If a new product is created and no "number" provided a
        /// value is generated and assigned to the product automatically.
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        /// <summary>
        /// name(Bezeichnung)
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// description(Beschreibung)
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// type(Produkttyp nummerisch) "type" is set automtically on the
        /// basis of "typeDisplay".
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public long? Type { get; private set; }

        /// <summary>
        /// typeDisplay(Produktart), default value is "Ware" if not set by the
        /// user. Possible values include: 'Ware', 'Material', 'Leistung',
        /// 'Recht'
        /// </summary>
        [JsonProperty(PropertyName = "typeDisplay")]
        public string TypeDisplay { get; set; }

        /// <summary>
        /// invoicePostingMode(Zuordnung Rechnungsart), default value is
        /// "Einzel-/Teil-/Schlussrechnung" if not set by the user  .
        /// Possible values include: 'Einzel-/Teil-/Schlussrechnung',
        /// 'Anzahlungs-/Abschlagsrechnung'
        /// </summary>
        [JsonProperty(PropertyName = "invoicePostingMode")]
        public string InvoicePostingMode { get; set; }

        /// <summary>
        /// taxRate(Steuersatz), default value is 0% if not set by the user.
        /// </summary>
        [JsonProperty(PropertyName = "taxRate")]
        public double? TaxRate { get; set; }

        /// <summary>
        /// revenueAccountMode(0 = Standard-Erlöskonten verwenden, 1 =
        /// Manuelle Erlöskonten verwenden). If revenue accounts are provided
        /// ("revenueAccount0", etc.), it is set automatically to 1. Default
        /// value is 0 Standard-Erlöskonten verwenden) if not set by the user
        /// explicitly.
        /// </summary>
        [JsonProperty(PropertyName = "revenueAccountMode")]
        public long? RevenueAccountMode { get; set; }

        /// <summary>
        /// revenueAccount0(Manuelle erlöskonten verwenden - Inland)
        /// </summary>
        [JsonProperty(PropertyName = "revenueAccount0")]
        public string RevenueAccount0 { get; set; }

        /// <summary>
        /// revenueAccount1(Manuelle erlöskonten verwenden - EU Ausland)
        /// </summary>
        [JsonProperty(PropertyName = "revenueAccount1")]
        public string RevenueAccount1 { get; set; }

        /// <summary>
        /// revenueAccount2(Manuelle erlöskonten verwenden - Ausland)
        /// </summary>
        [JsonProperty(PropertyName = "revenueAccount2")]
        public string RevenueAccount2 { get; set; }

        /// <summary>
        /// revenueAccount3(Manuelle erlöskonten verwenden -
        /// Abschlagsrechnungen)
        /// </summary>
        [JsonProperty(PropertyName = "revenueAccount3")]
        public string RevenueAccount3 { get; set; }

        /// <summary>
        /// Unit(Einheit). For more untits please check the Scopevisio client
        /// application, catalog Abrechnungsbelege -&gt; Einheit. Possible
        /// values include: 'Gramm', 'Kartons', 'Kilogramm', 'Kilometer',
        /// 'Kisten', 'Kubikmeter', 'Liter', 'Meter', 'Miligramm',
        /// 'Mililiter', 'Minuten', 'Monate', 'Paket', 'Paletten',
        /// 'Pauschale', 'Quadratmeter', 'Stück', 'Stunden', 'Tage', 'Tonnen'
        /// </summary>
        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        /// <summary>
        /// asIfSold(As-if-sold)
        /// </summary>
        [JsonProperty(PropertyName = "asIfSold")]
        public bool? AsIfSold { get; set; }

        /// <summary>
        /// billable(Fakturierbar)
        /// </summary>
        [JsonProperty(PropertyName = "billable")]
        public bool? Billable { get; set; }

        /// <summary>
        /// locked(Gesperrt)
        /// </summary>
        [JsonProperty(PropertyName = "locked")]
        public bool? Locked { get; set; }

        /// <summary>
        /// planValue(Planwert)
        /// </summary>
        [JsonProperty(PropertyName = "planValue")]
        public double? PlanValue { get; set; }

        /// <summary>
        /// costPrice(Einkaufspreis)
        /// </summary>
        [JsonProperty(PropertyName = "costPrice")]
        public double? CostPrice { get; set; }

        /// <summary>
        /// singleAmount(Netto Preisgruppe 1)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmount")]
        public double? SingleAmount { get; set; }

        /// <summary>
        /// singleAmount2(Netto Preisgruppe 2)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmount2")]
        public double? SingleAmount2 { get; set; }

        /// <summary>
        /// singleAmount3(Netto Preisgruppe 3)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmount3")]
        public double? SingleAmount3 { get; set; }

        /// <summary>
        /// singleAmountGross(Brutto Preisgruppe 1)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmountGross")]
        public double? SingleAmountGross { get; set; }

        /// <summary>
        /// singleAmountGross2(Brutto Preisgruppe 2)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmountGross2")]
        public double? SingleAmountGross2 { get; set; }

        /// <summary>
        /// singleAmountGross3(Brutto Preisgruppe 3)
        /// </summary>
        [JsonProperty(PropertyName = "singleAmountGross3")]
        public double? SingleAmountGross3 { get; set; }

        /// <summary>
        /// nameLng1(Englisch Bezeichnung)
        /// </summary>
        [JsonProperty(PropertyName = "nameLng1")]
        public string NameLng1 { get; set; }

        /// <summary>
        /// descriptionLng1(Englisch Beschreibung)
        /// </summary>
        [JsonProperty(PropertyName = "descriptionLng1")]
        public string DescriptionLng1 { get; set; }

        /// <summary>
        /// productGroupName(Produktgruppen)
        /// </summary>
        [JsonProperty(PropertyName = "productGroupName")]
        public string ProductGroupName { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Unit == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Unit");
            }
        }
    }
}