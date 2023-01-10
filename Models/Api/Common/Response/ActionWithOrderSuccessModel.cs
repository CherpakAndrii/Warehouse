﻿using System.Text.Json.Serialization;
using Models.Api.ApiEntityModels;

namespace Models.Api.Common.Response
{
    public abstract class ActionWithOrderSuccessModel
    {
        [JsonPropertyName("order")]
        public OrderModel Order { get; set; }
    }
}
