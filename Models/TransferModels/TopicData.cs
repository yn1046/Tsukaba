using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Tsukaba.Models.TransferModels
{
    public class TopicData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int BoardId { get; set; }
    }
}