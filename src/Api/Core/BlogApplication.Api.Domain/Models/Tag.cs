﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models;

public class Tag 
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Post> Posts { get; set; }
}