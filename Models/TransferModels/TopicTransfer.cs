using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Tsukaba.Models.TransferModels
{
    public class TopicTransfer
    {
        public string Title {get;set;}
        public string Message {get;set;}
        public int BoardId {get;set;}
        public IFormFile image {get;set;}
    }
}