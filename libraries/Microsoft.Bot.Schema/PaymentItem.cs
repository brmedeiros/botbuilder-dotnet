﻿// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Bot.Schema
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Indicates what the payment request is for and the value asked for
    /// </summary>
    [Obsolete("Bot Framework no longer supports payments.")]
    public partial class PaymentItem
    {
        /// <summary>
        /// Initializes a new instance of the PaymentItem class.
        /// </summary>
        public PaymentItem()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the PaymentItem class.
        /// </summary>
        /// <param name="label">Human-readable description of the item</param>
        /// <param name="amount">Monetary amount for the item</param>
        /// <param name="pending">When set to true this flag means that the
        /// amount field is not final.</param>
        public PaymentItem(string label = default(string), PaymentCurrencyAmount amount = default(PaymentCurrencyAmount), bool? pending = default(bool?))
        {
            Label = label;
            Amount = amount;
            Pending = pending;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets human-readable description of the item
        /// </summary>
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets monetary amount for the item
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public PaymentCurrencyAmount Amount { get; set; }

        /// <summary>
        /// Gets or sets when set to true this flag means that the amount field
        /// is not final.
        /// </summary>
        [JsonProperty(PropertyName = "pending")]
        public bool? Pending { get; set; }

    }
}
