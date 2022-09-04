﻿using System.Text.Json.Serialization;

namespace BlogApplication.Api.WebApi.Results
{
    public class ValidationResponseModel
    {
        public ValidationResponseModel()
        {

        }
        public ValidationResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ValidationResponseModel(string message) : this(new List<string>() { message })
        {

        }

        public IEnumerable<string> Errors { get; set; }

        [JsonIgnore]
        public string FalttenErrors => Errors != null ? string.Join(Environment.NewLine, Errors) : string.Empty;
    }
}
