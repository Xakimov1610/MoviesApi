using System;

namespace movie.Models
{
    public class NewImage
    { 
           public Guid Id{get;set;} =Guid.NewGuid(); 
    }
}