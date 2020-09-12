using Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Infrastructure.Models
{

    public abstract class EntityBase
    {
        [Key]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }
    }
}
