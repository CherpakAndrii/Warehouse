﻿using System.Text.Json.Serialization;

namespace Models.Api.Req_Res.Common.Request
{
    public class GetOrderListRequestModel : CommonUserRequestModel
    {
        [JsonPropertyName("productId")]
        public int? ProductId { get; set; }
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }
        
    }
}