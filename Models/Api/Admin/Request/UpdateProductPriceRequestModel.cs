﻿using System.Text.Json.Serialization;
using Models.Api.Common.Request;

namespace Models.Api.Admin.Request
{
    public class UpdateProductPriceRequestModel : ActionWithExistingProductRequestModel
    {
        [JsonPropertyName("newProductPrice")]
        public float NewProductPrice { get; set; }
    }
}
